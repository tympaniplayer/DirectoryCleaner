using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using CommandLine;

namespace CleanDirectories {
  public class Program {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    static void Main( string[] args ) {
      ConfigureNlog();
      Logger.Info( "Log configured" );

      CommandLineOptions options = new CommandLineOptions();
      if ( !Parser.Default.ParseArgumentsStrict( args, options ) ) {
        Logger.Error( "Command line arguments could not be parsed" );
        Environment.Exit( 1 );
      }

      if ( !Directory.Exists( options.SearchDirectory ) ) {
        Logger.Error( "Path does not exist" );
        Environment.Exit( 2 );
      }

      DirectoryCleaner.CleanDirectories( options.SearchDirectory, options.SearchPattern );
    }

    private static void ConfigureNlog() {
      var target = new ConsoleTarget {
        Layout = new SimpleLayout( "${time} | ${level:uppercase=true} | ${message}" )
      };

      var rule = new LoggingRule( "*", target );
      rule.EnableLoggingForLevel( LogLevel.Debug );
      rule.EnableLoggingForLevel( LogLevel.Trace );
      rule.EnableLoggingForLevel( LogLevel.Error );
      rule.EnableLoggingForLevel( LogLevel.Info );

      if ( LogManager.Configuration == null ) {
        LogManager.Configuration = new LoggingConfiguration();
      }

      LogManager.Configuration.AddTarget( "console", target );
      LogManager.Configuration.LoggingRules.Add( rule );
      LogManager.ReconfigExistingLoggers();
    }
  }
}
