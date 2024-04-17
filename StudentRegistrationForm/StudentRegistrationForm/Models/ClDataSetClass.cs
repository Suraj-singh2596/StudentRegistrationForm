using System.Data;

namespace StudentRegistrationForm.Models
{
    public class ClDataSetClass
    {
        public ClDataSetClass(out DataSet dt, string ProcedureName, string Parameters, string conString)
        {
            string m_sqlString = "EXEC " + ProcedureName + " " + Parameters;
            DAL d = DAL.GetInstance();
            dt = d.GetDataSet(m_sqlString, conString);
        }
    }
}