using System.Security.Cryptography;
using MemoryPack;

namespace Server3.Message
{

    [MemoryPackable]
    public partial class TestMsg
    {
        public string msg;
        public int index;
    }

    [MemoryPackable]
    public partial class C2L_AccountCheckReq
    {
        public string account;
        public string password;
    }

    [MemoryPackable]
    public partial class AccountCheckToHttpRes
    {
        public int returnCode;
        public string account;
    }

    [MemoryPackable]
    public partial class C2L_AccountCheckRes
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