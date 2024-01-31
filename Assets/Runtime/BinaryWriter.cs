using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JackBinary {

    // 写入
    public static class BinaryWriter {

        // sbyte sbyte[] byte byte[]
        // bool bool[]
        // char char[]
        // short short[] ushort ushort[]
        // int int[] uint uint[]
        // long long[] ulong ulong[]
        // float float[] double double[]
        // decimal decimal[]
        // string string[]

        // int a; int b
        // byte a0, a1, a2, a3; byte b0, b1, b2, b3
        //      0    1   2   3        ↑
        public static void WriteInt(byte[] buffer, int value, ref int index) {

            // 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
            // ↑
            // 1 
            buffer[index] = (byte)(value >> 0);
            index++;

            // 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
            //   ↑
            //   2
            buffer[index] = (byte)(value >> 8);
            index++;

            buffer[index] = (byte)(value >> 16);
            index++;

            buffer[index] = (byte)(value >> 24);
            index++;

        }

        public static void WriteUshort(byte[] buffer, ushort value, ref int index) {

            buffer[index] = (byte)(value >> 0);
            index++;

            buffer[index] = (byte)(value >> 8);
            index++;

        }

    }

}
