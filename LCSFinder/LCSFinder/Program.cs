using System.Diagnostics;

namespace LCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath1="",filePath2=""; // 文件路径
            bool l1, l2; // l1,l2 参数

            if (args.Length < 2) // 非法参数
            {
                throw new Exception("参数异常");
            }
            else
            {
                filePath1 = args[0];
                filePath2 = args[1];

                l1 = args.Contains("-l1");
                l2 = args.Contains("-l2");

                if (l1 && l2)
                {
                    throw new Exception("参数异常，l1,l2 不能同时启用");
                }
            }
            
            string[] file1 = File.ReadAllLines(filePath1);
            string[] file2 = File.ReadAllLines(filePath2);
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
            LCSFinder<String> finder = new(file1, file2);
            finder.AnalyzeLCS();
            finder.showLCS(!l1 && !l2,l1,l2);
            stopwatch.Stop();

            Console.WriteLine($"LCS算法耗时：{stopwatch.Elapsed.Seconds}s");
            Console.WriteLine($"LCS长度：{finder.LongestCommonSequence.Count}");
            Console.WriteLine("结果保存在程序目录下的result文件中");
        }
    }
}