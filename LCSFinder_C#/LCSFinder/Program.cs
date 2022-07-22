using System.Diagnostics;

namespace LCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath1, filePath2; // 文件路径
            bool l1, l2; // l1,l2 参数

            try
            {
                filePath1 = args[args.Length - 1];
                filePath2 = args[args.Length - 2];
            }
            catch
            {
                Console.WriteLine("参数缺失。");
                PrintHelp();
                return;
            }

            l1 = args.Contains("--l1");
            l2 = args.Contains("--l2");

            string[] file1, file2;
            uint[] file1_, file2_;
            try
            {
                file1 = File.ReadAllLines(filePath1);
                file2 = File.ReadAllLines(filePath2);
            }
            catch
            {
                Console.WriteLine("文件读取失败。");
                PrintHelp();
                return;
            }

            try
            {
                file1_ = new uint[file1.Length];
                file2_ = new uint[file2.Length];

                for (int i = 0; i < file1.Length; ++i)
                {
                    file1_[i] = uint.Parse(file1[i], System.Globalization.NumberStyles.HexNumber);
                }
                for (int i = 0; i < file2.Length; ++i)
                {
                    file2_[i] = uint.Parse(file2[i], System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch
            {
                Console.WriteLine("文件的每行都应该是16进制整数（不超过uint）");
                PrintHelp();
                return;
            }
            

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            LCSFinder<uint> finder = new(file1_, file2_);
            finder.AnalyzeLCS();
            stopwatch.Stop();

            Console.WriteLine($"LCS算法耗时：{stopwatch.Elapsed.Seconds}s");
            Console.WriteLine($"LCS长度：{finder.LongestCommonSequence.Count}");

            File.WriteAllText(".\\lcs_result.txt", string.Join(Environment.NewLine, finder.LongestCommonSequence));
            if (l1)
            {
                File.WriteAllText(".\\l1_result.txt", string.Join(Environment.NewLine, finder.LCSElementIndexInSequenceA));
            }
            if (l2)
            {
                File.WriteAllText(".\\l2_result.txt", string.Join(Environment.NewLine, finder.LCSElementIndexInSequenceA));
            }

            Console.WriteLine($"结果已输出到*_result.txt文件中。");
        }

        /// <summary>
        /// 打印帮助信息
        /// </summary>
        private static void PrintHelp()
        {
            Console.WriteLine("暂无帮助信息。");
        }
    }
}