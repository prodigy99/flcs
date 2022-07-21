using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS
{
    public class LongBitArray
    {
        public BitArray[] BitArrays { get; set; }
        public long Length { get; }

        public bool this[long index] 
        { 
            get 
            {
                int partition = (int)(index / int.MaxValue);
                int offset = (int)(index % int.MaxValue);

                return BitArrays[partition][offset];
            }
            set 
            {
                int partition = (int)(index / int.MaxValue);
                int offset = (int)(index % int.MaxValue);

                BitArrays[partition][offset] = value;
            }
        }
        public LongBitArray(long length)
        {
            if(length > 17_179_869_176) // 8 * int.MaxValue
            {
                throw new ArgumentException("数据量过大", nameof(length));
            }

            int arrayLength = (int)(length / int.MaxValue);
            if (length % int.MaxValue == 0)
            {
                /* 可以整除 */
                BitArrays = new BitArray[arrayLength];
            }
            else
            {
                /* 不能整除 */
                BitArrays = new BitArray[arrayLength + 1];
                BitArrays[arrayLength] = new BitArray((int)(length % int.MaxValue));
            }
            for (int i = 0; i < arrayLength; i++)
            {
                BitArrays[i] = new BitArray(int.MaxValue);
            }

            Length = length;
        }
    }
}
