using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DiceRoller.Models;
using Newtonsoft.Json;

namespace DiceRoller.Droid
{
    [Activity(Label = "Results", ParentActivity = typeof(MainActivity))]
    public class ResultsActivity : Activity
    {
        private const string RESULTS = "Results";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            List<RollResult> results;
            if (Intent.HasExtra(RESULTS))
                results = JsonConvert.DeserializeObject<List<RollResult>>(Intent.GetStringExtra(RESULTS));
            else
                results = new List<RollResult>();
            var details = ResultsFragment.NewInstance(results);
            // DetailsFragment.NewInstance is a factory method to create a Details Fragment
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Android.Resource.Id.Content, details);
            fragmentTransaction.Commit();
            // Create your application here
        }
    }
}