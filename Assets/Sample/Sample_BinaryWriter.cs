using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;

// 存档
namespace JackBinary.Sample {

    [StructLayout(LayoutKind.Explicit)]
    public struct Bit32 {
        // 二进制重叠
        [FieldOffset(0)] public float f; // 4byte
        [FieldOffset(0)] public uint uf; // 4byte
    }

    public class RoleEntity {

        public int level;
        public int hp;
        public int def;

        public void Save() {

            byte[] buffer = new byte[1024];

            // 按byte顺序写入
            int index = 2;
            BinaryWriter.WriteInt(buffer, level, ref index); // level
            BinaryWriter.WriteInt(buffer, hp, ref index); // hp
            BinaryWriter.WriteInt(buffer, def, ref index); // def

            index = 0;
            BinaryWriter.WriteUshort(buffer, (ushort)(index - 2), ref index); // length

            File.WriteAllBytes(System.Environment.CurrentDirectory + "/role.sav", buffer);

        }

        public void Load() {

            byte[] buffer = File.ReadAllBytes(System.Environment.CurrentDirectory + "/role.sav");

            // 按byte顺序读取
            int index = 0;
            ushort length = BinaryReader.ReadUshort(buffer, ref index);

            level = BinaryReader.ReadInt(buffer, ref index);
            hp = BinaryReader.ReadInt(buffer, ref index);
            def = BinaryReader.ReadInt(buffer, ref index);

        }

        public void Log() {
            Debug.Log("level: " + level + ", hp: " + hp + ", def: " + def);
        }

    }

    public class Sample_BinaryWriter : MonoBehaviour {

        void Awake() {

            // Bit32 b32 = new Bit32();
            // b32.us1 = 1;
            // b32.us2 = 2; // 131073
            // Debug.Log("b32 " + b32.uf);

            byte[] buffer = new byte[1024];
            int offset = 0; 

            Bit32 b32 = new Bit32();
            float f = 1.1f;
            b32.f = f;

            BinaryWriter.WriteUint(buffer, b32.uf, ref offset);

            // Read
            offset = 0;
            uint fr = BinaryReader.ReadUint(buffer, ref offset);
            Bit32 r32 = new Bit32();
            r32.uf = fr;
            Debug.Log($"f: {f}, f2: {r32.f}");

            Debug.Log($"Hello \0 World  \" ");

            // float a = 1.1f;
            // int value = (int)a; // 不能这样转, 因为它会十进制转换

            // 有符号与无符号的同位同型数(32bit -> 32bit)转换，它进行二进制转换
            // 二进制不变
            // 十进制变了
            // int -> uint
            int a = -1; // 111111111111111111
            uint ua = (uint)a; // 111111111111111111

            // 同符号转换, 不同的位数转换(32bit -> 16bit)，它进行十进制转换
            // 二进制变
            // 十进制不变
            // float(32bit) -> double(64bit)
            int a2 = -1; // -1, 1111 三十二个1
            short sa2 = (short)a2; // -1, 1111 十六个1

            RoleEntity role = new RoleEntity();
            role.Load();
            role.Log();
            // role.level = 99;
            // role.hp = 75;
            // role.def = 10;
            // role.Save();
            // role.Log();
        }

        // 序列化
        // int => byte[]
        // bool => byte[]

        // 反序列化
        // byte[] => int
        // byte[] => bool

        // 知识点：位运算
        // <<   左移
        // >>   右移
        // |    或

        void Write() {

            // byte byte, byte....
            // 长度        数据
            // 0    1     2
            // 1byte
            byte[] buffer = new byte[1024]; // 1024
            int index = 0;

            ushort length = 0; // 2byte 0~65535

            // 0111 1111, 1111 1000, 1000 1000, 0011 1100
            // 1111 1111  1111 1111  1111 1111  1111 1111
            int money = 2146994236; // 4byte
                                    // uint ua = 2146994236; // 4byte
                                    // 0 0 0 100
                                    // buffer[0] = 0011 1100; 60
                                    // buffer[1] = 1000 1000; 136
                                    // buffer[2] = 1111 1000; 248
                                    // buffer[3] = 0111 1111; 127
                                    // index = 4;

            // 写入
            index = 2;
            BinaryWriter.WriteInt(buffer, money, ref index);

            length = (ushort)(index - 2); // 6-2 = 4

            index = 0;
            BinaryWriter.WriteUshort(buffer, length, ref index);

            for (int i = 0; i < length + 2; i += 1) {
                Debug.Log(buffer[i]);
            }

            File.WriteAllBytes(System.Environment.CurrentDirectory + "/test.sav", buffer);

        }

        void Start() {
            // Write();
            Read();
        }

        void Read() {

            byte[] buffer = File.ReadAllBytes(System.Environment.CurrentDirectory + "/test.sav");

            int index = 0;

            ushort len = BinaryReader.ReadUshort(buffer, ref index);

            for (int i = 0; i < len + 2; i += 1) {
                // Debug.Log(buffer[i]);
            }

            int money = BinaryReader.ReadInt(buffer, ref index);
            if (index >= len + 2) {
                Debug.Log("读取结束");
            }

            Debug.Log("R " + money);

            // // 读取
            // // 0 0 0 0
            // index = 0;
            // int ra = BinaryReader.ReadInt(buffer, ref index);
            // // 0 0 0 100

            // Debug.Log("R: " + ra);

        }

    }

}
