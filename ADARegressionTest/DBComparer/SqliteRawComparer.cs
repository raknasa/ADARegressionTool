#region Header

//namespace DBComparer
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Configuration;
//    using System.Data;
//    using System.Data.SQLite;
//    using System.IO;
//    using System.Linq;
//    using Castle.Core.Logging;
//    public class SqliteRawComparer : IDBComparer
//    {
//        #region Fields
//        private ILogger _logger = new NullLogger();
//        #endregion Fields
//        #region Properties
//        public ILogger Logger
//        {
//            get { return _logger; }
//            set { _logger = value; }
//        }
//        #endregion Properties
//        #region Methods
//        public void AttachDB(string newDBName)
//        {
//            throw new NotImplementedException();
//        }
//        public void CompareDatabaseStructure()
//        {
//            string dbName = Path.GetFileNameWithoutExtension(dbTarget.ToString());
//            //string sql = string.Format("ATTACH '{0}' AS {1}", dbTarget.Value.FullName,
//                                       //"target");
//            //var cmd = new SQLiteCommand(sql);
//            //var connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ADADB"].ConnectionString);
//            var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Read Only=True;", dbTarget.Value.FullName));
//            var otherConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Read Only=True;", dbFile.Value.FullName));
//           // cmd.Connection = connection;
//            try
//            {
//                connection.Open();
//                IEnumerable<string> tableNames = GetTableDefinitions(connection, "target");
//                IEnumerable<string> otherTableNames = GetTableDefinitions(otherConnection, "Source");
//                bool result = CompareTablenames(tableNames, otherTableNames);
//                IEnumerable<string> queryes = GetQueryAllPosts(tableNames);
//                if (result ^ queryes.Any())
//                {
//                    WriteToTheLog(ExecuteSomeQuery(queryes, connection));
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("An error occurred, the attachment was not completed." + e);
//            }
//            finally
//            {
//                //cmd.Dispose();
//            }
//           // sql = "INSERT INTO SUBCONTRACTOR SELECT * FROM TOMERGE.SUBCONTRACTOR";
//           // cmd = new SQLiteCommand(sql) {Connection = connection};
//            try
//            {
//             //   int retval = cmd.ExecuteNonQuery();
//               // Console.WriteLine("There where {0} records affected", retval);
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("An error occurred, your attachment was not completed.");
//            }
//            finally
//            {
//                //cmd.Dispose();
//                connection.Close();
//            }
//        }
//        public IEnumerable<string> ExecuteSomeQuery(IEnumerable<string> sql)
//        {
//            throw new NotImplementedException();
//        }
//        /// <summary>
//        ///     Executes some query.
//        /// </summary>
//        /// <param name="sql">The SQL.</param>
//        /// <returns></returns>
//        /// <exception cref="System.NotImplementedException"></exception>
//        public IEnumerable<string> ExecuteSomeQuery(IEnumerable<string> sql, SQLiteConnection con)
//        {
//            using (SQLiteConnection c = con)
//            {
//                if (c.State != ConnectionState.Open) c.Open();
//                foreach (string s in sql)
//                {
//                    using (var cmd = new SQLiteCommand(s, c))
//                    {
//                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
//                        {
//                            while (rdr.Read())
//                            {
//                                yield return rdr.ToString();
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        public IEnumerable<string> GetQueryAllPosts(IEnumerable<string> tableNames)
//        {
//            foreach (string tableName in tableNames)
//            {
//                string select = string.Format("SELECT * FROM {1}.{0} EXCEPT SELECT * FROM {2}.{0}", tableName, "target",
//                                              "newDB");
//                yield return select;
//            }
//        }
//        public IEnumerable<string> GetTableDefinitions(SQLiteConnection connection, string dbName)
//        {
//            return GetTables(connection, dbName);
//        }
//        /// <summary>
//        ///     Writes to the log.
//        /// </summary>
//        /// <param name="results">The results.</param>
//        /// <exception cref="System.NotImplementedException"></exception>
//        public void WriteToTheLog(IEnumerable<string> results)
//        {
//            //TODO Log writing should not be in this dll
//            using (var writer = new StreamWriter("loop.txt"))
//            {
//                writer.WriteLine("-------------- Differences {0} ------------------------", "Hmm");
//                foreach (string result in results)
//                {
//                    writer.WriteLine(result);
//                    writer.WriteLine("--------------------------------------");
//                }
//            }
//        }
//        private static DataTable GetDataTable(string sql)
//        {
//            return GetDataTable(sql, null);
//        }
//        private static DataTable GetDataTable(string sql, SQLiteConnection dbConnection)
//        {
//            try
//            {
//                var dt = new DataTable();
//                using (var c = new SQLiteConnection(dbConnection))
//                {
//                    if (c.State != ConnectionState.Open) c.Open();
//                    using (var cmd = new SQLiteCommand(sql, c))
//                    {
//                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
//                        {
//                            dt.Load(rdr);
//                            return dt;
//                        }
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//                return null;
//            }
//        }
//        private bool CompareTablenames(IEnumerable<string> tableNames, IEnumerable<string> otherTableNames)
//        {
//            return !tableNames.Except(otherTableNames).Any();
//        }
//        private IEnumerable<string> GetTables(SQLiteConnection connection, string dbName)
//        {
//            // executes query that select names of all tables in master table of the database
//            string query = string.Format("SELECT sql FROM sqlite_master " + "WHERE type = 'table'" + "ORDER BY 1");
//            DataTable table = GetDataTable(query, connection);
//            // Return all table names in the ArrayList
//            foreach (DataRow row in table.Rows)
//            {
//                yield return string.Format("{0}.", row.ItemArray[0]);
//            }
//        }
//        #endregion Methods
//    }
//}

#endregion Header