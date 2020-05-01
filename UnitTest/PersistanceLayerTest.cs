using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ScheduleListPersistance;

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

        /// <summary>
        /// 
        /// </summary>
        public PersistanceLayerTest()
        {
            _persistance = Persistance.getInstanceOfMySqlConnection();
        }


        [TestMethod]
        public void TestMethod1()
        {
            Mock<Task> mockTask = new Mock<Task>();
            mockTask.Setup(Time = time;
            mockTask.Title = title;
            mockTask.Subtitle = subtitle;
            mockTask.Description = description;
            mockTask.Status = status;
            mockTask.Priority = priority;

            //Assert.AreEqual(3, _persistance.CreateNewTask()));

        }
    }
}
