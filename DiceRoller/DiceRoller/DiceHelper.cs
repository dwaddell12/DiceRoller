using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models.Dice;
using DiceRoller.Models.Game;

namespace DiceRoller
{
    /// <summary>
    /// 
    /// </summary>
    public static class DiceHelper
    {
        /// <summary>
        /// A placeholder value holding a unique value for each Die object created
        /// </summary>
        private static int _id = 0;

        public static List<BaseGame> InitializeGames()
        {
            var games = new List<BaseGame>();
            GenericGame dnd = new GenericGame();
            games.Add(new GenericGame() { Name = "D&D" });
            games.Add(new GenericGame() { Name = "Betrayal at House on the Hill" });
            games.Add(new GenericGame { Name = "Mad Ferret's Super Cool, Somewhat Mad, Mostly Ferret Quest" });
            foreach (BaseGame game in games)
            {
                game.Dice = InitializeDice(game);
            }
            return games;
        }

        public static List<BaseDie> InitializeDice(BaseGame game)
        {
            var dice = new List<BaseDie>();
            switch (game.Name)
            {
                case "D&D":
                    dice = InitializeGenericDice();
                    break;
                case "Betrayal at House on the Hill":
                    dice = InitializeBaothDice();
                    break;
                case "Mad Ferret's Super Cool, Somewhat Mad, Mostly Ferret Quest":
                    dice = InitializeFerretDice();
                    break;
                default:
                    dice = InitializeGenericDice();
                    break;
            }
            return dice;
        }


        /// <summary>
        /// Creates eight generic dice used in typical Tabletop games
        /// </summary>
        /// <returns>A list of generic dice.</returns>
        public static List<BaseDie> InitializeGenericDice()
        {
            return new List<BaseDie>
            {
                CreateGenericDie(2), CreateGenericDie(4), CreateGenericDie(6),
                CreateGenericDie(8), CreateGenericDie(10), CreateGenericDie(12),
                CreateGenericDie(20), CreateGenericDie(100)
            };
        }
        
        // BELOW ARE SAMPLE DICE WITH NO MEANINGFUL METHODS

        public static List<BaseDie> InitializeBaothDice()
        {
            List<BaseDie> dice = new List<BaseDie>();
            dice.Add(CreateBaothDice());
            return dice;
        }

        private static BaseDie CreateBaothDice()
        {
            GenericDie die = new GenericDie(new string[] { "0", "0", "1", "1", "2", "2" });
            die.Name = "Betrayal Die";
            return die;
        }

        public static List<BaseDie> InitializeFerretDice()
        {
            List<BaseDie> dice = new List<BaseDie>();
            dice.Add(CreateFerretDice());
            return dice;
        }

        private static BaseDie CreateFerretDice()
        {
            GenericDie die = new GenericDie(new string[] { "Ferret", "Furret", "Farret", "Forret", "Firret", "Fyrret" });
            die.Name = "Ferret Die";
            return die;
        }

        /// <summary>
        /// Creates a generic die that will have a number on each side corresponding to the number
        /// of sides on the die
        /// </summary>
        /// <param name="sides">The number of sides for the generic die</param>
        /// <returns>The generic die object with all values inserted</returns>
        private static GenericDie CreateGenericDie(int sides)
        {
            string[] values = new string[sides];
            for (int i = 0; i < sides; i++)
            {
                values[i] = (i + 1).ToString();
            }
            GenericDie die = new GenericDie(values) { Id = _id, Name = "d" + sides };
            _id++;
            return die;
        }
    }
}
