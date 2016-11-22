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
using DiceRoller.Models.Dice;

namespace DiceRoller.Droid
{
    public class RetainedFragment : Fragment
    {
        public LinearLayout DiceBagView { get; set; }
        public List<BaseDie> DiceBag { get; set; }
        
        private const string RETAINED_DATA = "Retained Data";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SelectorFragment dataFragment = FragmentManager.FindFragmentByTag<SelectorFragment>(RETAINED_DATA);
            // Create your fragment here
            if (dataFragment == null)
            {
                dataFragment = new SelectorFragment();
                FragmentManager.BeginTransaction().Add(dataFragment, RETAINED_DATA).Commit();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}