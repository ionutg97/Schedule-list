/**************************************************************************
 *                                                                        *
 *  File:        IService.cs                                              *
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
using System;
using System.Collections.Generic;

namespace ScheduleListService
{
    public interface IService
    {
        void SayHello();
        List<Day> GetDays();
        List<Task> GetTasks();
        List<Task> GetTasksForAGivenDate(string date);

        void CreateNewTask(Task task);

        int GetCompletedTaskNumbers();

        int GetInProgressTaskNumbers();

        void DeleteTask(Task task);
        Task UpdateTaskDetails(Task task, string title, string subtitle, string description);
        Task UpdateTaskStatus(Task task, string status);

        Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority);
      
        decimal GetFinishedTasksPercent(string start, string end);
        decimal GetEffiencyOfTasksPercent(string start, string end);

        DateTime ConvertStringToDate(string stringDate);

        Task CreateNewTask(string time, string title, string subtitle, string description, string status, int priority);

        decimal CalculateEffiencyOfTasksPercent(List<Task> tasks);

        decimal CalcualteFinishedTaskPercent(List<Task> allTasksBetweenDays);
    }
}
