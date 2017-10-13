using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace PDFMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi there! Lets sort this PDF out!");
            Console.Write("Enter path to odd pages: ");
            string odd = GetPdfPath();

            Console.Write("Enter path to even pages: ");
            string even = GetPdfPath();

            Console.WriteLine("Thanks! Merging now...");

            string output = Path.Combine(Path.GetDirectoryName(odd), "merged.pdf");

            string cmd = $"/C pdftk A=\"{odd}\" B=\"{even}\" shuffle A Bend-1 output \"{output}\"";
            Process.Start("CMD.exe", cmd);
            Console.WriteLine("Thank you bye!");
        }

        private static string TrimQuotes(string input) {
            if(Regex.IsMatch(input, "^\"(.*)\"$"))
                input = input.Substring(1, input.Length - 2);

            return input;
        }

        private static string GetPdfPath() {
            string path = TrimQuotes(Console.ReadLine());

            while(!File.Exists(path) || Path.GetExtension(path) != ".pdf") {
                Console.Write("Whoops, that PDF couldn't be found, try again: ");
                path = TrimQuotes(Console.ReadLine());
            }

            return path;
        }
    }
}
