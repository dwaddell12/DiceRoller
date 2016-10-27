using DiceRoller.Models.Side;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models.Dice
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericDie : BaseDie
    {

        public GenericDie() : base()
        {

        }
        
        public GenericDie(List<BaseSide> sides) : base(sides)
        {

        }
    }
}
