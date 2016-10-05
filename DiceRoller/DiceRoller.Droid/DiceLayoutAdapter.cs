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
    public class DiceLayoutAdapter : BaseAdapter<GenericDie>
    {
        GenericDie[] dice;
        Activity context;

        public DiceLayoutAdapter(Activity context, GenericDie[] dice) : base() {
            this.context = context;
            this.dice = dice;
        }
        public override GenericDie this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.DieListItem, null);
            }
            view.FindViewById<TextView>(Resource.Id.diceName).Text = dice[position].Name;
            return view;
        }
    }
}