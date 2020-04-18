using Models;
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
            _persistance = Persistance.getInstanceOfMySqlConnection();
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the persistance layer.
        ///  Get days list.
        /// </summary>
        public List<Day> GetDays()
        {
            return _persistance.GetDays();
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the persistance layer.
        ///  Get task list.
        /// </summary>
        public List<Task> GetTasks()
        {
            return _persistance.GetTasks();
        }

        public void SayHello()
        {
            _persistance.SayHello();
            Console.WriteLine("Hello from service");
        }
    }
}
