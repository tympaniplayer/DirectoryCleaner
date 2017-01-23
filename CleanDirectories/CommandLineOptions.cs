using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace CleanDirectories {
  public class CommandLineOptions {
    [Option( 'd', "directory", Required = true, HelpText = "Full path of the directory to search and clean" )]
    public string SearchDirectory { get; set; }

    [Option( 'p', "pattern", Required = true, HelpText = "The search pattern of the directories to delete" )]
    public string SearchPattern { get; set; }
  }
}
