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
        private const string RESULT = "Result";

        ListView dieList;
        TextView dieResult;

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
        /// Sets up the views of the MainActivity
        /// </summary>
        protected void InitializeViews()
        {
            CreateGenericDieList();
        }

        /// <summary>
        /// Sets up a list view that is populated with generic dice that display a result when tapped
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

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString(RESULT, dieResult.Text.ToString());
        }
    }
}


