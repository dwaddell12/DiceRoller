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

using DiceRoller.Models.Dice;
using DiceRoller.Models;

namespace DiceRoller.Droid
{
    public class ResultsLayoutAdapter : BaseAdapter<RollResult>
    {
        List<RollResult> results;
        Activity context;

        public ResultsLayoutAdapter(Activity _context, List<RollResult> _results) : base() {
            context = _context;
            results = _results;
        }
        public override RollResult this[int position]
        {
            get
            {
                return results[position];
            }
        }

        public override int Count
        {
            get
            {
                return results.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.Grid_ResultItem, null);
            }
            
            TextView name = view.FindViewById<TextView>(Resource.Id.Result_DiceName);
            ImageView icon = view.FindViewById<ImageView>(Resource.Id.Result_Icon);
            TextView side = view.FindViewById<TextView>(Resource.Id.Result_DiceSide);
            RollResult result = results[0];

            name.Text = result.Die.Name;
            side.Text = result.Side.Name;
            return view;
        }
    }
}