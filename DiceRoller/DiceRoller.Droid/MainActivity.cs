using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DiceRoller.Models;

namespace DiceRoller.Droid
{
	[Activity (Label = "DiceRoller.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        ListView dieList;
        TextView dieResult;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
            InitializeViews();
		}

        protected void InitializeViews()
        {
            dieResult = FindViewById<TextView>(Resource.Id.dieResult);
            CreateDieList();
        }

        protected void CreateDieList()
        {
            GenericDie[] dice = RollHelper.InitializeGenericDice();
            dieList = FindViewById<ListView>(Resource.Id.dieList);

            dieList.Adapter = new DiceLayoutAdapter(this, dice);
            dieList.ItemClick += (sender, args) => {
                var result = dice[args.Position].RollDie().ToString();
                dieResult.Text = result;
            };
        }
    }
}


