using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using DiceRoller.Models;

namespace DiceRoller
{
    public class DiceRollerDeviceDB
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public DiceRollerDeviceDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            CreateTables();
            if (!IsDatabasePopulated())
                PopulateDatabase();
        }

        /// <summary>
        /// Create or Ignore tables
        /// </summary>
        private void CreateTables()
        {
            database.CreateTable<BaseGame>();
            database.CreateTable<BaseDie>();
            database.CreateTable<BaseSide>();
        }
        private bool IsDatabasePopulated()
        {
            var list = GetCurrentGame();
            return list != null;
        }
        private void PopulateDatabase()
        {
            var games = DataController.InitializeGames();
            // Inserts the data for the BaseGame table.
            database.InsertAll(games);
            var dice = new List<BaseDie>();
            foreach (BaseGame game in games)
            {
                dice = DataController.InitializeDice(game);
                // Inserts the data for the BaseDie table.
                database.InsertAll(dice);
                var sides = new List<BaseSide>();
                foreach (BaseDie die in dice)
                {
                    sides = die.Sides;
                    // Inserts the data for the BaseSide table.
                    database.InsertAll(sides);
                }
            }
        }

        #region BaseGame Methods
        public BaseGame GetCurrentGame()
        {
            lock (locker)
            {
                return database.Query<BaseGame>("SELECT * FROM [BaseGame]").FirstOrDefault();
            }
        }
        public List<BaseGame> GetAllGames()
        {
            lock (locker)
            {
                return database.Query<BaseGame>("SELECT * FROM [BaseGame]");
            }
        }
        public void SaveGame(BaseGame item, bool IsUpdate)
        {
            lock (locker)
            {
                if (IsUpdate)
                    database.Update(item);
                else
                {
                    DeleteGame(item);
                    database.Insert(item);
                }
            }
        }
        public void DeleteGame(BaseGame item)
        {
            lock (locker)
            {
                database.Delete<BaseGame>(item.Id);
            }
        }
        #endregion

        #region BaseDie Methods
        public BaseDie GetCurrentDie()
        {
            lock (locker)
            {
                return database.Query<BaseDie>("SELECT * FROM [BaseDie]").FirstOrDefault();
            }
        }
        public List<BaseDie> GetDiceByGame(BaseGame game)
        {
            lock (locker)
            {
                return database.Query<BaseDie>("SELECT * FROM [BaseDie] WHERE Game = ?", game);
            }
        }
        public void SaveDie(BaseDie item, bool IsUpdate)
        {
            lock (locker)
            {
                if (IsUpdate)
                    database.Update(item);
                else
                {
                    DeleteDie(item);
                    database.Insert(item);
                }
            }
        }
        public void DeleteDie(BaseDie item)
        {
            lock (locker)
            {
                database.Delete<BaseDie>(item.Id);
            }
        }
        #endregion

        #region BaseSide Methods
        public BaseSide GetCurrentSide()
        {
            lock (locker)
            {
                return database.Query<BaseSide>("SELECT * FROM [BaseSide]").FirstOrDefault();
            }
        }
        public List<BaseSide> GetSidesByDie(BaseDie die)
        {
            lock (locker)
            {
                return database.Query<BaseSide>("SELECT * FROM [BaseSide] WHERE [BaseDie] = '" + die + "'");
            }
        }
        public void SaveSide(BaseSide item, bool IsUpdate)
        {
            lock (locker)
            {
                if (IsUpdate)
                    database.Update(item);
                else
                {
                    DeleteSide(item);
                    database.Insert(item);
                }
            }
        }
        public void DeleteSide(BaseSide item)
        {
            lock (locker)
            {
                database.Delete<BaseSide>(item.Id);
            }
        }
        #endregion
        
        
    }
}

