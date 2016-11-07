using System;
using System.Collections.Generic;
using System.Text;
using DiceRoller.Models.Dice;
using DiceRoller.Models.Game;
using DiceRoller.Models.Side;

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
        //private static int _id = 0;

        public static List<BaseGame> InitializeGames()
        {
            var games = new List<BaseGame>();
            GenericGame dnd = new GenericGame();
            games.Add(new GenericGame() { Name = "Dungeons & Dragons" });
            games.Add(new GenericGame() { Name = "Betrayal at House on the Hill" });
            games.Add(new GenericGame { Name = "World of Darkness" });
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
                case "Dungeons & Dragons":
                    dice = InitializeGenericDice;
                    break;
                case "Betrayal at House on the Hill":
                    dice = InitializeBaothDice;
                    break;
                case "World of Darkness":
                    dice = InitializeWodDice;
                    break;
                default:
                    dice = InitializeGenericDice;
                    break;
            }
            return dice;
        }



        /// <summary>
        /// Creates eight generic dice used in typical Tabletop games
        /// </summary>
        public static List<BaseDie> InitializeGenericDice
        {
            get
            {
                return new List<BaseDie>
                {
                    CreateGenericDie(2), CreateGenericDie(4), CreateGenericDie(6),
                    CreateGenericDie(8), CreateGenericDie(10), CreateGenericDie(12),
                    CreateGenericDie(20), CreateGenericDie(100)
                };
            }
            
        }

        /// <summary>
        /// Creates a generic die that will have a number on each side corresponding to the number
        /// of sides on the die
        /// </summary>
        /// <param name="sides">The number of sides for the generic die</param>
        /// <returns>The generic die object with all values inserted</returns>
        private static GenericDie CreateGenericDie(int sides)
        {
            GenericDie die = new GenericDie();
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < sides; i++)
            {
                GenericSide side = new GenericSide(die);
                side.Id = i;
                side.Name = (i+1).ToString();
                die.Sides.Add(side);
            }
            die.Name = "D&D d" + sides.ToString();
            return die;
        }

        // BELOW ARE SAMPLE DICE WITH NO MEANINGFUL METHODS

        public static List<BaseDie> InitializeBaothDice
        {
            get
            {
                List<BaseDie> dice = new List<BaseDie>();
                dice.Add(CreateBaothDice);
                return dice;
            }

        }

        private static BaseDie CreateBaothDice
        {
            get
            {
                GenericDie die = new GenericDie();
                die.Sides = new List<BaseSide>();
                for (int i = 0; i < 3; i++)
                {
                    GenericSide side = new GenericSide(die);
                    side.Id = i;
                    side.Name = (i).ToString();
                    die.Sides.Add(side);
                }

                die.Name = "Betrayal Die";
                return die;
            }
            
        }

        public static List<BaseDie> InitializeWodDice
        {
            get
            {
                List<BaseDie> dice = new List<BaseDie>();
                dice.Add(CreateWodDice);
                return dice;
            }
            
        }

        private static BaseDie CreateWodDice
        {
            get
            {
                GenericDie die = new GenericDie();
                die.Sides = new List<BaseSide>();
                for (int i = 0; i < 10; i++)
                {
                    GenericSide side = new GenericSide(die);
                    side.Id = i;
                    side.Name = (i + 1).ToString();
                    if (i == 10)
                    {
                        side.IsExploding = true;
                    }
                    die.Sides.Add(side);
                }
                die.Name = "Wod d10";
                return die;
            }
        }
    }
}
