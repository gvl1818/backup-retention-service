using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace BackupRetention
{
    public class SqlCEHelper
    {
        private SqlCeConnection LocalConnection = null;
        //private string DataSource = @"Data Source=[path to your .sdf file]";

        public SqlCEHelper(string strConnectionString)
        {
          // create new database connection
            LocalConnection = new SqlCeConnection(strConnectionString);
        }

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
 
        private void CloseConnection()
        {
            if (LocalConnection != null)
            {
                LocalConnection.Close();
                //LocalConnection.Dispose();
            }
        }
 
        #region ExecuteDataReader
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
}
