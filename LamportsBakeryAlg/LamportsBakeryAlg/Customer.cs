using System;


namespace LamportsBakeryAlg
{
    public class Customer
    {
        int _Id;
        static bool[] _ChoosingChair = new bool[10];
        static int[] _TicketChair = new int[10];
        static bool[] _ChoosingBarber = new bool[10];
        static int[] _TicketBarber = new int[10];


        public Customer(int id){
            _Id = id;
        }

        public void run(){
            FindAChair();
            FindABarber();
        }

        void FindAChair()
        {
            _ChoosingChair[_Id] = true;
            _TicketChair[_Id] = _Id + 1;
            _ChoosingChair[_Id] = false;

            for (int i = 0; i < 10; i++)
            {
                if (i == _Id)
                    continue;
                while (_ChoosingChair[i]) { DoNothing(); }
                while (_TicketChair[i] != 0 && _TicketChair[i] < _TicketChair[_Id]) { DoNothing(); }
                if (_TicketChair[i] == _TicketChair[_Id] && i < _Id)
                {
                    while (_TicketChair[i] != 0) { DoNothing(); }
                }
            }

            // critical section
            while (!Chair.IsAvailable()) { DoNothing(); }
            Chair.Occupy();
            Console.WriteLine(_Id + " found an open chair, there are " + Chair.Available() + " open chairs left.");
            _TicketChair[_Id] = 0;
            // end
        }


        void FindABarber()
        {
            _ChoosingBarber[_Id] = true;
            _TicketBarber[_Id] = _Id + 1;
            _ChoosingBarber[_Id] = false;

            for (int i = 0; i < 10; i++)
            {
                if (i == _Id)
                    continue;
                while (_ChoosingBarber[i]) { DoNothing(); }
                while (_TicketBarber[i] != 0 && _TicketBarber[i] < _TicketBarber[_Id]) { DoNothing(); }
                if (_TicketBarber[i] == _TicketBarber[_Id] && i < _Id)
                {
                    while (_TicketBarber[i] != 0) { DoNothing(); }
                }
            }

            //critical section
            while (!Barber.IsAvailable()) { DoNothing(); }
            Barber.GiveHaircut();
            Console.WriteLine(_Id + " now has a barber, there are  " + Barber.Available() + " available barbers.");
            leaveBarberShop();
            _TicketBarber[_Id] = 0;
            //end
        }


        void leaveBarberShop()
        {
            Chair.Leave();
            Barber.Leave();
            Console.WriteLine(_Id + " has left the barber shop.");
        }


        void DoNothing(){
            Console.Write("");
        }
    }
}
