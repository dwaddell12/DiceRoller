﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DiceRoller.Models;
using DiceRoller.Models.Dice;

namespace DiceRoller.Droid
{
	[Activity (Label = "DiceRoller.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private const string RESULT = "Result";

        /// <summary>
        /// 
        /// </summary>
        private ListView dieList;
        /// <summary>
        /// 
        /// </summary>
        private TextView dieResult;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"></param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
            dieResult = FindViewById<TextView>(Resource.Id.dieResult);
            if(bundle != null)
            {
                dieResult.Text = bundle.GetString(RESULT);
            }
            InitializeViews();
        }

        /// <summary>
        /// Sets up the views of the MainActivity.
        /// </summary>
        protected void InitializeViews()
        {
            CreateGenericDieList();
        }

        /// <summary>
        /// Sets up a list view that is populated with generic dice that display a result when tapped.
        /// </summary>
        protected void CreateGenericDieList()
        {
            GenericDie[] dice = RollHelper.InitializeGenericDice();
            dieList = FindViewById<ListView>(Resource.Id.dieList);

            dieList.Adapter = new DiceLayoutAdapter(this, dice);
            dieList.ItemClick += (sender, args) => {
                var result = dice[args.Position].RollDie().ToString();
                dieResult.Text = result;
            };
        }
        /// <summary>
        /// Collects pertainant data from the MainActivity
        /// </summary>
        /// <param name="outState">A bundle of data that is to persist.</param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString(RESULT, dieResult.Text.ToString());
        }
    }
}


