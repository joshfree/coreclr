// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void ParallelBitDepositUInt32()
        {
            var test = new ScalarBinaryOpTest__ParallelBitDepositUInt32();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.ReadUnaligned
                test.RunBasicScenario_UnsafeRead();

                // Validates calling via reflection works, using Unsafe.ReadUnaligned
                test.RunReflectionScenario_UnsafeRead();

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.ReadUnaligned
                test.RunLclVarScenario_UnsafeRead();

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class ScalarBinaryOpTest__ParallelBitDepositUInt32
    {
        private struct TestStruct
        {
            public UInt32 _fld1;
            public UInt32 _fld2;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();
                var random = new Random();

                testStruct._fld1 = (uint)(random.Next(0, int.MaxValue));
                testStruct._fld2 = (uint)(random.Next(0, int.MaxValue));

                return testStruct;
            }

            public void RunStructFldScenario(ScalarBinaryOpTest__ParallelBitDepositUInt32 testClass)
            {
                var result = Bmi2.ParallelBitDeposit(_fld1, _fld2);
                testClass.ValidateResult(_fld1, _fld2, result);
            }
        }

        private static UInt32 _data1;
        private static UInt32 _data2;

        private static UInt32 _clsVar1;
        private static UInt32 _clsVar2;

        private UInt32 _fld1;
        private UInt32 _fld2;

        static ScalarBinaryOpTest__ParallelBitDepositUInt32()
        {
            var random = new Random();
            _clsVar1 = (uint)(random.Next(0, int.MaxValue));
            _clsVar2 = (uint)(random.Next(0, int.MaxValue));
        }

        public ScalarBinaryOpTest__ParallelBitDepositUInt32()
        {
            Succeeded = true;

            var random = new Random();

            _fld1 = (uint)(random.Next(0, int.MaxValue));
            _fld2 = (uint)(random.Next(0, int.MaxValue));

            _data1 = (uint)(random.Next(0, int.MaxValue));
            _data2 = (uint)(random.Next(0, int.MaxValue));
        }

        public bool IsSupported => Bmi2.IsSupported && (Environment.Is64BitProcess || ((typeof(UInt32) != typeof(long)) && (typeof(UInt32) != typeof(ulong))));

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Bmi2.ParallelBitDeposit(
                Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data1)),
                Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data2))
            );

            ValidateResult(_data1, _data2, result);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var result = typeof(Bmi2).GetMethod(nameof(Bmi2.ParallelBitDeposit), new Type[] { typeof(UInt32), typeof(UInt32) })
                                     .Invoke(null, new object[] {
                                        Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data1)),
                                        Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data2))
                                     });

            ValidateResult(_data1, _data2, (UInt32)result);
        }

        public void RunClsVarScenario()
        {
            var result = Bmi2.ParallelBitDeposit(
                _clsVar1,
                _clsVar2
            );

            ValidateResult(_clsVar1, _clsVar2, result);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var data1 = Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data1));
            var data2 = Unsafe.ReadUnaligned<UInt32>(ref Unsafe.As<UInt32, byte>(ref _data2));
            var result = Bmi2.ParallelBitDeposit(data1, data2);

            ValidateResult(data1, data2, result);
        }

        public void RunClassLclFldScenario()
        {
            var test = new ScalarBinaryOpTest__ParallelBitDepositUInt32();
            var result = Bmi2.ParallelBitDeposit(test._fld1, test._fld2);

            ValidateResult(test._fld1, test._fld2, result);
        }

        public void RunClassFldScenario()
        {
            var result = Bmi2.ParallelBitDeposit(_fld1, _fld2);
            ValidateResult(_fld1, _fld2, result);
        }

        public void RunStructLclFldScenario()
        {
            var test = TestStruct.Create();
            var result = Bmi2.ParallelBitDeposit(test._fld1, test._fld2);

            ValidateResult(test._fld1, test._fld2, result);
        }

        public void RunStructFldScenario()
        {
            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(UInt32 left, UInt32 right, UInt32 result, [CallerMemberName] string method = "")
        {
            var isUnexpectedResult = false;

            
// The validation logic defined here for Bmi2.ParallelBitDeposit and Bmi2.ParallelBitExtract is
// based on the 'Operation' pseudo-code defined for the pdep and pext instruction in the 'Intel®
// 64 and IA-32 Architectures Software Developer’s Manual; Volume 2 (2A, 2B, 2C & 2D): Instruction
// Set Reference, A-Z'

uint temp = left;
uint mask = right;
uint dest = 0;
byte m = 0, k = 0;

while (m < 32)
{
    if (((mask >> m) & 1) == 1) // Extract bit at index m of mask
    {
        dest |= (((temp >> k) & 1) << m); // Extract bit at index k of temp and insert to index m of dest
        k++;
    }
    m++;
}

isUnexpectedResult = (dest != result);


            if (isUnexpectedResult)
            {
                Console.WriteLine($"{nameof(Bmi2)}.{nameof(Bmi2.ParallelBitDeposit)}<UInt32>(UInt32, UInt32): ParallelBitDeposit failed:");
                Console.WriteLine($"    left: {left}");
                Console.WriteLine($"   right: {right}");
                Console.WriteLine($"  result: {result}");
                Console.WriteLine();
            }
        }
    }
}
