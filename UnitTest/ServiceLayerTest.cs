using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ScheduleListService;

namespace UnitTest
{
    /// <summary>
    /// Halip Vasile Emanuel
    /// This class scope is to test the methods functionality of the service layer.
    /// </summary>
    [TestClass]
    public class ServiceLayerTest
    {
        private IService _serviceInstance;

        public ServiceLayerTest()
        {
            _serviceInstance = new Service();
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if ConvertStringToDate() method works as expected.
        /// </summary>
        [TestMethod]
        public void TestStringToDateConvertMethod()
        {
            String dateString = "1/5/2020 12:00:00 AM";
            DateTime actualResult = _serviceInstance.ConvertStringToDate(dateString);
            DateTime expectedResult = new DateTime(2020, 1, 5, 0, 0, 0);
            Assert.AreEqual(expectedResult.ToString(), actualResult.ToString());
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if CreateNewTask() method works as expected.
        /// </summary>
        [TestMethod]
        public void TestCreateNewTaskMethod()
        {
            string time = "10:10:10";
            string title = "newTitle";
            string subtitle = "newSubtitle";
            string description = "newDescription";
            string status = "newStatus";
            int priority = 1;

            Mock<Task> mockTask = new Mock<Task>();
            mockTask.Object.Time = time;
            mockTask.Object.Title = title;
            mockTask.Object.Subtitle = subtitle;
            mockTask.Object.Description = description;
            mockTask.Object.Status = status;
            mockTask.Object.Priority = priority;

            Task realTask = _serviceInstance.CreateNewTask(time, title, subtitle, description, status, priority);

            Assert.AreEqual(mockTask.Object.Title, realTask.Title);
            Assert.AreEqual(mockTask.Object.Subtitle, realTask.Subtitle);
            Assert.AreEqual(mockTask.Object.Description, realTask.Description);
            Assert.AreEqual(mockTask.Object.Status, realTask.Status);
            Assert.AreEqual(mockTask.Object.Priority, realTask.Priority);
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if CalculateEffiencyOfTasksPercent() method works as expected.
        /// </summary>
        [TestMethod]
        public void TestCalculateEffiencyOfTasksPercentMethod()
        {
            List<Task> tasks = CreateListForTest();
            decimal expectedResult = CalculateEffiencyOfTasksPercent(tasks);
            decimal actualResult = _serviceInstance.CalculateEffiencyOfTasksPercent(tasks);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if GetFinishedTasksPercent() method works as expected.
        /// </summary>
        [TestMethod]
        public void TestGetFinishedTasksPercentMethod()
        {
            List<Task> tasks = CreateListForTest();
            decimal expectedResult = CalculateFinishedTasksPercent(tasks);
            decimal actualResult = _serviceInstance.CalcualteFinishedTaskPercent(tasks);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Calcualte the Effiency of tasks to validate the TestCalculateEffiencyOfTasksPercent() method from service layer.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private decimal CalculateEffiencyOfTasksPercent(List<Task> tasks)
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
            decimal result = (int)((done_tasks_prio1 * 100 + done_tasks_prio2 * 75 + done_tasks_prio3 * 50) / count);
            return result;
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Create a list of Tasks to validate methods.
        /// </summary>
        /// <returns></returns>
        private List<Task> CreateListForTest()
        {
            List<Task> tasks = new List<Task>();
            Task t1 = new Task();
            t1.Status = "done";
            t1.Priority = 1;
            tasks.Add(t1);

            Task t2 = new Task();
            t2.Status = "done";
            t2.Priority = 2;
            tasks.Add(t2);

            Task t3 = new Task();
            t3.Status = "new";
            t3.Priority = 1;
            tasks.Add(t3);

            Task t4 = new Task();
            t4.Status = "done";
            t4.Priority = 3;
            tasks.Add(t4);

            Task t5 = new Task();
            t5.Status = "done";
            t5.Priority = 2;
            tasks.Add(t5);

            Task t6 = new Task();
            t6.Status = "new";
            t6.Priority = 3;
            tasks.Add(t6);

            Task t7 = new Task();
            t7.Status = "done";
            t7.Priority = 1;
            tasks.Add(t7);

            return tasks;
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Calculate finished tasks percent to validate CalcualteFinishedTaskPercent() method from service layer.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private decimal CalculateFinishedTasksPercent(List<Task> tasks)
        {
            decimal count = tasks.Count;
            decimal done = 0;
            foreach (var x in tasks)
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
    }
}
