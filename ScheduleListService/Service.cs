/**************************************************************************
 *                                                                        *
 *  File:        Service.cs                                               *
 *  Copyright:   (c) 2019-2020                                            *
 *                Stan Dragos                                             *
 *                Halip Vasile Emanuel                                    *
 *                Ciobanu Denis Marian                                    *
 *                Galan Ionut Andrei                                      *
 *  Description: Task Shedule - Windows Form Program                      *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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
        ///  Create a new Task.
        /// </summary>
        /// <param name="task"></param>
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

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  For test purpose only.
        /// </summary>
        public void SayHello()
        {
            _persistance.SayHello();
            Console.WriteLine("Hello from service");

            //data exista deja -> ar trebui sa faca insert direct la task
            Task task = CreateNewTask("09:50:05", "y", "y", "y", "new", 2);
            CreateNewTask(task);
        }

        /// <summary>
        /// Stan Dragos
        /// Get number of completed tasks.
        /// </summary>
        /// <returns>Number of completed Tasks.</returns>
        public int GetCompletedTaskNumbers()
        {
            return _persistance.GetCompletedTaskNumbers();
        }

        /// <summary>
        /// Stan Dragos
        /// Get number of in progress tasks.
        /// </summary>
        /// <returns>Number of in progress tasks</returns>
        public int GetInProgressTaskNumbers()
        {
            return _persistance.GetInProgressTaskNumbers();
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the persistance layer.
        ///  Delete a task from db.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            _persistance.DeleteTask(task);
        }

        /// <summary>
        /// Stan Dragos
        /// Update Task details for a given task members. Call the method from persistance layer.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <returns>Updated Task</returns>

        public Task UpdateTaskDetails(Task task, string title, string subtitle, string description)
        {
            return _persistance.UpdateTaskDetails(task, title, subtitle, description);
        }

        /// <summary>
        /// Stan Dragos
        /// Update Task status for a given status. Call the method from persistance layer.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <returns>Updated Task</returns>
        public Task UpdateTaskStatus(Task task, string status)
        {
            return _persistance.UpdateTaskStatus(task, status);
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        //   Call the method from the persistance layer.
        ///  Get a list of tasks for a given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A tasks List</returns>
        public List<Task> GetTasksForAGivenDate(string date)
        {
            return _persistance.GetTasksForAGivenDate(date);
        }

        /// <summary>
        /// Ciobanu Denis Marian
        /// Update a task for a given task members.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="time"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns>Updated Task</returns>
        public Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority)
        {
            return _persistance.UpdateTaskFowView(task, time, title, subtitle, status, priority);
        }

        /// <summary>
        /// Stan Dragos
        /// Get all tasks between those 2 dates, calling function from the persistance layer.
        /// returns an int that represents the percent of done tasks from db.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Percent of finished tasks</returns>
        public decimal GetFinishedTasksPercent(string start, string end)
        {
            List<Task> allTasksBetweenDays = GetTasksBetweenTwoDates(start, end);
            decimal result = CalcualteFinishedTaskPercent(allTasksBetweenDays);
            return result;
        }

        /// <summary>
        /// Stan Dragos
        /// returns an int that represents the percent of done tasks from db.
        /// </summary>
        /// <param name="allTasksBetweenDays"></param>
        /// <returns>Percent of finished tasks</returns>
        public decimal CalcualteFinishedTaskPercent(List<Task> allTasksBetweenDays)
        {
            decimal count = allTasksBetweenDays.Count;
            decimal done = 0;
            foreach (var x in allTasksBetweenDays)
            {
                if (x.Status == "done")
                {
                    done++;
                }
            }
            if (count == 0)
                return 0;
            decimal result = (int)(done / count * 100);
            return result;
        }

        /// <summary>
        /// Stan Dragos
        /// Get all tasks between those 2 dates, calling function from the persistance layer
        /// Returns an int that represents the efficiency of user calculated under a personalized formula 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Percent of task effiency for a given period</returns>
        public decimal GetEffiencyOfTasksPercent(string start, string end)
        {
            List<Task> allTasksBetweenDays = GetTasksBetweenTwoDates(start, end);
            decimal result = CalculateEffiencyOfTasksPercent(allTasksBetweenDays);
            return result;
        }


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Create a task object for internal usage.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns>Created task</returns>
        public Task CreateNewTask(string time, string title, string subtitle, string description, string status, int priority)
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

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Convert a string to Datetime -> we need only the date in this case
        /// </summary>
        /// <param name="stringDate"></param>
        /// <returns>New DateTime object</returns>        
        public DateTime ConvertStringToDate(string stringDate)
        {
            // formatul este asa =>     1/5/2020 12:00:00 AM
            string[] peaches = stringDate.Split(' ');  /// fac split in functie de spatiu 
            string[] date = peaches[0].Split('/');  /// apoi fac split in functie de / ca sa extrag data

            int day = Int32.Parse(date[1]);
            int month = Int32.Parse(date[0]);
            int year = Int32.Parse(date[2]);

            DateTime dt = new DateTime(year, month, day, 0, 0, 0);
            return dt;
        }

        /// <summary>
        /// Stan Dragos
        /// Returns an decimal that represents the efficiency of user calculated under a personalized formula.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>Percent of tasks effiency</returns>
        public decimal CalculateEffiencyOfTasksPercent(List<Task> tasks)
        {
            decimal count = tasks.Count;
            decimal done_tasks_prio1 = 0;
            decimal done_tasks_prio2 = 0;
            decimal done_tasks_prio3 = 0;
            foreach (var x in tasks)
            {
                if (x.Priority == 1 && x.Status == "done")
                {
                    done_tasks_prio1++;
                }
                else if (x.Priority == 2 && x.Status == "done")
                {
                    done_tasks_prio2++;
                }
                else if (x.Priority == 3 && x.Status == "done")
                {
                    done_tasks_prio3++;
                }
            }
            if (count == 0)
                return 0;
            decimal final = (int)((done_tasks_prio1 * 100 + done_tasks_prio2 * 75 + done_tasks_prio3 * 50) / count);
            return final;
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Get a list of task for a given start date and end date.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>List of tasks between a given date</returns>
        private List<Task> GetTasksBetweenTwoDates(String start, String end)
        {
            return _persistance.GetTasksBetweenDates(start, end);
        }
    }
}
