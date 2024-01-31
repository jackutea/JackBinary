# JackBinary
二进制序列化库/反序列化

```
struct Value {
    int a = 1;
    int b = 2;
}

Value v = new Value();
v.b = 9;
v.a = 5;

// ↓
// 顺序化
byte[] stream; // 2 int = 8byte = 64bit

```