using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DiceRoller.Models
{
    public class BaseSide : BaseObject
    {
        [NotNull]
        public BaseDie Die { get; }
        public bool IsExploding { get; set; }

        public BaseSide()
        {
            Die = new BaseDie();
        }

        public BaseSide (BaseDie die)
        {
            Die = die;
            IsExploding = false;
        }
    }
}