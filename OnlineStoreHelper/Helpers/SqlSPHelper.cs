using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuizComputation_490.Common
{
    public class SqlSPHelper
    {
        public static DataTable SqlSPConnector(string storedProcedure, Dictionary<string, object> parameters)
        {
            try
            {
                DataTable resultTable = new DataTable();
                SqlConnection con = new SqlConnection("Data Source=192.168.1.117,1580;Initial Catalog=OnlineStore;user id=sa;password=sit@123;");
                con.Open();                
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        foreach(KeyValuePair<string, object> kvp in parameters)
                        {
                            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);

                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(resultTable);
                        }
                    }
                return resultTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}