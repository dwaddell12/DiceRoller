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
	[Activity (Label = "DiceRoller", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private const string RETAINED_DATA = "Retained Data";

        SelectorFragment selectorFragment;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.Activity_Main);
            //if (selectorFragment == null)
            //{
            //    selectorFragment = SelectorFragment.NewInstance();
            //    var fragmentTransaction = FragmentManager.BeginTransaction();
            //    fragmentTransaction.Add(Android.Resource.Id.Content, selectorFragment);
            //    fragmentTransaction.Commit();
            //}
            //else
            //{
            //    selectorFragment = FragmentManager.GetFragment<SelectorFragment>(bundle, RETAINED_DATA);
            //}
            // DetailsFragment.NewInstance is a factory method to create a Details Fragment
            //if (bundle != null)
            //{
            //    //var replacedFrag = FragmentManager.FindFragmentById<SelectorFragment>(Resource.Id.Selector_Fragment);
            //    selectorFragment = FragmentManager.GetFragment<SelectorFragment>(bundle, RETAINED_DATA);
            //    //FragmentManager.BeginTransaction().Attach(selectorFragment);
            //    //FragmentTransaction transaction = FragmentManager.BeginTransaction();
            //    //transaction.Replace(Resource.Id.Selector_Fragment, selectorFragment);
            //    //transaction.Remove(replacedFrag);
            //    //transaction.Commit();
            //}
            //if (selectorFragment == null)
            //{
            //    selectorFragment = FragmentManager.FindFragmentById<SelectorFragment>(Resource.Id.Selector_Fragment);
            //    selectorFragment.RetainInstance = true;
            //}
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        }



        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            //FragmentManager.PutFragment(outState, RETAINED_DATA, selectorFragment);
        }
    }
}


