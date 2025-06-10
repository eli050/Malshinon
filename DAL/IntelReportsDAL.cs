using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.DB;
using Malshinon.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;


namespace Malshinon.DAL
{
    public class IntelReportsDAL
    {
        public MySQLData _mySql;
        public IntelReportsDAL(MySQLData sqlConnection)
        {
            _mySql = sqlConnection;
        }
        public IntelReports SearchById(int id)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySql.GetConnection();
                string query = $"SELECT * FROM intel_reports WHERE intel_reports.id = '{id}';";
                MySqlCommand cmd = new MySqlCommand(query,conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int ID = reader.GetInt32("id");
                    int reporterId = reader.GetInt32("reporter_id");
                    int targetId = reader.GetInt32("target_id");
                    string text = reader.GetString("text");
                    DateTime timeStamp = reader.GetDateTime("timestamp");
                    IntelReports intelReports = new IntelReports(reporterId,targetId,text,timeStamp,ID);
                    return intelReports;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySql.CloseConnection(conn);
                }
            }
        }
        public List<IntelReports> SearchByReporterId(int reporterId)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySql.GetConnection();
                string query = $"SELECT * FROM intel_reports WHERE intel_reports.reporter_id = '{reporterId}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<IntelReports> reports = new List<IntelReports>();
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32("id");
                        int ReporterId = reader.GetInt32("reporter_id");
                        int targetId = reader.GetInt32("target_id");
                        string text = reader.GetString("text");
                        DateTime timeStamp = reader.GetDateTime("timestamp");
                        reports.Add(new IntelReports(ReporterId, targetId, text, timeStamp, Id));
                    }
                    return reports;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySql.CloseConnection(conn);
                }
            }
        }
        public List<IntelReports> SearchByTargetId(int TargetId)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySql.GetConnection();
                string query = $"SELECT * FROM intel_reports WHERE intel_reports.target_id = '{TargetId}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<IntelReports> reports = new List<IntelReports>();
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32("id");
                        int ReporterId = reader.GetInt32("reporter_id");
                        int targetId = reader.GetInt32("target_id");
                        string text = reader.GetString("text");
                        DateTime timeStamp = reader.GetDateTime("timestamp");
                        reports.Add(new IntelReports(ReporterId, targetId, text, timeStamp, Id));
                    }
                    return reports;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySql.CloseConnection(conn);
                }
            }
        }
    }
}
