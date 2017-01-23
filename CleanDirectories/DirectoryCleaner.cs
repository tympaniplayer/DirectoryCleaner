using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace CleanDirectories {
  public static class DirectoryCleaner {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public static void CleanDirectories( string searchDirectory, string searchPattern ) {
      Logger.Info($"Marking {searchDirectory} as not readonly");
      var directoryInfo = new DirectoryInfo(searchDirectory);
      directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

      var svnDirectories = Directory.GetDirectories( searchDirectory, searchPattern ).ToList();
      Logger.Info( $"Deleting directories that match {searchPattern} in {searchDirectory}" );
      svnDirectories.ForEach( a => Directory.Delete(a, true) );

      foreach ( var directory in Directory.GetDirectories( searchDirectory ) ) {
        Logger.Info( $"Running clean directories in {directory}" );
        CleanDirectories( directory, searchPattern );
        Logger.Info( $"Finished running clean in {directory}" );
      }
      Logger.Info( "Finished cleaning all directories" );
    }
  }
}
