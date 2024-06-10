namespace Server3
{

    public static class ByteToString
    {
        public static string Do(byte[] data)
        {
            return BitConverter.ToString(data);
        }

        public static string ToStr(this byte[] data)
        {
            return BitConverter.ToString(data);
        }
    }

}