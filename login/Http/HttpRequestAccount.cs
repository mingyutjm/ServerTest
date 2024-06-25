using System.Net.NetworkInformation;
using System.Text.Json;
using Server3;
using Server3.Message;

namespace login.Http
{

    public class HttpRequestAccount : ThreadObject
    {
        private string _url;
        private string _account;
        private string _password;

        public HttpRequestAccount(string account, string password)
        {
            _url = "10.10.0.120/member_login_t.php";
            _account = account;
            _password = password;
        }

        public override bool Init()
        {
            SendHttp();
            return true;
        }

        public override void RegisterMsgFunction()
        {
        }

        public override void Tick()
        {
        }

        private async void SendHttp()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var body = new { account = _account, password = _password };
                    StringContent content = new StringContent(JsonSerializer.Serialize(body));
                    HttpResponseMessage response = await httpClient.PostAsync(_url, content);
                    // response.EnsureSuccessStatusCode();
                    // response.StatusCode
                    // string responseBody = await response.Content.ReadAsStringAsync();
                    ProcessHttpResMsg(response);
                }
                catch (HttpRequestException e)
                {
                    Log.Error($"Message: {e.Message}");
                }
            }
        }

        private void ProcessHttpResMsg(HttpResponseMessage response)
        {
            AccountCheckToHttpRes packetData = new AccountCheckToHttpRes();
            packetData.returnCode = (int)response.StatusCode;
            packetData.account = _account;

            Packet packet = Packet.Create((int)MsgId.AccountCheckToHttpRes, null);
            packet.SerializeToBuffer(packetData);
            DispatchPacket(packet);
        }
    }

}