using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models.Dice;

namespace DiceRoller.Models.Game
{
    public abstract class BaseGame : BaseObject
    {
        public BaseDie Dice { get; set; }
    }
}