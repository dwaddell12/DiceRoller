using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models
{
    /// <summary>
    /// A generic die is a common die who's sides are gradually iterating numbers
    /// </summary>
    public class GenericDie : BaseObject, IRollable
    {
        int lowerBound = 0,
            upperBound,
            result;
        Random rnd;
        /// <summary>
        /// A number represented by side of the die
        /// </summary>
        public int[] Values { get; set; }
 
        /// <summary>
        /// Property to show how many sides a generic die has
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return Values.Length;
            }
        }

        /// <summary>
        /// Rolls a generic die that will return one of the die's values
        /// </summary>
        /// <param name="die">The die to be rolled</param>
        /// <returns>The result of the generic die</returns>
        public string RollDie()
        {
            rnd = new Random();
            upperBound = NumberOfSides;
            result = Values[rnd.Next(lowerBound, upperBound)];
            return result.ToString();
        }
        /// <summary>
        /// A constructor for a generic die object
        /// </summary>
        /// <param name="values">An array of values for the generic die</param>
        public GenericDie(int[] values)
        {
            this.Values = values;
        }
    }
}