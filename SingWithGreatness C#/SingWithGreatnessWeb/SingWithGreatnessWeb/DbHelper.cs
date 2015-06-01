using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Configuration;

namespace SingWithGreatnessWeb
{
    public static class DbHelper
    {
        public static DataTable GetDBData(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(sql, connection))
                    {
                        using (OdbcDataAdapter da = new OdbcDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // write error message to logs
            }

            return dt;
        }

        public static void SendQuery(string sql)
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();

                    using (OdbcCommand command = new OdbcCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // write error message to logs
            }
        }

        public static int GetNextID(string table)
        {
            DataTable dt = new DataTable();

            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT MAX(id) FROM " + table, connection))
                    {
                        using (OdbcDataAdapter da = new OdbcDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // write error message to logs
            }

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}