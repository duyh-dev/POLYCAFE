using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class DBUtil
    {
        private static string _connectionString = "Data Source=.;Initial Catalog=QLSV;Integrated Security=True";

        public static SqlCommand GetCommand(string sql, CommandType type = CommandType.Text)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            return cmd;
        }

        public static int Update(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực hiện Update(): " + ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static DataTable Query(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực hiện Query(): " + ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static object Value(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực hiện Value(): " + ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}
