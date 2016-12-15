using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DiceRoller.Models
{
    /// <summary>
    /// Basic object that holds properties for an Id and a Name
    /// </summary>
    public abstract class BaseObject
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; }
        public string Name { get; set; }
    }
}
