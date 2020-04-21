using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleListPersistance
{
    public interface IPersistance
    {
        void SayHello();
        List<Day> GetDays();
        List<Task> GetTasks();

        List<Task> GetTasksForAGivenDate(String date);

        void CreateNewTask(Task task);

        int GetCompletedTaskNumbers();

        int GetInProgressTaskNumbers();

        void DeleteTask(Task task);
    }
}
