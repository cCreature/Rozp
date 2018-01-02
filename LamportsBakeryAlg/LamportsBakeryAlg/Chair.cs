using System;
namespace LamportsBakeryAlg
{
    public class Chair
    {
        static int _OccupiedChairs = 0;

        public static bool IsAvailable()
        {
            return _OccupiedChairs < 5;
        }

        public static int Available()
        {
            return 5 - _OccupiedChairs;
        }

        public static void Occupy()
        {
            _OccupiedChairs++;
        }

        public static void Leave()
        {
            _OccupiedChairs--;
        }
    }
}
