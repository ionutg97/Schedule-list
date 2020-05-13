/**************************************************************************
 *                                                                        *
 *  File:        PersostanceLayerTest.cs                                  *
 *  Copyright:   (c) 2019-2020, Halip Vasile Emanuel                      *
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ScheduleListPersistance;
using System;

namespace UnitTest
{
    /// <summary>
    /// Halip Vasile Emanuel
    /// This class scope is to ensure that extracted data from persistence layer are correct.
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

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if CreateNewTask() method is not null.
        /// </summary>
        [TestMethod]
        public void TestIfCreateNewTaskMethodIsNotNull()
        {
            string time = "10:10:10";
            string title = "newTitle";
            string subtitle = "newSubtitle";
            string description = "newDescription";
            string status = "newStatus";
            int priority = 1;

            Task realTask = _persistance.CreateNewTask(time, title, subtitle, description, status, priority);

            Assert.IsNotNull(realTask);
        }


        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if UpdateTaskFowView() method throw NullReferenceException.
        /// </summary>
        [TestMethod]
        public void TestIfUpdateTaskFowViewThrowNullReferenceException()
        { 
            Task actualTask = _persistance.UpdateTaskFowView(null,"","","","",0);
            NullReferenceException obj = new NullReferenceException();
            AssertFailedException.ReferenceEquals(obj, actualTask);
        }

        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if DayExists() method return false for a empty given date.
        /// </summary>
        [TestMethod]
        public void TestIfDayExistMethodReturnFalseForAGivenDate()
        {
            bool actualResult = _persistance.DayExists("");
            Assert.IsFalse(actualResult);
        }


        /// <summary>
        /// Halip Vasile Emanuel
        /// Test if DayExists() method return true for a negativ given date.
        /// </summary>
        [TestMethod]
        public void TestIfDayExistMethodReturnFalseForANegativeGivenDate()
        {
            bool actualResult = _persistance.DayExists("-2");
            Assert.IsFalse(actualResult);
        }
    }
}
