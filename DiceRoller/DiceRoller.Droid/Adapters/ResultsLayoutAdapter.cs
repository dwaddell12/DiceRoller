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

        private class ViewHolder : Java.Lang.Object
        {
            internal TextView resultDie;
            internal ImageView resultIcon;
            internal TextView resultSide;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder = null;
            if (convertView == null)
            {
                holder = new ViewHolder();
                convertView = context.LayoutInflater.Inflate(Resource.Layout.Grid_ResultItem, null);
                holder.resultDie = convertView.FindViewById<TextView>(Resource.Id.Result_DiceName);
                holder.resultIcon = convertView.FindViewById<ImageView>(Resource.Id.Result_Icon);
                holder.resultSide = convertView.FindViewById<TextView>(Resource.Id.Result_DiceSide);
                convertView.Tag = holder;
            }
            else
                holder = (ViewHolder) convertView.Tag;
            RollResult result = results[position];
            holder.resultDie.Text = result.Die.Name;
            holder.resultSide.Text = result.Side.Name;
            return convertView;
        }
    }
}