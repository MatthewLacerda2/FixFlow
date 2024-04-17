namespace Server.Seeder.WaveFunctions
{
    public class WaveFunctions
    {
        public static float Lerp(int min, int max, int seed)
        {
            float t = (float)seed / int.MaxValue;
            return min + (max - min) * t;
        }

        public static float Slerp(int min, int max, int seed)
        {
            float t = (float)Math.Sin(seed) * 0.5f + 0.5f;
            return min + (max - min) * t;
        }
    }
}
