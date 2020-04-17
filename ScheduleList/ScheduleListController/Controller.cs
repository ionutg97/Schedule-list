using ScheduleListService;
using System;

namespace ScheduleListController
{
    public class Controller
    {
        IService _service;

        public Controller()
        {
            _service = new Service();
        }
        public void sayHello()
        {
            _service.sayHello();
            Console.WriteLine("Hello from controller");
        }
    }
}
