namespace ADARegressionTest
{
    using System;
    using System.IO;

    using ADARegressionTest.Logging;

    using CmdLine;

    using DBComparer;

    internal class Program
    {
        #region Methods

        private static void Main()
        {
            try
            {
                var arguments = CommandLine.Parse<ComparingArguments>();
                var dbAssert = new DbObject(arguments.NewDB);
                var dbTarget = new DbObject(arguments.TargetDB);

                Console.WriteLine("Initiating DBComparer! ");
                Console.WriteLine("Comparing '{0}' ", dbAssert);
                Console.WriteLine("With '{0}'", dbTarget);

                IDBComparer comparer = new DBPetapocoComparer(dbAssert, dbTarget)
                                   {
                                       Logger =
                                           LocalLogger.GetInstance(
                                               new FileInfo(arguments.WriteLog),arguments.MailTo)
                                   };

                bool matchingStructure = comparer.CompareDatabaseStructure();
                Console.WriteLine(matchingStructure
                                      ? "Strukturen er den samme i begge baser"
                                      : "Strukturen er ikke ens på de to baser");
                if (matchingStructure)
                {
                    bool matchingContent = comparer.CompareDatabaseContent();
                    Console.WriteLine(matchingContent
                                          ? "Indholdet er det samme i begge baser"
                                          : "Indholdet er ikke ens i de to baser - se log");
                }

                Console.ReadKey();
            }
            catch (CommandLineException exception)
            {
                Console.WriteLine(exception.ArgumentHelp.Message);
                Console.WriteLine(exception.ArgumentHelp.GetHelpText(Console.BufferWidth));
                Console.ReadKey();
            }
        }

        #endregion Methods
    }
}