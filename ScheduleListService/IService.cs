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

        void CreateNewTask(Task task);

        int GetCompletedTaskNumbers();

        int GetInProgressTaskNumbers();

        void DeleteTask(Task task);
        Task UpdateTaskDetails(Task task, string title, string subtitle, string description);
        Task UpdateTaskStatus(Task task, string status);
    }
}
