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

        public List<Task> GetTasksBetweenDates(string start, string end)
        {
            return _persistance.GetTasksBetweenDates(start, end);
        }

        /// <summary>
        /// Stan Dragos
        /// Get all tasks between those 2 dates, calling function from the persistance layer
        /// returns an int that represents the percent of done tasks from db
        /// </summary>
        public decimal GetFinishedTasksPercent(string start, string end)
        {
            List < Task > allTasksBetweenDays = _persistance.GetTasksBetweenDates(start, end);
            decimal count = allTasksBetweenDays.Count;
            decimal done = 0;
            foreach(var x in allTasksBetweenDays)
            {
                if (x.Status == "done")
                {
                    done++;
                }
            }
            decimal final = (int)(done / count * 100);
            return final;
        }

        /// <summary>
        /// Stan Dragos
        /// Get all tasks between those 2 dates, calling function from the persistance layer
        /// Returns an int that represents the efficiency of user calculated under a personalized formula 
        /// </summary>
        public decimal GetEffiencyOfTasksPercent(string start, string end)
        {
            List<Task> allTasksBetweenDays = _persistance.GetTasksBetweenDates(start, end);
            decimal count = allTasksBetweenDays.Count;
            decimal done_tasks_prio1 = 0;
            decimal done_tasks_prio2 = 0;
            decimal done_tasks_prio3 = 0;
            foreach (var x in allTasksBetweenDays)
            {
                if (x.Priority == 1 && x.Status=="done")
                {
                    done_tasks_prio1++;
                }
                else if(x.Priority == 2 && x.Status == "done")
                {
                    done_tasks_prio2++;
                }
                else if (x.Priority == 3 && x.Status == "done")
                {
                    done_tasks_prio3++;
                }
            }
            decimal final = (int)((done_tasks_prio1 * 150 + done_tasks_prio2 * 100 + done_tasks_prio3 * 50) / count);
            return final;
        }
    }
}
