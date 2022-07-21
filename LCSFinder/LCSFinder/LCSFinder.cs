using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LCS
{
    internal class LCSFinder<T> where T : IComparable
    {
        public T[] SequenceA { get; }
        public T[] SequenceB { get; }

        public ushort[,] LCSTable { get; private set; }
        public LongBitArray DirectionTable { get; private set; }

        private Stack<T> lcs = new();
        private Stack<int> aIndex = new();
        private Stack<int> bIndex = new();

        public List<T> LongestCommonSequence
        {
            get
            {
                return new List<T>(lcs);
            }
        }

        public List<int> LCSElementIndexInSequenceA
        {
            get
            {
                return new List<int>(aIndex);
            }
        }

        public List<int> LCSElementIndexInSequenceB
        {
            get
            {
                return new List<int>(bIndex);
            }
        }

        public LCSFinder(T[] sequenceA, T[] sequenceB)
        {
            if (sequenceA.Length > ushort.MaxValue)
            {
                throw new ArgumentException("数组长度过长，应小于ushort类型的最大长度。", nameof(sequenceA));
            }
            else if (sequenceB.Length > ushort.MaxValue)
            {
                throw new ArgumentException("数组长度过长，应小于ushort类型的最大长度。", nameof(sequenceB));
            }
            else
            {
                SequenceA = sequenceA;
                SequenceB = sequenceB;
                LCSTable = new ushort[SequenceA.Length + 1, SequenceB.Length + 1];
                DirectionTable = new LongBitArray((long)(SequenceA.Length + 1) * (long)(SequenceB.Length + 1) * 2);
            }
        }

        public void AnalyzeLCS()
        {
            for (int i = 0; i <= SequenceA.Length; ++i)
            {
                for (int j = 0; j <= SequenceB.Length; ++j)
                {
                    long p = (i * (long)(SequenceB.Length + 1) + j) * 2;
                    if (i == 0 || j == 0)
                    {
                        LCSTable[i, j] = 0;
                    }
                    else
                    {
                        if (SequenceA[i - 1].CompareTo(SequenceB[j - 1]) == 0)
                        {
                            LCSTable[i, j] = (ushort)(LCSTable[i - 1, j - 1] + 1);
                            // 左上
                            DirectionTable[p] = true; // 向上
                            DirectionTable[p + 1] = true; // 向左
                        }
                        else
                        {
                            LCSTable[i, j] = Math.Max(LCSTable[i - 1, j], LCSTable[i, j - 1]);
                            if (LCSTable[i - 1, j] > LCSTable[i, j - 1])
                            {
                                // 正上
                                DirectionTable[p] = true; // 向上
                                DirectionTable[p + 1] = false;
                            }
                            else if (LCSTable[i - 1, j] == LCSTable[i, j - 1])
                            {
                                // 正上
                                DirectionTable[p] = true; // 向上
                                DirectionTable[p + 1] = false;
                            }
                            else if (LCSTable[i - 1, j] < LCSTable[i, j - 1])
                            {
                                // 左面
                                DirectionTable[p] = false;
                                DirectionTable[p + 1] = true; // 向左
                            }
                        }
                    }
                }
            }
        }

        // 输出结果
        public void showLCS(bool onlyLCS, bool l1, bool l2)
        {

            using StreamWriter file = new("result.txt");
            lcs.Clear();
            aIndex.Clear();
            bIndex.Clear();

            int i = SequenceA.Length;
            int j = SequenceB.Length;
            while (i != 0 && j != 0)
            {
                if (SequenceA[i - 1].CompareTo(SequenceB[j - 1]) == 0)
                {
                    lcs.Push(SequenceA[i - 1]);
                    aIndex.Push(i - 1);
                    bIndex.Push(j - 1);
                }

                long p = (i * (long)(SequenceB.Length + 1) + j) * 2;
                if (DirectionTable[p] && DirectionTable[p + 1])
                {
                    --i;
                    --j;
                }
                else if (DirectionTable[p] && !DirectionTable[p + 1])
                {
                    --i;
                }
                else if (!DirectionTable[p] && DirectionTable[p + 1])
                {
                    --j;
                }
            }
            if (onlyLCS)
            {
                while (lcs.Count > 0)
                {
                    T str = lcs.Pop();
                    file.WriteLine(str);
                    Console.WriteLine(str);
                }
            }

            if (l1)
            {
                while (aIndex.Count > 0)
                {
                    int str = aIndex.Pop();
                    file.WriteLine(str);
                    Console.WriteLine(str);
                }
            }

            if (l2)
            {
                while (bIndex.Count > 0)
                {
                    int str = bIndex.Pop();
                    file.WriteLine(str);
                    Console.WriteLine(str);
                }
            }

        }
    }
}
