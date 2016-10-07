using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models;

namespace DiceRoller.Models.Dice
{
    /// <summary>
    /// A generic die is a common die who's sides are gradually iterating numbers
    /// </summary>
    public abstract class BaseDie : BaseObject, IRollable
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
        private string result;
        /// <summary>
        /// The random value for getting the result.
        /// </summary>
        private Random rand;
        /// <summary>
        /// The value at the position represented by side of the die.
        /// </summary>
        public string[] Values { get; set; }
        /// <summary>
        /// The property to show how many sides a generic die has.
        /// </summary>
        public int NumberOfSides { get { return Values.Length; } }

        /// <summary>
        /// Rolls a die that will return one of the die's values.
        /// </summary>
        /// <returns>The result of the die.</returns>
        public object RollDie()
        {
            rand = new Random();
            upperBound = NumberOfSides;
            result = Values[rand.Next(lowerBound, upperBound)];
            return result;
        }
        /// <summary>
        /// A constructor for a die object
        /// </summary>
        /// <param name="values">An array of values for the die.</param>
        public BaseDie(string[] values)
        {
            Values = values;
        }
    }
}