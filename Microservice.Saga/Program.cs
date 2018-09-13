using System;

namespace Microservice.Saga
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Saga";

            var saga = new OrderSaga();

            Console.WriteLine("Hello World!");
        }
    }
}
