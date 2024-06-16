namespace Server3.Message
{

    public enum MsgId
    {
        None,
        TestMsg,
        C2L_AccountCheckReq, // Client to login
        C2L_AccountCheckRes, // Client to login
        AccountCheckToHttpRes,
        NetworkDisconnectToNet,
    }

}