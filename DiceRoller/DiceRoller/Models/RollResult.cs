using DiceRoller.Models.Dice;
using DiceRoller.Models.Side;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models
{
    public class RollResult : BaseObject
    {
        public BaseDie Die { get; set; }
        public BaseSide Side { get; set; }

        public RollResult(BaseSide side) : base()
        {
            Side = side;
            Die = side.Die;
        }
    }
}
