namespace DBComparer
{
    using System.IO;

    public class DbObject
    {
        #region Constructors

        public DbObject(string newDB)
        {
            if (!File.Exists(newDB)) throw new FileNotFoundException(newDB);
            Value = new FileInfo(newDB);
        }

        #endregion Constructors

        #region Properties

        public FileInfo Value
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return Value.Name;
        }

        #endregion Methods
    }
}