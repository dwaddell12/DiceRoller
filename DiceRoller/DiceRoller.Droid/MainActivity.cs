using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DiceRoller.Models;
using DiceRoller.Models.Dice;
using System.Collections.Generic;

namespace DiceRoller.Droid
{
	[Activity (Label = "DiceRoller.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"></param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Activity_Main);
        }
        /// <summary>
        /// Sets up a list view that is populated with generic dice that display a result when tapped.
        /// </summary>
        private void CreateGenericDieList()
        {
            /*
            List<BaseDie> dice = DiceHelper.InitializeGenericDice();
            dieList = FindViewById<ListView>(Resource.Id.Die_List);

            dieList.Adapter = new DiceLayoutAdapter(this, dice);
            dieList.ItemClick += (sender, args) => {
                var result = dice[args.Position].RollDie();
                dieResult.Text = result.ToString();
            };
            */
        }
        /// <summary>
        /// Collects pertainant data from the MainActivity
        /// </summary>
        /// <param name="outState">A bundle of data that is to persist.</param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            //outState.PutString(RESULTS, dieResult.Text.ToString());
        }
    }
}


