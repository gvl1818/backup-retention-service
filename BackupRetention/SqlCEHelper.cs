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
    public class SqlCEHelper : IDisposable
    {
        private SqlCeConnection LocalConnection = null;

        private bool disposed = false; // to detect redundant calls

        /// <summary>
        /// Contructor expects: @"Data Source=pathtofile.sdf";
        /// </summary>
        /// <param name="strConnectionString"></param>
        public SqlCEHelper(string strConnectionString)
        {
            // create new database connection
            LocalConnection = new SqlCeConnection(strConnectionString);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                   
                }
                // Dispose unmanaged managed resources.
                if (LocalConnection != null)
                {
                    if (LocalConnection.State == ConnectionState.Open)
                    {
                        LocalConnection.Close();
                    }
                    LocalConnection.Dispose();
                }
                disposed = true;
                
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SqlCEHelper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Opens SqlCeConnection
        /// </summary>
        public void OpenConnection()
        {
            if (LocalConnection != null)
            {
                if (LocalConnection.State != ConnectionState.Open)
                {
                    LocalConnection.Open();
                }
            }
            else
            {
                throw new NullReferenceException("The connection string is null!");
            }
        }
        /// <summary>
        /// Closes SqlCeConnection
        /// </summary>
        public void CloseConnection()
        {
            if (LocalConnection != null)
            {
                if (LocalConnection.State == ConnectionState.Open)
                {
                    LocalConnection.Close();
                }
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
        public object ExecuteScalar(string query, bool blCloseConnection = true)
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
            if (blCloseConnection)
            {
                CloseConnection();
            }
          }
          return localScalarObject;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query, List<SqlCeParameter> parameters, bool blCloseConnection=true)
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
            if (blCloseConnection)
            {
                CloseConnection();
            }
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
        public int ExecuteNonQuery(string query, bool blCloseConnection=true)
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
            if (blCloseConnection)
            {
                CloseConnection();
            }
          }
 
          return (rowsAffected);
        }

        /// <summary>
        /// Executes non query sql statement with parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, List<SqlCeParameter> parameters, bool blCloseConnection=true)
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
            if (blCloseConnection)
            {
                CloseConnection();
            }
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
        public long Insert(string query, List<SqlCeParameter> parameters, bool blCloseConnection=true)
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
                if (blCloseConnection)
                {
                    CloseConnection();
                }
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
            SqlCEHelper db=null;
            List<SqlCeParameter> list=null;
            SqlCeParameter sp=null;
     
            try
            {
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                list = new List<SqlCeParameter>();

                sp = new SqlCeParameter("@Name", SqlDbType.NVarChar,3000);
                sp.Value =Common.FixNullstring(Name);
                list.Add(sp);

                sp = new SqlCeParameter("@FullName", SqlDbType.NVarChar, 3000);
                sp.Value =Common.FixNullstring(FullName);
                list.Add(sp);

                sp = new SqlCeParameter("@FileLength", SqlDbType.BigInt);
                sp.Value =Length;
                list.Add(sp);
     
                lastid = db.Insert("INSERT INTO RFile (Name,FullName,FileLength) VALUES (@Name,@FullName,@FileLength)", list);
            }
            catch (Exception ex)
            {
                lastid = 0;
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
                if (list !=null)
                {
                    list.Clear(); 
                }
            }
            return lastid;

        }
     */
}
