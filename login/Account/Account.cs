using System.Net.Sockets;
using login.Http;
using MemoryPack;
using Server3;
using Server3.Message;

namespace login
{

    public class Account : ThreadObject
    {
        private LoginObjMgr _loginObjMgr;

        public override bool Init()
        {
            return true;
        }

        public override void Dispose()
        {
        }

        public override void RegisterMsgFunction()
        {
            // RegisterFunction((int)MsgId.C2L_AccountCheckReq, HandleMsg_AccountCheckReq);
            // RegisterFunction((int)MsgId.AccountCheckToHttpRes, HandleMsg_AccountCheckToHttpRes);

            var msgCallbacks = new MessageCallbackFunction();
            AttachCallbackHandler(msgCallbacks);

            msgCallbacks.RegisterFunction((int)MsgId.AccountCheckReq, HandleMsg_AccountCheckReq);
            msgCallbacks.RegisterFunction((int)MsgId.AccountCheckToHttpRes, HandleMsg_AccountCheckToHttpRes);
        }

        public override void Tick()
        {
        }

        // 处理新的登录消息
        private void HandleMsg_AccountCheckReq(Packet packet)
        {
            AccountCheckReq sourceData = packet.Deserialize<AccountCheckReq>();
            Socket fromSocket = packet.Socket;

            Log.Info($"account check account: {sourceData.account}");
            // 检查是否已经登录过
            if (_loginObjMgr.TryQueryPlayer(sourceData.account, out _))
            {
                // 已经登录过, 返回logging
                AccountCheckRes responseData = new AccountCheckRes();
                responseData.returnCode = (int)AccountCheckRes.ReturnCode.Logging;
                var responsePacket = Packet.Create((int)MsgId.AccountCheckRes, fromSocket);
                responsePacket.SerializeToBuffer(responseData);
                SendPacket(responsePacket);

                // 关闭网络
                var disconnectPacket = Packet.Create((int)MsgId.NetworkDisconnectToNet, fromSocket);
                DispatchPacket(disconnectPacket);
                return;
            }

            // 更新信息
            _loginObjMgr.AddPlayer(fromSocket, sourceData.account, sourceData.password);
            // Http验证账号
            HttpRequestAccount httpReq = new HttpRequestAccount(sourceData.account, sourceData.password);
            ThreadMgr.Instance.AddObjToThread(httpReq);
        }

        // 处理请求Http登录的Res消息
        private void HandleMsg_AccountCheckToHttpRes(Packet packet)
        {
            var res = packet.Deserialize<AccountCheckToHttpRes>();
            if (_loginObjMgr.TryQueryPlayer(res.account, out LoginObj? player))
            {
                AccountCheckRes responseData = new AccountCheckRes();
                responseData.returnCode = (int)AccountCheckRes.ReturnCode.Ok;
                var responsePacket = Packet.Create((int)MsgId.AccountCheckRes, player!.Socket);
                responsePacket.SerializeToBuffer(responseData);
                SendPacket(responsePacket);
            }
            else
            {
                Log.Error($"Can't find player. account {res.account}");
            }
        }
    }

}