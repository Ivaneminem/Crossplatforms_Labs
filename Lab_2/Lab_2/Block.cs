using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Block
    {
        public int InitialValue { get; set; }
        public int FinalValue { get; set; }

        public Block(int initial, int final)
        {
            InitialValue = initial;
            FinalValue = final;
        }
    }
}

