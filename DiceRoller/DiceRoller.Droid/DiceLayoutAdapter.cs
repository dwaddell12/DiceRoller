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
    public class DiceLayoutAdapter : BaseAdapter<BaseDie>
    {
        BaseDie[] dice;
        Activity context;

        public DiceLayoutAdapter(Activity context, BaseDie[] dice) : base() {
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
                view = context.LayoutInflater.Inflate(Resource.Layout.List_DieItem, null);
            }
            var _dieName = view.FindViewById<TextView>(Resource.Id.Dice_Name);
            var _dieIcon = view.FindViewById<ImageView>(Resource.Id.Dice_Icon);
            var _dieAmount = view.FindViewById<EditText>(Resource.Id.Dice_Amount);
            _dieName.Text = dice[position].Name;
            //To be implemented
            //_dieIcon.SetImageDrawable(null);
            return view;
        }
    }
}