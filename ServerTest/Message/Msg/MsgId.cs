namespace Server3.Message
{

    public enum MsgId
    {
        None,
        TestMsg,
        AccountCheckReq, // Client to login
        AccountCheckRes, // Client to login
        AccountCheckToHttpRes,
        NetworkDisconnectToNet,
    }

}