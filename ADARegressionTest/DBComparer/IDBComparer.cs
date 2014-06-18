namespace DBComparer
{
    using log4net;

    using PetaPoco;

    public interface IDBComparer
    {
        #region Properties

        Database DBAssert
        {
            get;
        }

        Database DbTarget
        {
            get;
        }

        ILog Logger
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        bool CompareDatabaseContent();

        bool CompareDatabaseStructure();

        #endregion Methods
    }
}