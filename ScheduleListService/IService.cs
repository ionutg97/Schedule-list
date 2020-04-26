using Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        List<Task> GetTasksBetweenDates(string start, string end);
        
        decimal GetFinishedTasksPercent(string start, string end);
        decimal GetEffiencyOfTasksPercent(string start, string end);
    }
}
