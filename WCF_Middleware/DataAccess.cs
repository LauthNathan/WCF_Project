﻿using System;
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
