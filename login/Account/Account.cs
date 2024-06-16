using System.Net.Sockets;
using MemoryPack;
using Server3;
using Server3.Message;

namespace login
{

    public class Account : ThreadObject
    {
        private PlayerMgr _playerMgr;

        public override bool Init()
        {
            return true;
        }

        public override void Dispose()
        {
        }

        public override void RegisterMsgFunction()
        {
            RegisterFunction((int)MsgId.C2L_AccountCheckReq, Handle_AccountCheckReq);
            RegisterFunction((int)MsgId.AccountCheckToHttpRes, Handle_AccountCheckToHttpRes);
        }

        public override void Tick()
        {
        }

        //
        private void Handle_AccountCheckReq(Packet packet)
        {
            C2L_AccountCheckReq? sourceData = packet.Deserialize<C2L_AccountCheckReq>();
            Socket fromSocket = packet.Socket;

            Log.Info($"account check account: {sourceData!.account}");
            // 检查是否已经登录过
            if (_playerMgr.TryQueryPlayer(sourceData.account, out Player player))
            {
                // 已经登录过, 返回logging
                C2L_AccountCheckRes responseData = new C2L_AccountCheckRes();
                responseData.returnCode = (int)C2L_AccountCheckRes.ReturnCode.Logging;
                var responsePacket = Packet.Create((int)MsgId.C2L_AccountCheckRes, fromSocket);
                responsePacket.SerializeToBuffer(responseData);
                SendPacket(responsePacket);
                // 关闭网络
                var disconnectPacket = Packet.Create((int)MsgId.NetworkDisconnectToNet, fromSocket);
                DispatchPacket(disconnectPacket);
                return;
            }

            _playerMgr.AddPlayer(fromSocket, sourceData.account, sourceData.password);
            // Http验证账号
            
        }

        private void Handle_AccountCheckToHttpRes(Packet packet)
        {
        }
    }

}