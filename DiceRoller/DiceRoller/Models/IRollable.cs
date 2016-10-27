using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller.Models
{
    /// <summary>
    /// 
    /// </summary>
    interface IRollable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        RollResult RollDie();
    }
}
