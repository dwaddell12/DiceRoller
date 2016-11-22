using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DiceRoller.Models;
using DiceRoller.Models.Side;
using Newtonsoft.Json;

namespace DiceRoller.Droid
{
    public class ResultsFragment : Fragment
    {
        GridView resultGrid;
        List<RollResult> results;

        private const string RESULTS = "Results";

        public static ResultsFragment NewInstance(List<RollResult> rolls)
        {
            var details = new ResultsFragment { Arguments = new Bundle() };
            return details;
        }
        
        // Maybe need an UpdateInstance();

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Intent intent = Activity.Intent;
            resultGrid = Activity.FindViewById<GridView>(Resource.Id.Result_Grid);
            if (intent.HasExtra(RESULTS))
                results = JsonConvert.DeserializeObject<List<RollResult>>(intent.GetStringExtra(RESULTS));
            else
                results = new List<RollResult>();
            resultGrid.Adapter = new ResultsLayoutAdapter(Activity, results);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.Fragment_Results, container, false);
        }
    }
}