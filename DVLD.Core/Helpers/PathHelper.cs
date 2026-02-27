using System;
using System.IO;

namespace DVLD.Core.Helpers
{
    public static class PathHelper
    {
        public static readonly string ProgramDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DVLD");

        public static readonly string ImagesFolderPath = Path.Combine(ProgramDataPath, "Images");

        public static readonly string LoggingFolderPath = Path.Combine(ProgramDataPath, "Logs");
    }
}
