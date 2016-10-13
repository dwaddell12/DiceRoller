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

namespace DiceRoller.Droid
{
    public class ResultsLayoutAdapter : BaseAdapter<BaseDie>
    {
        BaseDie[] dice;
        Activity context;

        public ResultsLayoutAdapter(Activity context, BaseDie[] dice) : base() {
            this.context = context;
            this.dice = dice;
        }
        public override BaseDie this[int position]
        {
            get
            {
                return dice[position];
            }
        }

        public override int Count
        {
            get
            {
                return dice.Length;
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
            
            view.FindViewById<TextView>(Resource.Id.Result_DiceName);
            view.FindViewById<ImageView>(Resource.Id.Result_Icon);
            view.FindViewById<TextView>(Resource.Id.Result_DiceSide);
            //view.FindViewById<TextView>(Resource.Id.Dice_Name).Text = dice[position].Name;
            //view.FindViewById<ImageView>(Resource.Id.diceIcon).SetImageIcon()
            return view;
        }
    }
}