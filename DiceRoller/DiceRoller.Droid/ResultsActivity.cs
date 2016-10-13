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

namespace DiceRoller.Droid
{
    [Activity(Label = "ResultsActivity")]
    public class ResultsActivity : Activity
    {
        private const string RESULTS = "Results";
        private const string CURRENT_POSITION = "Current Position";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var index = Intent.Extras.GetInt(RESULTS, 0);

            var details = ResultsFragment.NewInstance(index);
            // DetailsFragment.NewInstance is a factory method to create a Details Fragment
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Android.Resource.Id.Content, details);
            fragmentTransaction.Commit();
            // Create your application here
        }
    }
}