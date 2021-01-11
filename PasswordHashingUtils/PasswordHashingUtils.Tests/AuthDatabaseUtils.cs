using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using IIG.DatabaseConnectionUtils;

namespace IIG.CoSFE.DatabaseUtils
{
    public class AuthDatabaseUtils : DatabaseConnection
    {
        public AuthDatabaseUtils(NameValueCollection appSettings) : base(appSettings)
        {
        }

        public AuthDatabaseUtils(string server, string database, bool isTrusted, string login = null,
            string password = null, int connectionTimeOut = 15) : base(server, database, isTrusted, login, password, connectionTimeOut)
        {
            // Console.WriteLine("database", server, database);
        }

        public bool AddCredentials(string login, string password)
        {
            
            if (!ConnectToDatabase()){
                Console.WriteLine("Couldn't connect to db");
                return false;
            }
            bool res;
            using (var cmd = new SqlCommand("[dbo].[AddCredentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    
                    cmd.ExecuteNonQuery();
                    
                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
            
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool UpdateCredentials(string login, string password, string newLogin, string newPassword)
        {
            
            if (!ConnectToDatabase()){
                Console.WriteLine("Couldn't connect to db");
                return false;
            }

            bool res;

            using (var cmd = new SqlCommand("[dbo].[UpdateCredentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@NewLogin", SqlDbType.NVarChar).Value = newLogin;
                    cmd.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = newPassword;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                   
                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool DeleteCredentials(string login, string password)
        {
            
            if (!ConnectToDatabase()){
                Console.WriteLine("Couldn't connect to db");
                return false;
            }

            bool res;
            using (var cmd = new SqlCommand("[dbo].[DeleteCredentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    
                    cmd.ExecuteNonQuery();
                    
                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
            
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool CheckCredentials(string login, string password)
        {
            
            if (ConnectToDatabase() == false){
                Console.WriteLine("Couldn't connect to db");
                return false;
            }
            
            bool res;
            using (var cmd = new SqlCommand("[dbo].[CheckCredentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    // Console.WriteLine(ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    // Console.WriteLine(cmd.Parameters["@Result"].Value);
                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }
    }
}