namespace Server3
{

    public class Rand
    {
        private int _seed;
        private Random _rand = new Random();

        public int Seed => _seed;

        public void SetSeed(int seed)
        {
            _rand = new Random(seed);
        }

        public int EvalInt32()
        {
            return _rand.Next();
        }

        public float EvalFloat01()
        {
            return _rand.NextSingle();
        }

        // [min, max)
        public int RangeInt(int min, int max)
        {
            return _rand.Next(min, max);
        }

        // (min, max)
        public float RangeFloat(float min, float max)
        {
            float randF = _rand.NextSingle();
            return min + randF * (max - min);
        }
    }

}