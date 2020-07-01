using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Data.SqlClient;

namespace WCF_Middleware {
    public class DataAccess {

        
        private string connectionString = null;
        private string Rq_sql { get; set; }
        public SqlConnection Cnn { get ; set; }
        private SqlDataReader DataReader { get; set; }
        private SqlCommand Cmd { get ; set ; }
        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public DataAccess() {
            connectDB();
        }

        /// <summary>
        /// Connect this instance to the database
        /// </summary>
        private void connectDB() {
            ConnectionString = "Data Source=DESKTOP-500VV2V\\WCF_SQL_SERVER;Initial Catalog=wcf_bdd;Integrated Security=True;";
            Cnn = new SqlConnection(ConnectionString);
            try {
                Cnn.Open();
                Console.WriteLine("Connection Open ! ");

            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                Console.WriteLine("Can not open connection ! ");
            }
        }

        /// <summary>
        /// Get the usertoken corresponding to the given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pswd"></param>
        /// <returns></returns>
        public string authenticate(string username, string pswd) {
            Rq_sql = "SELECT tokenUser FROM users WHERE username = '" + username + "' AND password = '" + pswd + "';";
            Cmd = new SqlCommand(Rq_sql, Cnn);
            DataReader = Cmd.ExecuteReader();
            string response;

            if (DataReader.Read()) {

                response = DataReader.GetString(0);
                DataReader.Close();
                Cmd.Dispose();
                return response;

            } else {
                Console.WriteLine("Wrong credentials");
                DataReader.Close();
                Cmd.Dispose();
                return null;
            }
        }

        /// <summary>
        /// Retrieve the mail corresponding to the given user token 
        /// </summary>
        /// <param name="usertoken">User token of the user sending the request to the server</param>
        /// <returns>The mail as a string, or null if no mail is found</returns>
        public string getMail(string usertoken) {
            string Rq_sql = "SELECT email FROM users WHERE tokenUser = '" + usertoken + "';";
            SqlCommand cmd = new SqlCommand(Rq_sql, Cnn);
            SqlDataReader DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                string response = DataReader.GetString(0);
                DataReader.Close();
                cmd.Dispose();

                return response;

            } else {

                DataReader.Close();
                cmd.Dispose();
                return null;
            }

        }

        /// <summary>
        /// Check if the user token given by the message is in the database, and so authorized to access to the different services 
        /// </summary>
        /// <param name="message">The MSG object sent by the client</param>
        /// <returns>True if the username correspond to one in the database, and false if not </returns>
        public bool checkToken(MSG message) {

            Rq_sql = "SELECT username, tokenUser FROM users WHERE tokenUser = '" + message.tokenUser + "';";
            Cmd = new SqlCommand(Rq_sql, Cnn);
            DataReader = Cmd.ExecuteReader();

            if (DataReader.Read()) {

                DataReader.Close();
                Cmd.Dispose();
                
                return true;

            } else {

                DataReader.Close();
                Cmd.Dispose();
                
                return false;
            }
        }

        /// <summary>
        /// Get the username associated wit the given user token
        /// </summary>
        /// <param name="message">The MSG object sent by the client</param>
        /// <returns>Return the username found, or null if no correspondance has been found</returns>
        public string getUsername(MSG message) {
            Rq_sql = "SELECT username FROM users WHERE tokenUser = '" + message.tokenUser + "';";
            Cmd = new SqlCommand(Rq_sql, Cnn);
            DataReader = Cmd.ExecuteReader();

            if (DataReader.Read()) {

                string response = DataReader.GetString(0);
                DataReader.Close();
                Cmd.Dispose();
               
                return response;

            } else {

                DataReader.Close();
                Cmd.Dispose();
                
                return null;
            }
        }

        /// <summary>
        /// Check if the app token given by the message is in the database, and so authorized to access to the different services 
        /// </summary>
        /// <param name="message">The MSG object sent by the client</param>
        /// <returns>True if the app token correspond to one in the database, and false if not</returns>
        public bool checkAppToken(MSG message) {
            Rq_sql = "SELECT tokenApp FROM tokenApp WHERE tokenApp = '" + message.tokenApp + "';";
            Cmd = new SqlCommand(Rq_sql, Cnn);
            DataReader = Cmd.ExecuteReader();

            if (DataReader.Read()) {

                DataReader.Close();
                Cmd.Dispose();
                return true;

            } else {

                DataReader.Close();
                Cmd.Dispose();
                return false;
            }
        }
    }

}
