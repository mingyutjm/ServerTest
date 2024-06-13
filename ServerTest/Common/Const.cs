namespace Server3;

public static class Const
{
    public const int AdditionalSize = 1024;        // << 10 bit, 1024
    public const int MaxBufferSize = 1024 * 1024;  // 1M
    public const int DefaultPacketBufferSize = 16; // 10 byte
    public const int DefaultSendBufferSize = 10;
    public const int DefaultRecvBufferSize = 10;
}