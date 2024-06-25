using System.Security.Cryptography;
using MemoryPack;

namespace Server3.Message
{

    [MemoryPackable]
    public partial struct TestMsg
    {
        public string msg;
        public int index;
    }

    [MemoryPackable]
    public partial struct AccountCheckReq
    {
        public string account;
        public string password;
    }

    [MemoryPackable]
    public partial struct AccountCheckToHttpRes
    {
        public string account;
        public int returnCode;
    }

    [MemoryPackable]
    public partial struct AccountCheckRes
    {
        public enum ReturnCode
        {
            Ok = 0,
            Unknown,
            NotFoundAccount,
            PasswordWrong,
            Logging,
            Timeout,
        }

        public int returnCode;
    }

}