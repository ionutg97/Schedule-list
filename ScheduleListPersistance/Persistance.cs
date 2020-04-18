using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleListPersistance
{
    public class Persistance : IPersistance
    {

        private MySqlConnection _connection;
        private static IPersistance _persistance;

        /// <summary>
        ///  Galan Ionut Andrei
        ///  Create conection with database.
        /// </summary>
        private Persistance()
        {
            string connString = "SERVER= 127.0.0.1;PORT=3306;DATABASE=schedule_list;UID=root;PASSWORD=admin;";

            try
            {
                _connection = new MySqlConnection();
                _connection.ConnectionString = connString;
                _connection.Open();
                Console.WriteLine("Connection success!");

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  Galan Ionut Andrei
        ///  Return persistance layer instance using Singleton Design.
        /// </summary>
        public static IPersistance getInstanceOfMySqlConnection()
        {
            if (_persistance == null)
                _persistance = new Persistance();
            return _persistance;
        }

        public void SayHello()
        {
            Console.WriteLine("Hello from persistance");
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Get all days from db.
        /// </summary>
        List<Day> IPersistance.GetDays()
        {
            List<Day> days = new List<Day>();

            string sql = "SELECT * FROM days";
            var cmd = new MySqlCommand(sql, _connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));

                int task_id = rdr.GetInt32(2);
                string stringDate = rdr.GetString(1);
                DateTime dt = ConvertStringToDate(stringDate);

                Day day = new Day();
                day.Date = dt;
                day.Task_id = task_id;

                days.Add(day);
            }
            rdr.Close();

            return days;
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Get all tasks from db.
        /// </summary>
        List<Task> IPersistance.GetTasks()
        {
            List<Task> tasks = new List<Task>();

            string sql = "SELECT * FROM tasks";
            var cmd = new MySqlCommand(sql, _connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),
                    rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetInt32(6));

                // formatul este asa =>       12:00:00
                string time = rdr.GetString(1);
                string[] times = time.Split(':');

                DateTime date = new DateTime(2020, 1, 1, Int32.Parse(times[0]), Int32.Parse(times[1]), Int32.Parse(times[2]));

                string title = rdr.GetString(2);
                string subtitle = rdr.GetString(3);
                string description = rdr.GetString(4);
                string status = rdr.GetString(5);
                int priority = rdr.GetInt32(6);

                Task task = CreateNewTask(date, title, subtitle, description, status, priority);

                tasks.Add(task);
            }
            rdr.Close();

            return tasks;
        }


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Create a new task with given values.
        /// </summary>
        private Task CreateNewTask(DateTime date, string title, string subtitle, string description, string status, int priority)
        {
            Task task = new Task();
            task.Date = date;
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
        private DateTime ConvertStringToDate(string stringDate)
        {
            // formatul este asa =>     1/5/2020 12:00:00 AM
            string[] peaches = stringDate.Split(' ');  /// fac split in functie de spatiu 
            string[] date = peaches[0].Split('/');  /// apoi fac split in functie de / ca sa extrag data

           /* Console.WriteLine("" + date[0].ToString());
            Console.WriteLine("" + date[1].ToString());
            Console.WriteLine("" + date[2].ToString());*/

            int day = Int32.Parse(date[1]);
            int month = Int32.Parse(date[0]);
            int year = Int32.Parse(date[2]);

            DateTime dt = new DateTime(year, month, day, 0, 0, 0);
            return dt;
        }


 
    }
}

