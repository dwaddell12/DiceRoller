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

namespace DiceRoller.Droid
{
    public class ResultsFragment : Fragment
    {
        TextView resultText;
        private const string CURRENT_POSITION = "Current Position";
        private const string RESULTS = "Results";
        public static ResultsFragment NewInstance(int pos)
        {
            var detailsFrag = new ResultsFragment { Arguments = new Bundle() };
            detailsFrag.Arguments.PutInt(CURRENT_POSITION, pos);
            return detailsFrag;
        }

        public int ShownResults
        {
            get { return Arguments.GetInt(CURRENT_POSITION, 0); }
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
            var grid = new GridView(Activity);
            //grid.Adapter
            //var text = new TextView(Activity);
            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            //text.SetPadding(padding, padding, padding, padding);
            //text.TextSize = 24;
            //text.Text = Shakespeare.Dialogue[ShownPlayId];
            //scroller.AddView(text);
            return grid;
        }
    }
}