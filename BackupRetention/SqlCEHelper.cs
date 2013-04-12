using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace BackupRetention
{
    /// <summary>
    /// System.Data.SqlServerCe Helper Class
    /// </summary>
    public class SqlCEHelper
    {
        private SqlCeConnection LocalConnection = null;

        /// <summary>
        /// Contructor expects: @"Data Source=pathtofile.sdf";
        /// </summary>
        /// <param name="strConnectionString"></param>
        public SqlCEHelper(string strConnectionString)
        {
            // create new database connection
            LocalConnection = new SqlCeConnection(strConnectionString);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SqlCEHelper()
        {
            if (LocalConnection != null)
            {
                if (LocalConnection.State == ConnectionState.Open)
                {
                    LocalConnection.Close();
                }
                LocalConnection.Dispose();
            }
        }

        /// <summary>
        /// Opens SqlCeConnection
        /// </summary>
        private void OpenConnection()
        {
            if (LocalConnection != null)
            {
                LocalConnection.Open();
            }
            else
            {
                throw new NullReferenceException("The connection string is null!");
            }
        }
        /// <summary>
        /// Closes SqlCeConnection
        /// </summary>
        private void CloseConnection()
        {
            if (LocalConnection != null)
            {
                LocalConnection.Close();
                //LocalConnection.Dispose();
            }
        }
 
        #region ExecuteDataReader
        /// <summary>
        /// ExecuteDataReader
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SqlCeDataReader ExecuteDataReader(string query)
        {
          SqlCeDataReader localReader = null;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            localReader = localCommand.ExecuteReader(CommandBehavior.CloseConnection);
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
          }
          return (localReader);
        }

        /// <summary>
        /// ExecuteDataReader
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlCeDataReader ExecuteDataReader(string query, List<SqlCeParameter> parameters)
        {
          SqlCeDataReader localReader = null;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            if (parameters != null)
            {
              foreach (SqlCeParameter localParam in parameters)
                localCommand.Parameters.Add(localParam);
 
              localReader = localCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
          }
          return (localReader);
        }
        #endregion
 
        #region DataSet
        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string query)
        {
          DataSet localDataSet = null;
          SqlCeCommand localCommand = null;
          SqlCeDataAdapter localDataAdapter = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            localDataAdapter = new SqlCeDataAdapter();
            localDataAdapter.SelectCommand = localCommand;
            localDataSet = new DataSet();
 
            localDataAdapter.Fill(localDataSet, "DATA");
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
          }
          return (localDataSet);
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string query, List<SqlCeParameter> parameters)
        {
          DataSet localDataSet = null;
          SqlCeCommand localCommand = null;
          SqlCeDataAdapter localDataAdapter = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            localDataAdapter = new SqlCeDataAdapter();
            localDataAdapter.SelectCommand = localCommand;
            localDataSet = new DataSet();
 
            if (parameters != null)
            {
              foreach (SqlCeParameter localParam in parameters)
                localCommand.Parameters.Add(localParam);
 
              localDataAdapter.Fill(localDataSet, "DATA");
            }
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            localCommand.Dispose();
          }
          return (localDataSet);
        }
        #endregion
 
        #region ExecuteScalar
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query)
        {
          object localScalarObject = string.Empty;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            localScalarObject = localCommand.ExecuteScalar();
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
            CloseConnection();
          }
          return localScalarObject;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query, List<SqlCeParameter> parameters)
        {
          object localScalarObject = string.Empty;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            if (parameters != null)
            {
              foreach (SqlCeParameter localParam in parameters)
                localCommand.Parameters.Add(localParam);
 
              localScalarObject = localCommand.ExecuteScalar();
            }
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
            CloseConnection();
          }
          return localScalarObject;
        }
        #endregion
 
        #region ExecuteNonQuery
        /// <summary>
        /// Executes non query sql statement
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
          int rowsAffected = 0;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            rowsAffected = localCommand.ExecuteNonQuery();
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
            CloseConnection();
          }
 
          return (rowsAffected);
        }

        /// <summary>
        /// Executes non query sql statement with parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, List<SqlCeParameter> parameters)
        {
          int rowsAffected = 0;
          SqlCeCommand localCommand = null;
 
          try
          {
            OpenConnection();
 
            localCommand = new SqlCeCommand(query, LocalConnection);
            localCommand.CommandType = CommandType.Text;
 
            if (parameters != null)
            {
              foreach (SqlCeParameter localParam in parameters)
                localCommand.Parameters.Add(localParam);
            }
            rowsAffected = localCommand.ExecuteNonQuery();
          }
          catch (SqlCeException sqlCeEx)
          {
            throw (sqlCeEx);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          finally
          {
            if (localCommand != null) localCommand.Dispose();
            CloseConnection();
          }
          return (rowsAffected);
        }
        #endregion

        /// <summary>
        /// Inserts record into SQL Server Compact Table
        /// example sql: "INSERT INTO RFile (Name,FullName) VALUES (@Name,@FullName)"
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public long Insert(string query, List<SqlCeParameter> parameters)
        {
            long lastID = 0;
            int rowsAffected = 0;
            SqlCeCommand localCommand = null;
            SqlCeCommand localCommand2 = null;
            try
            {
                OpenConnection();

                localCommand = new SqlCeCommand(query, LocalConnection);
                localCommand.CommandType = CommandType.Text;

                localCommand2 = new SqlCeCommand("SELECT @@IDENTITY AS ID", LocalConnection);
                localCommand2.CommandType = CommandType.Text;
            
                if (parameters != null)
                {
                    foreach (SqlCeParameter localParam in parameters)
                        localCommand.Parameters.Add(localParam);
                }
                rowsAffected = localCommand.ExecuteNonQuery();
                //Get @@IDENTITY value;
                object o = localCommand2.ExecuteScalar();
                long.TryParse(o.ToString(), out lastID);

            }
            catch (SqlCeException sqlCeEx)
            {
                throw (sqlCeEx);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (localCommand != null) localCommand.Dispose();
                if (localCommand2 != null) localCommand2.Dispose();
                CloseConnection();
            }
            return lastID;
        }
    }

    /* Example Usage
     using System.Data;
     using System.Data.SqlServerCe;
     
     public long Save()
        {
            long lastid = 0;
            SqlCEHelper db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf"));

            List<SqlCeParameter> list = new List<SqlCeParameter>();

            SqlCeParameter sp;

            sp = new SqlCeParameter("@Name", SqlDbType.NVarChar,3000);
            sp.Value =Common.FixNullstring(Name);
            list.Add(sp);

            sp = new SqlCeParameter("@FullName", SqlDbType.NVarChar, 3000);
            sp.Value =Common.FixNullstring(FullName);
            list.Add(sp);

            sp = new SqlCeParameter("@FileLength", SqlDbType.BigInt);
            sp.Value =Length;
            list.Add(sp);

            try
            {
                lastid = db.Insert("INSERT INTO RFile (Name,FullName,FileLength) VALUES (@Name,@FullName,@FileLength)", list);
            }
            catch (Exception ex)
            {
                lastid = 0;

                throw ex;
               
            }
            return lastid;

        }
     */
}
