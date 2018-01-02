using System;
namespace LamportsBakeryAlg
{
    public class Barber
    {
        static int _OccupiedBarbers = 0;

        public static bool IsAvailable()
        {
            return _OccupiedBarbers < 3;
        }

        public static int Available()
        {
            return 3 - _OccupiedBarbers;
        }

        public static void GiveHaircut()
        {
            _OccupiedBarbers++;
        }

        public static void Leave()
        {
            _OccupiedBarbers--;
        }

    }
}
