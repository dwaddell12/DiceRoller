using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DiceRoller.Models
{
    public class BaseGame : BaseObject
    {
        public List<BaseDie> Dice { get; set; }
        public bool HasDiceSum { get; set; }

        public BaseGame()
        {
            Dice = new List<BaseDie>();
        }

    }
}