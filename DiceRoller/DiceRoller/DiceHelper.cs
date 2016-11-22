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
        private static long UNIQUE_ID = 100000;
        /// <summary>
        /// A placeholder value holding a unique value for each Die object created
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static List<BaseDie> InitializeDice(BaseGame game)
        {
            var dice = new List<BaseDie>();
            switch (game.Name)
            {
                case "Dungeons & Dragons":
                    dice = InitializeGenericDice(game);
                    break;
                case "Betrayal at House on the Hill":
                    dice = InitializeBaothDice(game);
                    break;
                case "World of Darkness":
                    dice = InitializeWodDice(game);
                    break;
                default:
                    dice = InitializeGenericDice(game);
                    break;
            }
            return dice;
        }



        /// <summary>
        /// Creates eight generic dice used in typical Tabletop games
        /// </summary>
        public static List<BaseDie> InitializeGenericDice(BaseGame game)
        {
                return new List<BaseDie>
                {
                    CreateGenericDie(game, 2), CreateGenericDie(game, 4),
                    CreateGenericDie(game, 6), CreateGenericDie(game, 8),
                    CreateGenericDie(game, 10), CreateGenericDie(game, 12),
                    CreateGenericDie(game, 20), CreateGenericDie(game, 100)
                };
   
        }

        /// <summary>
        /// Creates a generic die that will have a number on each side corresponding to the number
        /// of sides on the die
        /// </summary>
        /// <param name="sides">The number of sides for the generic die</param>
        /// <returns>The generic die object with all values inserted</returns>
        private static GenericDie CreateGenericDie(BaseGame game, int sides)
        {
            GenericDie die = new GenericDie(game);
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < sides; i++)
            {
                //Each game, die, and side will have a unique Id value.
                //Until then, this temporary code is here.
                UNIQUE_ID++;
                die.Id = UNIQUE_ID;
                GenericSide side = new GenericSide(die);
                side.Id = i;
                side.Name = (i+1).ToString();
                die.Sides.Add(side);
            }
            die.Name = "D&D D" + sides.ToString();
            return die;
        }

        // BELOW ARE SAMPLE DICE WITH NO MEANINGFUL METHODS
        /// <summary>
        /// 
        /// </summary>
        public static List<BaseDie> InitializeBaothDice(BaseGame game)
        {
            List<BaseDie> dice = new List<BaseDie>();
            dice.Add(CreateBaothDice(game));
            return dice;
        }
        /// <summary>
        /// 
        /// </summary>
        private static BaseDie CreateBaothDice(BaseGame game)
        {
            GenericDie die = new GenericDie(game);
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < 3; i++)
            {
                UNIQUE_ID++;
                die.Id = UNIQUE_ID;
                GenericSide side = new GenericSide(die);
                //side.Id = i;
                side.Id = UNIQUE_ID;
                side.Name = (i).ToString();
                die.Sides.Add(side);
            }

            die.Name = "Betrayal Die";
            return die;
        }
            
        public static List<BaseDie> InitializeWodDice(BaseGame game)
        {
            List<BaseDie> dice = new List<BaseDie>();
            dice.Add(CreateWodDice(game));
            return dice;
        }
        private static BaseDie CreateWodDice(BaseGame game)
        {
            GenericDie die = new GenericDie(game);
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < 10; i++)
            {
                UNIQUE_ID++;
                GenericSide side = new GenericSide(die);
                //side.Id = i;
                side.Id = UNIQUE_ID;
                side.Name = (i + 1).ToString();
                if (i == 10)
                {
                    side.IsExploding = true;
                }
                die.Sides.Add(side);
            }
            die.Name = "WoD D10";
            return die;
        }
    }
}
