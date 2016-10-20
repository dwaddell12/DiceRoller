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
        TextView _dieName;
        ImageView _dieIcon;
        EditText _dieAmount;


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

        public int GetDiceAmount(int position)
        {
            
            if(_dieAmount.Text == "")
            {
                return 0;
            }
            return int.Parse(_dieAmount.Text);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.List_DieItem, null);
            }
            _dieName = view.FindViewById<TextView>(Resource.Id.Dice_Name);
            _dieIcon = view.FindViewById<ImageView>(Resource.Id.Dice_Icon);
            _dieAmount = view.FindViewById<EditText>(Resource.Id.Dice_Amount);

            _dieName.Text = dice[position].Name;
            _dieAmount.TextChanged += _dieAmount_TextChanged;
            //To be implemented
            //_dieIcon.SetImageDrawable(null);
            return view;
        }

        private void _dieAmount_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            
        }
    }
}