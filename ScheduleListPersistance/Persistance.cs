/**************************************************************************
 *                                                                        *
 *  File:        Persistance.cs                                           *
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
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            } catch (MySqlException e)
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
            } catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return tasks;
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Get a list of task from db for a given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Task> IPersistance.GetTasksForAGivenDate(string date)
        {
            List<Task> tasks = new List<Task>();

            // string sql = "select t.time, d.date, t.title, t.subtitle, t.description, t.status, t.priority from tasks t join days d using(id) where d.date = @date";
            string sql = "select t.time, d.date, t.title, t.subtitle, t.description, t.status, t.priority from tasks t join days d on(d.id = t.day_id)  where d.date = @date;";
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

                    Task task = CreateNewTask(time, title, subtitle, description, status, priority);

                    tasks.Add(task);
                }
                rdr.Close();

            } catch (MySqlException e)
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
        /// <param name="task"></param>
        public void CreateNewTask(Task task, string selectedDate)
        {
            if (DayExists(selectedDate) == true) {
                int day_id = GetIdForAGivenDate(selectedDate);
                task.DayId = day_id;
                InsertTaskIntoDb(task);
            }
            else
            {
                InsertDateIntoDb(selectedDate);
                int day_id = GetIdForAGivenDate(selectedDate);
                task.DayId = day_id;
                InsertTaskIntoDb(task);
            }
        }


        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Insert a new day in db.
        /// </summary>
        /// <param name="currentDate"></param>
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
        /// <param name="task"></param>
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
        ///  Delete a task from db.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            //string sql = "delete from tasks where time=@time and title=@title and day_id=@day_id and description=@description";

            string sql = "delete from tasks where time=@time and title=@title";
            try
            {
                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@time", task.Time);
                cmd.Parameters.AddWithValue("@title", task.Title);
                //cmd.Parameters.AddWithValue("@day_id", task.DayId);
                //cmd.Parameters.AddWithValue("@description", task.Description);

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
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetIdForAGivenDate(string date)
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

            } catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return id;
        }

        /// <summary>
        ///  Halip Vasile Emanuel
        ///  Check in days table if exist a column that contains our currenDate.
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public bool DayExists(string currentDate)
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
            } catch (MySqlException e)
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
        /// <param name="time"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public Task CreateNewTask(string time, string title, string subtitle, string description, string status, int priority)
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
        ///  Stan Dragos
        ///  Get in progress task number.
        /// </summary>
        int IPersistance.GetInProgressTaskNumbers()
        {
            int number = 0;
            string sql = "SELECT * FROM tasks where status = @status";
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@status", "in progress");

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    number++;
                }
                rdr.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return number;
        }

        /// <summary>
        ///  Stan Dragos
        ///  Update a task from db by title, subtitle and description.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task UpdateTaskDetails(Task task, string title, string subtitle, string description)
        {
            string sql = "update tasks set title=@title, subtitle=@subtitle, description=@description where id=@id";

            try
            {
                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", task.Id);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@subtitle", subtitle);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return task;
        }

        /// <summary>
        ///  Stan Dragos
        ///  Update a task from db by status.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task UpdateTaskStatus(Task task, string status)
        {
            string sql = "update tasks set status=@status where id=@id";

            try
            {
                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", task.Id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return task;
        }

        /// <summary>
        ///  Stan Dragos
        ///  Get list of all tasks that are in between two dates.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Task> GetTasksBetweenDates(string start, string end)
        {
            List<Task> tasks = new List<Task>();

            //string sql = "select * from tasks t join days d on(d.id = t.day_id) where date between @start and @end;";
            string sql = "select * from tasks t join days d on(d.id = t.day_id) where STR_TO_DATE(date, \"%d.%m.%Y\") between STR_TO_DATE(@start, \"%d.%m.%Y\") and STR_TO_DATE(@end, \"%d.%m.%Y\");";  
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string time = rdr.GetString(0);
                    string stringDate = rdr.GetString(1);
                    string title = rdr.GetString(2);
                    string subtitle = rdr.GetString(3);
                    string description = rdr.GetString(4);
                    string status = rdr.GetString(5);
                    int priority = rdr.GetInt32(6);

                    Task task = CreateNewTask(time, title, subtitle, description, status, priority);

                    tasks.Add(task);
                }
                rdr.Close();
            }         
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return tasks;
        }


        /// <summary>
        ///  Ciobanu Denis Marian
        ///  Update Task by giving proprieties from view.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="time"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public Task UpdateTaskFowView(Task task, string time, string title, string subtitle, string status, int priority)
        {
            
            string sql = "update tasks set time = @time, title=@title, subtitle=@subtitle, status = @status, priority = @priority where title = @old_title and time = @old_time and subtitle = @old_subtitle";

            try
            {
                var cmd = new MySqlCommand(sql, _connection);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@old_time", task.Time);
                cmd.Parameters.AddWithValue("@old_title", task.Title);
                cmd.Parameters.AddWithValue("@old_subtitle", task.Subtitle);

                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@subtitle", subtitle);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@priority", priority);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine(e.ToString());
            }
            return task;
        }

        /// <summary>
        ///  Stan Dragos
        ///  Get completed task number
        /// </summary>
        int IPersistance.GetCompletedTaskNumbers()
        {
            int number = 0;
            string sql = "SELECT * FROM tasks where status = @status";
            var cmd = new MySqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@status", "done");

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    number++;
                }
                rdr.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return number;
        }
    } 
}

