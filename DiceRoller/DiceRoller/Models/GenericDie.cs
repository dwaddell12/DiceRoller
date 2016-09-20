using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models
{
    public class GenericDie : BaseObject
    {
        public int[] Values { get; set; }

        public int NumberOfSides
        {
            get
            {
                return Values.Length;
            }
        }

        public GenericDie(int[] values)
        {
            this.Values = values;
        }
    }
}