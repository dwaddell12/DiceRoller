using DiceRoller.Models.Game;
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

        public GenericDie(BaseGame game) : base(game)
        {

        }
        
        public GenericDie(BaseGame game, List<BaseSide> sides) : base(game, sides)
        {

        }
    }
}
