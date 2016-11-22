using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models
{
    /// <summary>
    /// Basic object that holds properties for an Id and a Name
    /// </summary>
    public abstract class BaseObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
