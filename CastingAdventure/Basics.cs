﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CastingAdventure {
    internal class B { // Base class
        
    }
    internal class D : B { // Derived class
        
    }
    public class BasicsTests {
        private readonly ITestOutputHelper _output;
        public BasicsTests(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void Valid_casting_tests() {
            Object o1 = new Object();
            Object o2 = new B(); // B is Derived from Object
            Object o3 = new D();
            Object o4 = o3;
            B b1 = new B();
            B b2 = new D(); // D is Derived from B
            D d1 = new D();
            //B b3 = new Object(); // CTE - Object is NOT Derived from B
            //D d2 = new Object(); // CTE - Object is NOT Derived from D
            B b4 = d1;
            //D d3 = b2; // CTE b2 is a D Casted to B, B is NOT Derived from D
            D d4 = (D) d1; // Cast is NOT needed, d1 is already D
            D d5 = (D) b2; // Casting B to D, OK
            //D d6 = (D)b1; // RTE - B is NOT Derived from D, can't be Cast to D
            //B b5 = (B) o1; // RTE - Object is NOT Derived from B, can't be Cast to B
            B b6 = (D) b2; 
        }

        [Fact]
        public void Numeric_conversion_with_unboxing() {
            Object o1 = new System.Int32();
            System.Int32 i1 = new System.Int32();

            //float f1 = (float) o1; // RTE - Unboxing Object to Float
            float f2 = (float) i1;
        }
        
        struct Point {
            public Int32 x, y;
        }
        [Fact]
        public void Unboxing_of_ArrayList_boxed_items() {
            ArrayList a = new ArrayList(); // ArrayList is Reference Type (allocated in the heap)
            Point p;            // Allocate a Point (not in the heap).
            for (Int32 i = 0; i < 10; i++) {
                p.x = p.y = i;   // Initialize the members in the value type.
                a.Add(p);        // Box the value type and add the
                                 // reference to the Arraylist.
            }

            Point p1 = (Point) a[0]; // Unboxing Point from ArrayList boxed item
        }
    }
}