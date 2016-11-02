﻿using DiceRoller.Models;
using DiceRoller.Models.Dice;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller
{
    public static class RollHelper
    {
        public static List<RollResult> RollCollectedDice(List<BaseDie> dice)
        {
            List<RollResult> results = new List<RollResult>();
            foreach (BaseDie die in dice)
            {
                RollResult result = new RollResult(die);
                results.Add(result);
            }
            return results;
        }
    }
}