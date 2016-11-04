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
using Java.Lang;

namespace DiceRoller.Droid
{
    public class DiceLayoutAdapter : BaseAdapter<BaseDie>
    {
        List<BaseDie> dice;
        Activity context;

        public DiceLayoutAdapter(Activity context, List<BaseDie> dice) : base() {
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
                return dice.Count;
            }
        }

        public BaseDie GetDieItem(int position)
        {
            return dice[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        //public int GetDiceAmount(int position)
        //{
        //    var view = GetView(position, null, null);
        //    EditText _dieAmount = view.FindViewById<EditText>(Resource.Id.Dice_Amount);
        //    string amountEntry = _dieAmount.Text;
        //    if (amountEntry == "" || amountEntry == null)
        //    {
        //        return 0;
        //    }
        //    return int.Parse(amountEntry);
        //}

        private class ViewHolder : Java.Lang.Object
        {
            internal TextView dieName;
            internal ImageView dieIcon;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder = null;
            if (convertView == null)
            {
                holder = new ViewHolder();
                convertView = context.LayoutInflater.Inflate(Resource.Layout.List_DieItem, null);
                holder.dieName = convertView.FindViewById<TextView>(Resource.Id.Dice_Name);
                holder.dieIcon = convertView.FindViewById<ImageView>(Resource.Id.Dice_Icon);
                convertView.Tag = holder;
            }
            else
                holder = (ViewHolder) convertView.Tag;
            holder.dieName.Text = dice[position].Name;
            //To be implemented
            //_dieIcon.SetImageDrawable(null);
            return convertView;
        }
    }
}