using System.Data;
using System.Data.SqlClient;

namespace StudentRegistrationForm.Models
{
    public class DAL
    {
        private DAL()
        {
        }

        private static DAL? objdal;

        public static DAL GetInstance()
        {
            if (objdal == null)
            {
                objdal = new DAL();
            }
            return objdal;
        }

        public DataSet GetDataSet(string sSqlfo, string conString)
        {
             
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                using (SqlCommand command = new SqlCommand(sSqlfo, con))
                using (SqlDataAdapter adap = new SqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();
                    adap.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }
                    else
                    {
                        return new DataSet(); 
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}