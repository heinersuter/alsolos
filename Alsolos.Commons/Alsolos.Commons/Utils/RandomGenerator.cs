namespace Alsolos.Commons.Utils
{
    using System;
    using System.Text;

    public static class RandomGenerator
    {
        private static Random _random = new Random((int)DateTime.Now.Ticks);
        
        public static char GetUppercaseChar()
        {
            var decNumber = _random.Next(0, 26);
            var character = (char)('A' + decNumber);
            return character;
        }
        
        public static string GetUppercaseString(int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(GetUppercaseChar());
            }
            return sb.ToString();
        }
    }
}
