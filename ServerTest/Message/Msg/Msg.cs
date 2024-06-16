using MemoryPack;

namespace Server3.Message
{

    [MemoryPackable]
    public partial class TestMsg
    {
        public string msg;
        public int index;
    }

}