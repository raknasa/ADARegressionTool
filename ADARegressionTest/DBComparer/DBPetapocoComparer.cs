namespace DBComparer
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;

    using log4net;

    using PetaPoco;

    public class DBPetapocoComparer : IDBComparer
    {
        #region Fields

        private readonly Database _dbAssert;
        private readonly Database _dbTarget;

        #endregion Fields

        #region Constructors

        public DBPetapocoComparer(DbObject dbAssert, DbObject dbTarget)
        {
            _dbAssert = new Database(string.Format("Data Source={0};Version=3;Read Only=True;", dbAssert.Value),
                                     new SQLiteFactory());
            _dbTarget = new Database(string.Format("Data Source={0};Version=3;Read Only=True;", dbTarget.Value),
                                     new SQLiteFactory());
        }

        #endregion Constructors

        #region Properties

        public Database DBAssert
        {
            get { return _dbTarget; }
        }

        public Database DbTarget
        {
            get { return _dbTarget; }
        }

        public ILog Logger
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public bool CompareDatabaseContent()
        {
            IEnumerable<bool> results = new List<bool>();
            IEnumerable<string> targetTableNames = GetTableNames(_dbTarget);
            foreach (string targetTableName in targetTableNames)
            {
                IEnumerable<dynamic> targetItems = GetItems(targetTableName);
                IEnumerable<dynamic> assertItems = GetItems(targetTableName, _dbAssert);
                results = results.Concat(new[] {CompareItems(targetItems, assertItems)});
            }
            return !results.ToList().Exists(x => x == false);
        }

        public bool CompareDatabaseStructure()
        {
            string query = string.Format("SELECT sql FROM sqlite_master ORDER BY 1");

            IEnumerable<string> target = _dbTarget.Query<string>(query);

            IEnumerable<string> assertion = _dbAssert.Query<string>(query);
            IEnumerable<string> diff = target.Except(assertion);
            PrintToLog(diff);
            return !diff.Any();
        }

        private static IEnumerable<dynamic> GetItems(string targetTableName, Database database)
        {
            return database.Query<dynamic>(string.Format("Select * from {0}  order by 1", targetTableName));
        }

        private static IEnumerable<string> GetRowsAsStrings(IEnumerable<dynamic> dbItems)
        {
            foreach (dynamic dynamicItem in dbItems)
            {
                yield return DynamicExtension.PropertiesAsString(dynamicItem);
            }
        }

        private static IEnumerable<string> GetTableNames(Database database)
        {
            return
                database.Query<string>(
                    string.Format("SELECT name FROM sqlite_master " + "WHERE type = 'table'" + "ORDER BY 1"));
        }

        private bool CompareItems(IEnumerable<dynamic> targetItems, IEnumerable<dynamic> assertItems)
        {
            IEnumerable<string> targetItemsList = GetRowsAsStrings(targetItems);
            IEnumerable<string> assertItemList = GetRowsAsStrings(assertItems);
            IEnumerable<string> diff = targetItemsList.Except(assertItemList);
            PrintToLog(diff);
            return !diff.Any();
        }

        private IEnumerable<dynamic> GetItems(string tableName)
        {
            return GetItems(tableName, _dbTarget);
        }

        private void PrintToLog(IEnumerable<string> diff)
        {
            foreach (string s in diff)
            {
                Logger.Error(s);
            }
        }

        #endregion Methods
    }
}