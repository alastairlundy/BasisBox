using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCount.Library.Models
{
    public class WCountResult
    {
        public ulong WordCount { get; set; }
        public ulong CharCount { get; set; }
        public ulong ByteCount { get; set; }

        public int LineCount { get; set; }
    }
}