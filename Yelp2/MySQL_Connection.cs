using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Yelp2
{
    class MySQL_Connection
    {
        private MySqlConnection connection;
    

    //Constructor

   public MySQL_Connection()
        {
            try
            {
                Initialize();
            }
            catch(MySqlException ex)
            {

            }
        }

    //Intialize Connection

    private void Initialize()
    {
        string server;
        string database;
        string uid;
        string passwrd;
        server = "localhost";
        database = "yelp2";
        uid = "root";
        passwrd = "Bosox23!";
        string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + passwrd + ";";
        
        connection = new MySqlConnection(connectionString);

    }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                if(ex.Number == 0)
                {
                    return false;

                }
                else if(ex.Number == 1045)
                {
                    return false;

                }
            }
            return false;
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;

            }
            catch(MySqlException ex)
            {

            }
            return false;
        }

        public List<String> SQLSELECTExec(string querySTR, string column_name)
        {
            List<String> qResult = new List<String>();

            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();
               
                while(datareader.Read())
                {
                    qResult.Add(datareader.GetString(column_name));
                }
                datareader.Close();

                this.CloseConnection();
            }
            return qResult;
        }

        public String SQLSELECTExec2(string querySTR, string column_name)
        {
            String qResult = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                 if(datareader.IsDBNull(0))
                 {
                     qResult = "";
                 }
                    else
                 {
                     qResult = Convert.ToString(double.Parse(datareader.GetString(0)));
                 }
                        
                    
                    //qResult.Add(datareader.GetString(column_name));
                }
                datareader.Close();

                this.CloseConnection();
            }
            return qResult;



        }

       
        public void createsqldatabase(string querystr)
        {
            if(this.OpenConnection() == true)
            {
               
                
               MySqlCommand cmd = new MySqlCommand(querystr,connection);
                    
               cmd.ExecuteNonQuery();
               
                
            }
        }
        public void insertsqldatabase(string querystr)
        {
            


                MySqlCommand cmd = new MySqlCommand(querystr, connection);

                cmd.ExecuteNonQuery();


            
        }
    
        public List<String> Sqlopenconnection(string querystr, string column)
        {
            List<String> qResult = new List<String>();
            MySqlCommand cmd = new MySqlCommand(querystr, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    qResult.Add(datareader.GetString(column));
                }
                datareader.Close();
            
            return qResult;
        }
        public List<String> SQLSELECTExec3(string querySTR, string column_name)
        {
            List<String> qResult = new List<String>();
           
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    if (datareader.IsDBNull(0))
                    {
                        qResult.Add("");
                    }
                    else
                    {
                        //qResult.Add(Convert.ToString(double.Parse(datareader.GetString(0))));
                        qResult.Add(Convert.ToString(double.Parse(datareader.GetString(0))));
                    }


                    //qResult.Add(datareader.GetString(column_name));
                }
                datareader.Close();

            
            return qResult;



        }




    }
}
