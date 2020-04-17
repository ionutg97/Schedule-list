using ScheduleListPersistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleListService
{
    public class Service : IService
    {
        private IPersistance _persistance;

        public Service()
        {
            _persistance = new Persistance();
        }
        public void sayHello()
        {
            _persistance.SayHello();
            Console.WriteLine("Hello from service");
        }
    }
}
