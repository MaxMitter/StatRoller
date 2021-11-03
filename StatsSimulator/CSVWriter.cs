using System.IO;

namespace StatsSimulator
{
    public class CSVWriter
    {
        public static string FilePath { get; set; } = "default.csv";

        public static void Write(string data, string filePath = "")
        {
            if (!File.Exists(filePath))
                filePath = FilePath;

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(data);
            }
        }
    }
}