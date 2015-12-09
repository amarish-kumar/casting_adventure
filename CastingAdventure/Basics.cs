using System;
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
            D d4 = (D)d1; // Cast is NOT needed, d1 is already D
            D d5 = (D)b2; // Casting B to D, OK
            //D d6 = (D)b1; // RTE - B is NOT Derived from D, can't be Cast to D
            //B b5 = (B) o1; // RTE - Object is NOT Derived from B, can't be Cast to B
            B b6 = (D)b2;
        }

        [Fact]
        public void Numeric_conversion_with_unboxing() {
            Object o1 = new System.Int32();
            System.Int32 i1 = new System.Int32();

            //float f1 = (float) o1; // RTE - Unboxing Object to Float
            float f1 = (float)i1;
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

            Point p1 = (Point)a[0]; // Unboxing Point from ArrayList boxed item
        }

        [Fact]
        public void Invalid_cast_exception() {

            //If the variable containing the reference to the boxed value type instance is null, a NullReferenceException is thrown.
            //If the reference doesn’t refer to an object that is a boxed instance of the desired value type, an InvalidCastException is thrown.

            Int32 x = 5;
            Object o = x; // Box x; o refers to the boxed Int32, NOT a boxed Int16
                          //_output.WriteLine(o.GetType().ToString()); // Int32 NOT Int16
            Exception e = Assert.Throws<InvalidCastException>(() => { Int16 y = (Int16)o; });
        }
        [Fact]
        public void Valid_unboxing() {

            // Following the rule that boxed instance can 
            // only be unboxed to desired value type

            Int32 x = 5;
            Object o = x;
            Int16 y = (Int16)(Int32)o; // Valid because we are unboxing to Int32,
                                       // which is the same as the originally boxed type
        }
        [Fact]
        public void Unbox_and_copy() {
            Point p;
            p.x = p.y = 1;
            Object o = p;   // Boxes p; o refers to the boxed instance

            p = (Point)o;  // Unboxes o AND copies fields from boxed
                           // instance to stack variable
        }
        [Fact]
        public void Boxing_unboxing_copying_boxing() {
            Point p;
            p.x = p.y = 1;
            Object o = p; // Boxes p; o refers to boxed instance

            // Change Point's x field to 2
            p = (Point)o; // Unboxes o AND copies fields from boxed
                            // instance to stack variable

            p.x = 2;
            o = p;
        }
        [Fact]
        public void Unboxed_boxed_different_values() {
            Int32 v = 5;            // Create an unboxed value type variable.
            Object o = v;            // o refers to a boxed Int32 containing 5.
            v = 123;                 // Changes the unboxed value to 123

            _output.WriteLine(v + ", " + (Int32)o); // Displays "123, 5"
        }

        [Fact]
        public void Boxing_in_ArrayList() {
            ArrayList a = new ArrayList();
            int b;            
            for (Int32 i = 0; i < 10; i++) {
                b = 10;  
                a.Add(b);        
            }
            Exception e = Assert.Throws<InvalidCastException>(() => { float f = (float)a[0]; });
        }
    }
}
