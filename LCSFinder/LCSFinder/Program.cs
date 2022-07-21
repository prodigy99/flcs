using System.Diagnostics;

namespace LCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] file1 = File.ReadAllLines(".\\trace_log_arm.txt");
            string[] file2 = File.ReadAllLines(".\\trace_log_i386.txt");
            uint[] file1_ = new uint[file1.Length];
            uint[] file2_ = new uint[file2.Length];
            for (int i = 0; i < file1.Length; ++i)
            {
                file1_[i] = uint.Parse(file1[i], System.Globalization.NumberStyles.HexNumber);
            }
            for (int i = 0; i < file2.Length; ++i)
            {
                file2_[i] = uint.Parse(file2[i], System.Globalization.NumberStyles.HexNumber);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            LCSFinder<uint> finder = new(file1_, file2_);
            finder.AnalyzeLCS();
            stopwatch.Stop();

            Console.WriteLine($"LCS算法耗时：{stopwatch.Elapsed.Seconds}s");
            Console.WriteLine($"LCS长度：{finder.LongestCommonSequence.Count}");
        }
    }
}