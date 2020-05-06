using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ScheduleListPersistance;
using System.Collections.Generic;

namespace UnitTest
{
    /// <summary>
    /// Halip Vasile Emanuel
    /// This class scope is to ensure that extracted data from persistence  layer are correct.
    /// </summary>
    [TestClass]
    public class PersistanceLayerTest
    {

        private IPersistance _persistance;

        public PersistanceLayerTest()
        {
            _persistance = Persistance.getInstanceOfMySqlConnection();
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

            Task realTask = _persistance.CreateNewTask(time, title, subtitle, description, status, priority);

            Assert.AreEqual(mockTask.Object.Title, realTask.Title);
            Assert.AreEqual(mockTask.Object.Subtitle, realTask.Subtitle);
            Assert.AreEqual(mockTask.Object.Description, realTask.Description);
            Assert.AreEqual(mockTask.Object.Status, realTask.Status);
            Assert.AreEqual(mockTask.Object.Priority, realTask.Priority);
        }
    }
}
