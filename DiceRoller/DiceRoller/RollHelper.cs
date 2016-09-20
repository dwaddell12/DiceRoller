using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models;

namespace DiceRoller
{
    static class RollHelper
    {
        /// <summary>
        /// A placeholder value holding a unique value for each Die object created
        /// </summary>
        private static int _id = 0;
        /// <summary>
        /// Creates eight generic dice used in typical Tabletop games
        /// </summary>
        /// <returns>An array of generic dice</returns>
        public static GenericDie[] InitializeGenericDice()
        {
            return new GenericDie[]
            {
                CreateGenericDie(2), CreateGenericDie(4), CreateGenericDie(6),
                CreateGenericDie(8), CreateGenericDie(10), CreateGenericDie(12),
                CreateGenericDie(20), CreateGenericDie(100)
            };
        }

        /// <summary>
        /// Creates a generic die that will have a number on each side corresponding to the number
        /// of sides on the die
        /// </summary>
        /// <param name="sides">The number of sides for the generic die</param>
        /// <returns>The generic die object with all values inserted</returns>
        public static GenericDie CreateGenericDie(int sides)
        {
            int[] values = new int[sides];
            for (int i = 0; i < sides; i++)
            {
                values[i] = i+1;
            }
            GenericDie die = new GenericDie(values) { Id = _id, Name = "D" + sides };
            _id++;
            return die;
        }
    }
}
