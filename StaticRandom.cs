using System;
// ReSharper disable UnusedMember.Global

namespace Util
{
    // static, thread-safe wrapper for Random class
    public static class StaticRandom
    {
        private static readonly Random R = new Random();
        private static readonly object ObjLock = new object();

        public static int Next()
        {
            // //Contract.Ensures(// Contract.Result<int>() >= 0);

            lock (ObjLock)
            {
                return R.Next();
            }
        }

        public static int Next(int maxValue)
        {
            // // Contract.Requires(maxValue >= 0);
            // //Contract.Ensures(// Contract.Result<int>() >= 0);
            // //Contract.Ensures(// Contract.Result<int>() < maxValue || maxValue == 0 && // Contract.Result<int>() == 0);
            
            lock (ObjLock)
            {
                return R.Next(maxValue);
            }
        }

        public static int Next(int minValue, int maxValue)
        {
            // // Contract.Requires(minValue <= maxValue);
            // //Contract.Ensures(// Contract.Result<int>() >= minValue);
            // //Contract.Ensures(// Contract.Result<int>() < maxValue ||
                             //maxValue == minValue && // Contract.Result<int>() == minValue);
            lock (ObjLock)
            {
                return R.Next(minValue, maxValue);
            }
        }

        public static void NextBytes(byte[] buffer)
        {
            // // Contract.Requires(buffer != null);

            lock (ObjLock)
            {
                R.NextBytes(buffer);
            }
        }

        public static double NextDouble()
        {
            lock (ObjLock)
            {
                return R.NextDouble();
            }
        }
    }
}
