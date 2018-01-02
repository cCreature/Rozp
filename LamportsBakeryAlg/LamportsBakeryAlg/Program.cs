using System;
using System.Threading;

namespace LamportsBakeryAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSimulation();
        }

        static void RunSimulation(){
            for (int i = 1; i < 10; i++){
                var customer = new Customer(i);
                var th = new Thread(customer.run);
                th.Start();
            }
        }
    }
}
