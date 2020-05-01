using Models;
using ScheduleListService;
using System;
using System.Collections.Generic;

namespace ScheduleListController
{
    public class Controller
    {
        IService _service;

        public Controller()
        {
            _service = new Service();
        }

        /// <summary>
        ///  Galan Ionut Andrei
        ///  This method was created to check if everything works well.
        ///  It connect view layer wiht controller layer.
        /// </summary>
        public void SayHello()
        {
            _service.SayHello();
            Console.WriteLine("Hello from controller");

            DisplayDays();
            DisplayTasks();
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Get days list and display it.
        /// </summary>
        public void DisplayDays()
        {
           List<Day> days = _service.GetDays();

            foreach(var day in days)
            {
                Console.WriteLine("--->  " + day.Date);
            }
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Get tasks list and display it.
        /// </summary>
        public void DisplayTasks()
        {
            List<Task> tasks = _service.GetTasks();

            foreach (var task in tasks)
            {
                Console.WriteLine("--->  " + task.Time + " " + task.Title + " "+ task.Subtitle 
                    + " " + task.Description + " " + task.Status + " " + task.Priority);
            }
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Create new Task.
        /// </summary>
        public void CreateNewTask(Task task)
        {
            _service.CreateNewTask(task);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Get number of "completed" tasks.
        /// </summary>
        public int GetCompletedTaskNumbers()
        {
            return _service.GetCompletedTaskNumbers();
        }
        

        /// <summary>
        ///  Stan Dragos
        ///  Get number of "in progress" tasks.
        /// </summary>
        public int GetInProgressTaskNumbers()
        {
            return _service.GetInProgressTaskNumbers();
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Delete a task.
        /// </summary>
        public void DeleteTask(Task task)
        {
            _service.DeleteTask(task);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Update a task by title, subtitle and description.
        /// </summary>
        public Task UpdateTaskDetails(Task task, string title, string subtitle, string description)
        {
            return _service.UpdateTaskDetails(task, title, subtitle, description);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Update a task by status.
        /// </summary>
        public Task UpdateTaskStatus(Task task, string status)
        {
            return _service.UpdateTaskStatus(task, status);
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Get a list of tasks for a given date.
        /// </summary>
        public List<Task> GetTasksForAGivenDate(string date)
        {
            return _service.GetTasksForAGivenDate(date);
        }

        public Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority)
        {
            return _service.UpdateTaskFowView(task, time, title, subtitle, status, priority);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Ges finished tasks percent for a certain day interval.
        /// </summary>
        public decimal GetFinishedTasksPercent(string start, string end)
        {
            return _service.GetFinishedTasksPercent(start, end);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Get efficiency percent for a certain day interval.
        /// </summary>
        public decimal GetEffiencyOfTasksPercent(string start, string end)
        {
            return _service.GetEffiencyOfTasksPercent(start, end);
        }
    }
}
