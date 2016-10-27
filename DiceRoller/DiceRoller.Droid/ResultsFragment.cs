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

namespace DiceRoller.Droid
{
    public class ResultsFragment : Fragment
    {
        TextView _resultName;
        TextView _resultSide;
        private const string RESULTS = "Results";
        public static ResultsFragment NewInstance()
        {
            return new ResultsFragment { Arguments = new Bundle() };
        }
        

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            if (container == null)
            {
                // Currently in a layout without a container, so no reason to create our view.
                return null;
            }
            GridView grid = container.FindViewById<GridView>(Resource.Id.Result_Grid);
            List<RollResult> results = new List<RollResult>();
            ResultsLayoutAdapter adapter = new ResultsLayoutAdapter(Activity, results);
            grid.Adapter = adapter;
            return container;
        }
    }
}