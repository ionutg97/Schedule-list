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
        ///  Create a new Task;
        /// </summary>
        public void CreateNewTask(Task task)
        {
            _persistance.CreateNewTask(task);
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

            //data exista deja -> ar trebui sa faca insert direct la task
            Task task = CreateNewTask("09:50:05", "y", "y", "y", "new", 2);
            CreateNewTask(task);
        }

        private Task CreateNewTask(string time, string title, string subtitle, string description, string status, int priority)
        {
            Task task = new Task();
            task.Time = time;
            task.Title = title;
            task.Subtitle = subtitle;
            task.Description = description;
            task.Status = status;
            task.Priority = priority;
            return task;
        }
        public int GetCompletedTaskNumbers()
        {
            return _persistance.GetCompletedTaskNumbers();
        }

        public int GetInProgressTaskNumbers()
        {
            return _persistance.GetInProgressTaskNumbers();
        }

        public void DeleteTask(Task task)
        {
            _persistance.DeleteTask(task);
        }

        public Task UpdateTaskDetails(Task task, string title, string subtitle, string description)
        {
            return _persistance.UpdateTaskDetails(task, title, subtitle, description);
        }

        public Task UpdateTaskStatus(Task task, string status)
        {
            return _persistance.UpdateTaskStatus(task, status);
        }

        public List<Task> GetTasksForAGivenDate(string date)
        {
            return _persistance.GetTasksForAGivenDate(date);
        }

        public Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority)
        {
            return _persistance.UpdateTaskFowView(task, time, title, subtitle, status, priority);
        }

        public decimal GetFinishedTasksPercent(string start, string end)
        {
            return _persistance.GetFinishedTasksPercent(start, end);
        }

        public decimal GetEffiencyOfTasksPercent(string start, string end)
        {
            return _persistance.GetEffiencyOfTasksPercent(start, end);
        }
    }
}
