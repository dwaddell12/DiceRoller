using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models;
using DiceRoller.Models.Side;
using DiceRoller.Models.Game;

namespace DiceRoller.Models.Dice
{
    /// <summary>
    /// A generic die is a common die who's sides are gradually iterating numbers
    /// </summary>
    public class BaseDie : BaseObject
    {
        /// <summary>
        /// The lower bound of the value of the die sides.
        /// </summary>
        private int lowerBound = 0;
        /// <summary>
        /// The upper bound of the value of the die sides.
        /// </summary>
        private int upperBound;
        /// <summary>
        /// The result of the value of the die side.
        /// </summary>
        private RollResult result;

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        /// <summary>
        /// The value at the position represented by side of the die.
        /// </summary>
        public List<BaseSide> Sides { get; set; }
        public BaseGame Game { get; set; }
        /// <summary>
        /// The property to show how many sides a generic die has.
        /// </summary>
        public int NumberOfSides { get { return Sides.Count; } }

        /// <summary>
        /// Rolls a die that will return one of the die's values.
        /// </summary>
        /// <returns>The result of the die.</returns>
        public RollResult RollDie
        {
            get
            {
                lock (syncLock)
                {
                    upperBound = NumberOfSides;
                    result = new RollResult(Sides[random.Next(lowerBound, upperBound)]);
                    return result;
                }
                
            }
        }

        /// <summary>
        /// An empty constructor for a die object
        /// </summary>
        public BaseDie()
        {
            Sides = new List<BaseSide>();
        }
        /// <summary>
        /// A constructor for a die object
        /// </summary>
        /// <param name="values">An array of values for the die.</param>
        public BaseDie(List<BaseSide> sides)
        {
            Sides = sides;
        }
    }
}