using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleListPersistance
{
    public class Persistance : IPersistance
    {

        private MySqlConnection _connection;
        private static IPersistance _persistance;

        private Persistance()
        {
            string connString = "SERVER= 127.0.0.1;PORT=3306;DATABASE=schedule_list;UID=root;PASSWORD=mysql;";

            try
            {
                _connection = new MySqlConnection();
                _connection.ConnectionString = connString;
                _connection.Open();
                Console.WriteLine("Connection success!");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IPersistance getInstancePersistance()
        {
            if (_persistance == null)
                _persistance = new Persistance();
            return _persistance;
        }

        public void SayHello()
        {
            Console.WriteLine("Hello from persistance");
        }

    }
}

