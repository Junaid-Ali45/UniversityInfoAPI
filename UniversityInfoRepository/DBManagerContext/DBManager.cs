using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInfoRepository.DBManagerContext
{

    public class DBManager
    {
        public readonly IConfiguration _configuration;
        public string masterConStr = "";
        public string siteConStr = "";
        DataTable dt;
        public DBManager(IConfiguration configuration)
        {
            _configuration = configuration;
            masterConStr = _configuration.GetConnectionString("MasterConnection");
        }

        public string ValidationConnectionString(string SiteName)
        {
            DataTable DT = new DataTable();
            DT = GetConnectionString(SiteName);
            string connectionString = "Data Source=" + DT.Rows[0]["DataServer"] + ";Initial Catalog=" + DT.Rows[0]["DatabaseName"] + ";User ID=" + DT.Rows[0]["DBUserName"] + ";Password=" + DT.Rows[0]["DBUserPassword"] + "";
            return connectionString;
        }
        public DataTable GetConnectionString(string SiteName)
        {
            dt = new DataTable();
            dt = GetData("select * from tbl_PluginSetup where SiteID ='" + SiteName + "'");
            return dt;
        }

        public string GetScaler(string str)
        {
            string myReader = "";
            try
            {

                using (SqlConnection myCon = new SqlConnection(masterConStr))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(str, myCon))
                    {
                        myReader = myCommand.ExecuteScalar().ToString();
                        //objresult.Load(myReader);

                        //myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return myReader;

        }
        public DataTable GetData(string str)
        {
            DataTable objresult = new DataTable();
            try
            {
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(masterConStr))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(str, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        objresult.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return objresult;

        }
        public DataTable GetData(string SiteName, string str)
        {
            DataTable objresult = new DataTable();
            try
            {
                siteConStr = ValidationConnectionString(SiteName);
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(siteConStr))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(str, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        objresult.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return objresult;

        }
        public int ExecuteData(string str, params IDataParameter[] sqlParams)
        {
            int rows = -1;
            //try
            //{

            //siteConStr = ValidationConnectionString(SiteName);
            using (SqlConnection myCon = new SqlConnection(masterConStr))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(str, myCon))
                {
                    if (sqlParams != null)
                    {
                        foreach (IDataParameter para in sqlParams)
                        {
                            cmd.Parameters.Add(para);
                        }
                        rows = cmd.ExecuteNonQuery();

                        myCon.Close();
                    }





                }
            }
            //}
            //catch (Exception ex)
            //{

            //}


            return rows;


        }

        /// <summary>
        /// Calls SqlCommand.ExecuteDataReader() to retrieve a dataset from the database.
        /// </summary>
        /// <paramname="cmdText">The stored proc or query to execute</param>
        /// <paramname="parameters">The parameters to use in the storedproc/query</param>
        /// <returns></returns>
        public DataTable GetDataStoreProc(string cmdText, SqlParameter[] parameters = null,
                                   CommandType cmdType = CommandType.StoredProcedure)
        {
            // by defining these variables OUTSIDE the using statements, we can evaluate them in 
            // the debugger even when the using's go out of scope.
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            DataTable data = null;

            // create the connection
            using (conn = new SqlConnection(masterConStr))
            {
                // open it
                conn.Open();
                // create the SqlCommand object
                using (cmd = new SqlCommand(cmdText, conn)
                { CommandTimeout = 400, CommandType = cmdType })
                {
                    // give the SqlCommand object the parameters required for the stored proc/query
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    //create the SqlDataReader
                    using (reader = cmd.ExecuteReader())
                    {
                        // move the data to a DataTable
                        data = new DataTable();
                        data.Load(reader);
                    }
                }
            }
            // return the DataTable object to the calling method
            return data;
        }

        /// <summary>
        /// Calls SqlCommand.ExecuteNonQuery to save data to the database.
        /// </summary>
        /// <paramname="connStr"></param>
        /// <paramname="cmdText"></param>
        /// <paramname="parameters"></param>
        /// <returns></returns>
        public int SetDataStoreProc(string cmdText, SqlParameter[] parameters,
                             CommandType cmdType = CommandType.StoredProcedure)
        {
            int result = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(masterConStr))
            {
                conn.Open();
                using (cmd = new SqlCommand(cmdText, conn)
                { CommandTimeout = 400, CommandType = cmdType })
                {
                    SqlParameter rowsAffected = null;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                        // if this is a stored proc and we want to add a return param
                        if (cmdType == CommandType.StoredProcedure)
                        {
                            // see if we already have a return parameter
                            rowsAffected = parameters.FirstOrDefault
                                          (x => x.Direction == ParameterDirection.ReturnValue);
                            // if we don't, add one.
                            if (rowsAffected == null)
                            {
                                rowsAffected = cmd.Parameters.Add(new SqlParameter
                                              ("@rowsAffected", SqlDbType.Int)
                                { Direction = ParameterDirection.ReturnValue });
                            }
                        }
                    }
                    result = cmd.ExecuteNonQuery();
                    //result = (rowsAffected != null) ? (int)rowsAffected.Value : result;
                }
            }
            return result;
        }
        public string SetReturnDataStoreProc(string cmdText, SqlParameter[] parameters,
                             CommandType cmdType = CommandType.StoredProcedure)
        {
            string result = "";
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(masterConStr))
            {
                conn.Open();
                using (cmd = new SqlCommand(cmdText, conn)
                { CommandTimeout = 400, CommandType = cmdType })
                {
                    SqlParameter rowsAffected = null;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                        // if this is a stored proc and we want to add a return param
                        if (cmdType == CommandType.StoredProcedure)
                        {
                            // see if we already have a return parameter
                            rowsAffected = parameters.FirstOrDefault
                                          (x => x.Direction == ParameterDirection.ReturnValue);
                            // if we don't, add one.
                            if (rowsAffected == null)
                            {
                                rowsAffected = cmd.Parameters.Add(new SqlParameter
                                              ("@rowsAffected", SqlDbType.NVarChar)
                                { Direction = ParameterDirection.ReturnValue });
                            }
                        }
                    }
                    result = cmd.ExecuteScalar().ToString();
                    //result = (rowsAffected != null) ? (int)rowsAffected.Value : result;
                }
            }
            return result;
        }
    }
}
