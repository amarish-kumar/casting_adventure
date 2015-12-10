using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CastingAdventure {
    public class ConversionOperators {
        private readonly ITestOutputHelper _output;
        public ConversionOperators(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void Conversion_operator_float() {
            Object o1 = new System.Int32();
            _output.WriteLine(o1.GetType().ToString()); // Evaluates to System.Int32 even thought it's Object
                                                        // This is good because how else would you be able to
                                                        // see what the type of a classes field is?
                                                        // That field is boxed         
            // "However, calling a nonvirtual inherited method (such as GetType or MemberwiseClone) always requires the value type to be boxed because these methods are defined by System.Object, so the methods expect the this argument to be a pointer that refers to an object on the heap."
            // We wouldn't be able to call .GetType() unless we boxed it

            float f1 = Convert.ToSingle(o1); // It's a boxed Int32
                                             // We are actually passing a reference type (Object) to ToSingle()
            float f2 = ToSingleTest(o1); // Hand coded version of the static Convert.ToSingle(object value) method

            Assert.Equal("System.Single", f1.GetType().ToString());
            Assert.Equal("System.Single", f2.GetType().ToString());
        }

        [Fact]
        public void GetType_implications() {
            Object o1 = new System.Int32();
            System.Int32 i1 = new System.Int32();

            _output.WriteLine(i1.GetType().ToString()); // Did this get boxed per the requirement?
            _output.WriteLine(IsBoxed(i1).ToString());

            float l2 = ToSingleTest(i1); // Gets unboxed as it's cast to int parameter?
            float l3 = IntToSingleTest(i1); // It can be param type of Object or Int32 
        }

        [Fact]
        public void Another_bad_cast_example() {
            Object i = 5;   
            Int64 l = (Int64) i;

            int i2 = 5;
            float f = (int)i2;
        }

        // http://referencesource.microsoft.com/#mscorlib/system/convert.cs,1c5255ca394e755d
        public float IntToSingleTest(int value) {
            return value;
        }

        public float ToSingleTest(object value) {
            return value == null ? 0 : ((IConvertible)value).ToSingle(null);
        }

        // https://stackoverflow.com/questions/5806898/how-to-test-whether-a-value-is-boxed-in-c-sharp-net 
        public static bool IsBoxed(object item) {
            return true;
        }
        public static bool IsBoxed<T>(T item) where T : struct {
            return false;
        }

        struct Point : IConvertible {
            public Int32 x, y;

            public TypeCode GetTypeCode() {
                throw new NotImplementedException();
            }
            public bool ToBoolean(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public char ToChar(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public sbyte ToSByte(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public byte ToByte(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public short ToInt16(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public ushort ToUInt16(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public int ToInt32(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public uint ToUInt32(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public long ToInt64(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public ulong ToUInt64(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public float ToSingle(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public double ToDouble(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public decimal ToDecimal(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public DateTime ToDateTime(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public string ToString(IFormatProvider provider) {
                throw new NotImplementedException();
            }
            public object ToType(Type conversionType, IFormatProvider provider) {
                throw new NotImplementedException();
            }
        }
    }
}
