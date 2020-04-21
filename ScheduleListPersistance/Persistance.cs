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

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetString(1));

                    string stringDate = rdr.GetString(1);

                    Day day = new Day();
                    day.Date = stringDate;

                    days.Add(day);
                }
                rdr.Close();
            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

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

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),
                        rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetInt32(6), rdr.GetInt32(7));

                    string stringTime = rdr.GetString(1);
                    string title = rdr.GetString(2);
                    string subtitle = rdr.GetString(3);
                    string description = rdr.GetString(4);
                    string status = rdr.GetString(5);
                    int priority = rdr.GetInt32(6);

                    Task task = CreateNewTask(stringTime, title, subtitle, description, status, priority);

                    tasks.Add(task);
                }
                rdr.Close();
            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return tasks;
        }

        List<Task> IPersistance.GetTasksForAGivenDate(string date)
        {
            List<Task> tasks = new List<Task>();

            string sql = "select t.time, d.date, t.title, t.subtitle, t.description, t.status, t.priority from tasks t join days d using(id) where d.date = @data";
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@date", date);

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2),
                        rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetInt32(6));

                    string time = rdr.GetString(0);
                    string stringDate = rdr.GetString(1);
                    string title = rdr.GetString(2);
                    string subtitle = rdr.GetString(3);
                    string description = rdr.GetString(4);
                    string status = rdr.GetString(5);
                    int priority = rdr.GetInt32(6);

                    Task task = CreateNewTask(stringDate, title, subtitle, description, status, priority);

                    tasks.Add(task);
                }
                rdr.Close();

            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return tasks;

        }



        /// <summary>
        ///  Halip Vasile Emanuel
        ///  If the currentDate exist we can directly insert the task into db.
        ///  Otherwise we insert a new day, get it's id and using it to create a new task.
        /// </summary>
        public void CreateNewTask(Task task)
        {
            //string currentDate = "20.04.2050"; // use this to see tha else branch is working as expected
            string currentDate = DateTime.UtcNow.Date.ToString("dd.MM.yyyy");
            Console.WriteLine(currentDate);

            if (DayExists(currentDate) == true) {
                int day_id = GetIdForAGivenDate(currentDate);
                task.DayId = day_id;
                InsertTaskIntoDb(task);
            }
            else
            {
                InsertDateIntoDb(currentDate);
                int day_id = GetIdForAGivenDate(currentDate);
                task.DayId = day_id;
                InsertTaskIntoDb(task);
            }
        }


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Insert a new day in db.
        /// </summary>
        private void InsertDateIntoDb(string currentDate)
        {
            string sql = "INSERT INTO days(`date`) VALUES(@date)";

            try
            {


                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@date", currentDate);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
}


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Insert a new task in db and day_id( is fk that link days and tasks tables).
        /// </summary>
        private void InsertTaskIntoDb(Task task)
        {
            string sql = "INSERT INTO tasks(`time`, `title`, `subtitle`, `description`, `status`,`priority`, `day_id`)" +
                " VALUES(@time, @test_title, @test_subtitle, @test_description, @status, @priority, @day_id)";

            try
            {
                var cmd = new MySqlCommand(sql, _connection);

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@time", task.Time);
                cmd.Parameters.AddWithValue("@test_title", task.Title);
                cmd.Parameters.AddWithValue("@test_subtitle", task.Subtitle);
                cmd.Parameters.AddWithValue("@test_description", task.Description);
                cmd.Parameters.AddWithValue("@status", task.Status);
                cmd.Parameters.AddWithValue("@priority", task.Priority);
                cmd.Parameters.AddWithValue("@day_id", task.DayId);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Return a unique id from bd for a given date from days table.
        /// </summary>
        private int GetIdForAGivenDate(string date)
        {
            int id = -1;

            string sql = "select id from days d where d.date = @date";
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@date", date);

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    id = rdr.GetInt32(0);
                }
                rdr.Close();

            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
          
            return id;
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Check in days table if exist a column that contains our currenDate.
        /// </summary>
        private bool DayExists(string currentDate)
        {
            string sql = "SELECT * FROM days where days.date=@date";
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@date", currentDate);

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //exista
                    rdr.Close();
                    return true;
                }
                rdr.Close();
            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            //nu exista
            return false;
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Create a new task with given values.
        /// </summary>
        private Task CreateNewTask(string time, string title, string subtitle, string description, string status, int priority)
        {
            Task task = new Task();
            task.Time = time;
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

           /*Console.WriteLine("" + date[0].ToString());
            Console.WriteLine("" + date[1].ToString());
            Console.WriteLine("" + date[2].ToString());*/

            int day = Int32.Parse(date[1]);
            int month = Int32.Parse(date[0]);
            int year = Int32.Parse(date[2]);

            DateTime dt = new DateTime(year, month, day, 0, 0, 0);
            return dt;
        }

        public void DeleteTask(Task task)
        {
            string sql = "delete from tasks where time=@time and title=@title and day_id=@day_id and description=@description";

            try
            {
                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@time", task.Time);
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@day_id", task.DayId);
                cmd.Parameters.AddWithValue("@description", task.Description);

                cmd.ExecuteNonQuery();
            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
}
    }
}

