using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerCLI
{
    public class CommandLineOptions
    {
        [Option("rom", Required = true, HelpText = "path to the base rom file")]
        public string BaseRomFilename { get; set; }

        [Option("seed", Required = false, DefaultValue = "", HelpText = "seed number")]
        public string SeedNumber { get; set; }

        [Option("base", Required = true, HelpText = "path to the base2patched.json")]
        public string BasePatchJsonFilename { get; set; }

        [Option("randomizer", Required = true, HelpText = "path to the randomizerPatch.json")]
        public string RandomizerPatchJsonFilename { get; set; }

        [Option("enemizer", Required = true, HelpText = "path to the enemizerOptions.json")]
        public string EnemizerOptionsJsonFilename { get; set; }

        [Option("output", Required = true, HelpText = "path to the outputPatch.json")]
        public string OutputPatchJsonFilename { get; set; }
    }
}
