using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 存档
namespace JackBinary.Sample {

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
