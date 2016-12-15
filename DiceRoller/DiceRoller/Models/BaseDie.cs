using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DiceRoller.Models
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

        [NotNull]
        /// <summary>
        /// The value at the position represented by side of the die.
        /// </summary>
        public List<BaseSide> Sides { get; set; }
        [NotNull]
        public BaseGame Game { get; }
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
        /// 
        /// </summary>
        public BaseDie()
        {
            Game = new BaseGame();
            Sides = new List<BaseSide>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sides"></param>
        public BaseDie(BaseGame game)
        {
            Game = game;
            Sides = new List<BaseSide>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="sides"></param>
        public BaseDie(BaseGame game, List<BaseSide> sides)
        {
            Game = game;
            Sides = sides;
        }
    }
}