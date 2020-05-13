/**************************************************************************
 *                                                                        *
 *  File:        Controller.cs                                            *
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
        /// <param name="task"></param>
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
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            _service.DeleteTask(task);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Update a task by title, subtitle and description
        /// </summary>
        /// <param name="task"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task UpdateTaskDetails(Task task, string title, string subtitle, string description)
        {
            return _service.UpdateTaskDetails(task, title, subtitle, description);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Update a task by status.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task UpdateTaskStatus(Task task, string status)
        {
            return _service.UpdateTaskStatus(task, status);
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Call the method from the service layer.
        ///  Get a list of tasks for a given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Task> GetTasksForAGivenDate(string date)
        {
            return _service.GetTasksForAGivenDate(date);
        }

        /// <summary>
        /// Denis Ciobanu
        /// Update Task with new valus.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="time"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority)
        {
            return _service.UpdateTaskFowView(task, time, title, subtitle, status, priority);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Get finished tasks percent for a certain day interval.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public decimal GetFinishedTasksPercent(string start, string end)
        {
            return _service.GetFinishedTasksPercent(start, end);
        }

        /// <summary>
        ///  Stan Dragos
        ///  Call the method from the service layer.
        ///  Get efficiency percent for a certain day interval.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public decimal GetEffiencyOfTasksPercent(string start, string end)
        {
            return _service.GetEffiencyOfTasksPercent(start, end);
        }
    }
}
