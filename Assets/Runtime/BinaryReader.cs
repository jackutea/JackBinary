namespace JackBinary {

    public static class BinaryReader {

        public static int ReadInt(byte[] buffer, ref int index) {

            // 0000 0000, 0000 0000, 0000 0000, 0000 0000
            //                                  ↑↑↑↑↑↑↑↑↑

            int value = 0;

            // 0000 0000, 0000 0000, 0000 0000, 1100 1000
            //                                  ↑↑↑↑↑↑↑↑↑
            value = value | (buffer[index] << 0);
            index++;

            // 0000 0000, 0000 0000, 0000 0000, 1100 1000
            //                       ↑↑↑↑↑↑↑↑↑
            // 0000 0000, 0000 0000, 1100 1000, 0000 0000
            value = value | (buffer[index] << 8);
            index++;

            value = value | (buffer[index] << 16);
            index++;

            value = value | (buffer[index] << 24);
            index++;

            return value;
        }

        public static ushort ReadUshort(byte[] buffer, ref int index) {

            ushort value = 0;

            value = (ushort)(value | (buffer[index] << 0));
            index++;

            value = (ushort)(value | (buffer[index] << 8));
            index++;

            return value;
        }
    }
}