using DiceRoller.Models.Dice;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models.Side
{
    public abstract class BaseSide : BaseObject
    {
        public BaseDie Die { get; set; }
        public bool IsExploding { get; set; }

        public BaseSide (BaseDie die)
        {
            Die = die;
            IsExploding = false;
        }
    }
}
