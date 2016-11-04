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
using DiceRoller.Models.Dice;
using DiceRoller.Models.Game;
using Newtonsoft.Json;

namespace DiceRoller.Droid
{
    
    public class SelectorFragment : Fragment
    {
        Spinner gameSpinner;
        ListView diceList;
        Button rollButton;
        Button clearButton;
        List<BaseDie> dice, diceBag;
        List<BaseGame> games;
        LinearLayout diceBagView;
        long lastClickTimer = 0;

        private const string RESULTS = "Results";
        bool isDualPane;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            gameSpinner = Activity.FindViewById<Spinner>(Resource.Id.Game_Spinner);
            diceList = Activity.FindViewById<ListView>(Resource.Id.Die_List);
            rollButton = Activity.FindViewById<Button>(Resource.Id.Roll_Button);
            clearButton = Activity.FindViewById<Button>(Resource.Id.Clear_Button);
            diceBag = new List<BaseDie>();
            diceList.ItemClick += (dieListSender, adapterArgs) =>
            {
                DiceLayoutAdapter adapter = (DiceLayoutAdapter)diceList.Adapter;
                BaseDie die = adapter.GetDieItem(adapterArgs.Position);
                Button dieButton;
                
                //I use the Id from the AdapterArgs as a tag for recognizing buttons
                //in the horizontal scroll at the bottom.
                long id = adapterArgs.Id;
                if (diceBagView.FindViewWithTag(id) != null)
                    dieButton = (Button)diceBagView.FindViewWithTag(id);
                else
                {
                    dieButton = new Button(Activity);
                    dieButton.Tag = id;
                    diceBagView.AddView(dieButton, 0);
                }
                diceBag.Add(die);
                dieButton.Text = die.Name + " x" + (diceBag.Where(m => m.Name == die.Name).Count());
                dieButton.Click += (buttonSender, buttonArgs) =>
                {
                    //This lastClicked timer is implemented as a result of multiple clicks
                    //being registered sequentially. Stops clicks before 100ms from registering.
                    if (SystemClock.ElapsedRealtime() - lastClickTimer > 100)
                    {
                        lastClickTimer = SystemClock.ElapsedRealtime();
                        diceBag.Remove(die);
                        if (diceBag.Contains(die))
                            dieButton.Text = die.Name + " x" + (diceBag.Where(m => m.Name == die.Name).Count());
                        else
                            diceBagView.RemoveView(dieButton);
                    }
                };
            };

            diceBagView = Activity.FindViewById<LinearLayout>(Resource.Id.Dice_Layout);
            dice = DiceHelper.InitializeGenericDice;

            games = new List<BaseGame>(DiceHelper.InitializeGames());
            gameSpinner.Adapter = new GameLayoutAdapter(Activity, games);
            gameSpinner.ItemSelected += _gameSpinner_ItemSelected;
            gameSpinner.SetSelection(0);
            rollButton.Click += RollButton_Click;
            clearButton.Click += ClearButton_Click;
            var detailsFrame = Activity.FindViewById<View>(Resource.Id.Results_FrameLayout);
            isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
            if (isDualPane)
                ShowDetails(new List<RollResult>());
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            diceBag = new List<BaseDie>();
            diceBagView.RemoveAllViews();
            rollButton.Text = "Roll";
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            List<RollResult> results = RollHelper.RollCollectedDice(diceBag);
            ShowDetails(results);
            //for (int i = 0; i < diceList.Count; i++)
            //{
            //    //List<RollResult> results = RollHelper.RollCollectedDice(diceBag);
            //    //rollButton.Text = "";
            //    //ShowDetails(results);
            //    //TODO: Send results to new activity.
            //    //foreach (RollResult result in results)
            //    //    rollButton.Text += result.Side.Name + " from " + result.Die.Name + "\n";

            //}
        }

        private void _gameSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            diceBag = new List<BaseDie>();
            diceBagView.RemoveAllViews();
            diceList.Adapter = new DiceLayoutAdapter(Activity, games[e.Position].Dice);
        }
        
        private void ShowDetails(List<RollResult> results)
        {
            if (isDualPane)
            {
                // We can display everything in place with fragments.
                // Have the list highlight this item and show the data.
                //_dieList.SetItemChecked(pos, true);
                // Check what fragment is shown, replace if needed.
                var details = FragmentManager.FindFragmentById(Resource.Id.Results_FrameLayout) as ResultsFragment;
                if (details == null)
                {
                    // Make new fragment to show this selection.
                    details = ResultsFragment.NewInstance(results);
                    // Execute a transaction, replacing any existing
                    // fragment with this one inside the frame.
                    var ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.Results_FrameLayout, details);
                    ft.SetTransition(FragmentTransit.FragmentFade);
                    ft.Commit();
                }
                else
                {
                    details = ResultsFragment.NewInstance(results);

                    //var intent = new Intent();
                    //JsonSerializerSettings settings = new JsonSerializerSettings();
                    //settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //intent.PutExtra(RESULTS, JsonConvert.SerializeObject(results, settings));
                    
                    //intent.SetClass(Activity, typeof(ResultsActivity));
                    //details.FragmentManager.StartActivity(intent);
                    //details.OnActivityCreated(new Bundle());
                }
            }
            else
            {
                // Otherwise we need to launch a new Activity to display
                // the dialog fragment with selected text.
                var intent = new Intent();
                // We have to customize our settings in one way for the JsonSerializer.
                // We need to ignore Self-Refer
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                intent.PutExtra(RESULTS, JsonConvert.SerializeObject(results, settings));
                intent.SetClass(Activity, typeof(ResultsActivity));
                StartActivity(intent);
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.Fragment_Selector, container, true);
        }
    }
}