using DiceRoller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiceRoller
{
    /// <summary>
    /// A controller used for handling the data of the application.
    /// </summary>
    public static class DataController
    {
        public static string DatabasePath
        {
            get
            {
                var sqliteFilename = "DiceRollerDeviceDB.db3";
#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
#elif __ANDROID__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
#else // WinPhone
                var path = Path.Combine("", sqliteFilename);
#endif
                return path;
            }
        }

        public static List<BaseGame> InitializeGames()
        {
            var games = new List<BaseGame>();
            games.Add(new BaseGame() { Name = "Dungeons & Dragons", HasDiceSum = false });
            games.Add(new BaseGame() { Name = "Betrayal at House on the Hill", HasDiceSum = true });
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
                    dice = InitializeBaseDice(game);
                    break;
                case "Betrayal at House on the Hill":
                    dice = InitializeBetrayalDice(game);
                    break;
                default:
                    dice = InitializeBaseDice(game);
                    break;
            }
            return dice;
        }

        /// <summary>
        /// Creates eight generic dice used in typical Tabletop games
        /// </summary>
        public static List<BaseDie> InitializeBaseDice(BaseGame game)
        {
            return new List<BaseDie>
                {
                    InitializeBaseSides(game, 2), InitializeBaseSides(game, 4),
                    InitializeBaseSides(game, 6), InitializeBaseSides(game, 8),
                    InitializeBaseSides(game, 10), InitializeBaseSides(game, 12),
                    InitializeBaseSides(game, 20), InitializeBaseSides(game, 100)
                };

        }
        /// <summary>
        /// Creates a generic die that will have a number on each side corresponding to the number
        /// of sides on the die
        /// </summary>
        /// <param name="sides">The number of sides for the generic die</param>
        /// <returns>The generic die object with all values inserted</returns>
        private static BaseDie InitializeBaseSides(BaseGame game, int sides)
        {
            BaseDie die = new BaseDie(game);
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < sides; i++)
            {
                //Each game, die, and side will have a unique Id value.
                //Until then, this temporary code is here.
                BaseSide side = new BaseSide(die);
                side.Name = (i + 1).ToString();
                die.Sides.Add(side);
            }
            die.Name = "D&D D" + sides.ToString();
            return die;
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<BaseDie> InitializeBetrayalDice(BaseGame game)
        {
            List<BaseDie> dice = new List<BaseDie>();
            dice.Add(InitializeBetrayalSides(game));
            return dice;
        }
        /// <summary>
        /// 
        /// </summary>
        private static BaseDie InitializeBetrayalSides(BaseGame game)
        {
            BaseDie die = new BaseDie(game);
            die.Sides = new List<BaseSide>();
            for (int i = 0; i < 3; i++)
            {
                BaseSide side = new BaseSide(die);
                side.Name = (i).ToString();
                die.Sides.Add(side);
            }
            die.Name = "Betrayal Die";
            return die;
        }
    }
}
