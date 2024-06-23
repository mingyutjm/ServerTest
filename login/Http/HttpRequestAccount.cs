using Server3;

namespace login.Http
{

    public class HttpRequestAccount : ThreadObject
    {
        public HttpRequestAccount(string account, string password)
        {
        }

        public override bool Init()
        {
            return true;
        }

        public override void RegisterMsgFunction()
        {
        }

        public override void Tick()
        {
        }
        
        
    }

}