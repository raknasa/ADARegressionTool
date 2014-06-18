namespace ADARegressionTest
{
    using CmdLine;

    [CommandLineArguments(Program = "ADARegressionTest", Title = "Compare two ADA-databases",
        Description = "Used to verify no regressionfailure has been found")]
    public class ComparingArguments
    {
        #region Fields

        private string _writeLog = "file.log";

        #endregion Fields

        #region Properties

        [CommandLineParameter(Command = "?", Default = false, Description = "Show Help", Name = "Help", IsHelp = true)]
        public bool Help
        {
            get; set;
        }

        [CommandLineParameter(Command = "m", Default = "", Description = "Mailadresse for errors", Name = "MailTo")]
        public string MailTo
        {
            get;
            set;
        }

        [CommandLineParameter(Name = "NewDB", Command = "n", ParameterIndex = 2, Required = true,
            Description = "Specifies the DB to be compaired against the target")]
        public string NewDB
        {
            get; set;
        }

        [CommandLineParameter(Name = "targetDB", Command = "t", ParameterIndex = 1, Required = true,
            Description = "Specifies the DB to be used as target.")]
        public string TargetDB
        {
            get; set;
        }

        [CommandLineParameter(Command = "l", Required = false,
            Description = "Indicates the name of the logfil. Default is 'file.log'")]
        public string WriteLog
        {
            get { return _writeLog; }
            set { _writeLog = value; }
        }

        #endregion Properties
    }
}