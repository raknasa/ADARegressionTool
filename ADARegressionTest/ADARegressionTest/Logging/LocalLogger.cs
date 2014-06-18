namespace ADARegressionTest.Logging
{
    using System.IO;

    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;

    public class LocalLogger
    {
        #region Methods

        public static ILog GetInstance(FileInfo logfile, string mailTo)
        {
            ILog log = LogManager.GetLogger(typeof (LocalLogger));
            Setup(logfile, mailTo);
            return log;
        }

        private static void Setup(FileInfo logfile, string mailTo)
        {
            var hierarchy = (Hierarchy) LogManager.GetRepository();

            RollingFileAppender roller = SetupRollingFileAppender(logfile);
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = SetupMemoryAppender();
            hierarchy.Root.AddAppender(memory);

            ColoredConsoleAppender screener = SetupConsoleAppender();
            hierarchy.Root.AddAppender(screener);

            SmtpAppender mailer = SetupMailAppender(mailTo);
            hierarchy.Root.AddAppender(mailer);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        private static ColoredConsoleAppender SetupConsoleAppender()
        {
            var patternLayout = new PatternLayout
                                    {
                                        ConversionPattern =
                                            "%level [%thread] %d{HH:mm:ss} - %message%newline"
                                    };
            patternLayout.ActivateOptions();
            var screener = new ColoredConsoleAppender {Layout = patternLayout};

            screener.AddMapping(new ColoredConsoleAppender.LevelColors
                                    {
                                        Level = Level.Debug,
                                        ForeColor =
                                            ColoredConsoleAppender.Colors.Cyan |
                                            ColoredConsoleAppender.Colors.HighIntensity
                                    });
            screener.AddMapping(new ColoredConsoleAppender.LevelColors
                                    {
                                        Level = Level.Info,
                                        ForeColor =
                                            ColoredConsoleAppender.Colors.Green |
                                            ColoredConsoleAppender.Colors.HighIntensity
                                    });
            screener.AddMapping(new ColoredConsoleAppender.LevelColors
                                    {
                                        Level = Level.Warn,
                                        ForeColor =
                                            ColoredConsoleAppender.Colors.Purple |
                                            ColoredConsoleAppender.Colors.HighIntensity
                                    });
            screener.AddMapping(new ColoredConsoleAppender.LevelColors
                                    {
                                        Level = Level.Error,
                                        ForeColor =
                                            ColoredConsoleAppender.Colors.Red |
                                            ColoredConsoleAppender.Colors.HighIntensity
                                    });
            screener.AddMapping(new ColoredConsoleAppender.LevelColors
                                    {
                                        Level = Level.Fatal,
                                        ForeColor =
                                            ColoredConsoleAppender.Colors.White |
                                            ColoredConsoleAppender.Colors.HighIntensity,
                                        BackColor = ColoredConsoleAppender.Colors.Red
                                    });
            screener.ActivateOptions();
            return screener;
        }

        private static SmtpAppender SetupMailAppender(string mailTo = "")
        {
            var patternLayout = new PatternLayout
                                    {
                                        ConversionPattern =
                                            "%property{log4net:HostName} :: %level :: %message %newlineLogger: %logger%newlineThread: %thread%newlineDate: %date%newlineNDC: %property{NDC}%newline%newline"
                                    };
            patternLayout.ActivateOptions();
            var mailer = new SmtpAppender
                             {
                                 To = mailTo,
                                 From = "ADARegessiontestTool",
                                 Layout = patternLayout,
                                 SmtpHost = "Smtp.live.com",
                                 Subject = "Regressiontest Error",
                                 Threshold = Level.Debug,
                                 BufferSize = 512
                             };
            mailer.ActivateOptions();
            return mailer;
        }

        private static MemoryAppender SetupMemoryAppender()
        {
            var memory = new MemoryAppender();
            memory.ActivateOptions();
            return memory;
        }

        private static RollingFileAppender SetupRollingFileAppender(FileInfo logfile)
        {
            var patternLayout = new PatternLayout
                                    {
                                        ConversionPattern =
                                            "%date [%thread] %-5level %logger - %message%newline"
                                    };
            patternLayout.ActivateOptions();
            var roller = new RollingFileAppender
                             {
                                 AppendToFile = false,
                                 File = logfile.FullName,
                                 Layout = patternLayout,
                                 MaxSizeRollBackups = 5,
                                 MaximumFileSize = "1GB",
                                 RollingStyle = RollingFileAppender.RollingMode.Size,
                                 StaticLogFileName = true
                             };
            roller.ActivateOptions();
            return roller;
        }

        #endregion Methods
    }
}