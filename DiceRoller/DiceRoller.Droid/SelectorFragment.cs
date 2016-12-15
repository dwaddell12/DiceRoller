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
using DiceRoller;
using DiceRoller.Models;
using SQLite;
using Newtonsoft.Json;

namespace DiceRoller.Droid
{
    public class SelectorFragment : Fragment
    {
        #region Private variables
        private Spinner gameSpinner;
        private ListView diceList;
        private Button rollButton;
        private Button clearButton;
        private LinearLayout diceBagView, persistingDiceBagView;
        private List<BaseDie> dice;
        private List<BaseDie> diceBag;
        private List<BaseGame> games;
        private long lastClickTimer = 0;
        private bool isDualPane;
        #endregion
        #region Constant strings
        private const string RETAINED_DATA = "Retained Data";
        private const string RESULTS = "Results";
        private const string DICEBAG = "Dice Bag";
        private const string DICEBAG_VIEW = "Dicebag View";
        private const string GAME_POSITION = "Game Position";
        #endregion
        
        public DiceRollerDeviceDB Database
        {
            get
            {
                var activity = (MainActivity) Activity;
                return activity.Database;
            }
        }
        
        //JSON SETTINGS
        //private static JsonSerializerSettings settings = new JsonSerializerSettings()
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    ConstructorHandling = ConstructorHandling.Default
        //};


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindContentViews();
            //if(savedInstanceState == null)
            //{
                diceBag = new List<BaseDie>();
            //}
            //else
            //{
            //    diceBag = JsonConvert.DeserializeObject<List<BaseDie>>(savedInstanceState.GetString(DICEBAG));
            //}

            //if (persistingDiceBagView != null && persistingDiceBagView.ChildCount > 0)
            //{
            //    int counter = persistingDiceBagView.ChildCount - 1;
            //    View child;
            //    for (int i = 0; i <= counter; i++)
            //    {
            //        child = persistingDiceBagView.GetChildAt(0);
            //        persistingDiceBagView.RemoveView(child);
            //        diceBagView.AddView(child);
            //    }
            //    //diceBagView.Invalidate();
            //}
            games = Database.GetAllGames();
            //games = new List<BaseGame>(DataController.InitializeGames());
            gameSpinner.Adapter = new GameLayoutAdapter(Activity, games);

            SetOnClickListeners();

            var detailsFrame = Activity.FindViewById<View>(Resource.Id.Results_FrameLayout);
            isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
            if (isDualPane)
                ShowDetails(new List<RollResult>());
        }

        #region Initializing Methods
        private void FindContentViews()
        {
            diceList = View.FindViewById<ListView>(Resource.Id.Dice_List);
            rollButton = View.FindViewById<Button>(Resource.Id.Roll_Button);
            clearButton = View.FindViewById<Button>(Resource.Id.Clear_Button);
            diceBagView = View.FindViewById<LinearLayout>(Resource.Id.Dice_Layout);
            gameSpinner = View.FindViewById<Spinner>(Resource.Id.Game_Spinner);
        }
        private void SetOnClickListeners()
        {
            gameSpinner.ItemSelected += GameSpinner_ItemSelected;
            diceList.ItemClick += DiceList_ItemClick;
            rollButton.Click += RollButton_Click;
            clearButton.Click += ClearButton_Click;
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearBag();
        } 
        private void RollButton_Click(object sender, EventArgs e)
        {
            List<RollResult> results = RollHelper.RollCollectedDice(diceBag);
            ShowDetails(results);
        }
        private void DiceList_ItemClick(object dieListSender, AdapterView.ItemClickEventArgs adapterArgs)
        {
            DiceLayoutAdapter adapter = (DiceLayoutAdapter)diceList.Adapter;
            BaseDie die = adapter.GetDieItem(adapterArgs.Position);
            Button dieButton;
            if (diceBagView.FindViewWithTag(die.Id) != null)
                dieButton = (Button)diceBagView.FindViewWithTag(die.Id);
            else
            {
                dieButton = new Button(Activity);
                dieButton.Tag = die.Id;
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
        }
        private void GameSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            dice = Database.GetDiceByGame(games[e.Position]);
            //dice = DataController.InitializeDice(games[e.Position]);
            diceList.Adapter = new DiceLayoutAdapter(Activity, dice);
        }
        #endregion

        private void ClearBag()
        {
            diceBag = new List<BaseDie>();
            diceBagView.RemoveAllViews();
        }
        private void ShowDetails(List<RollResult> results)
        {
            if (isDualPane)
            {
                // We can display everything in place with fragments.
                // Have the list highlight this item and show the data.
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
                //There is a fragment and it needs new details.
                else
                {
                    details = ResultsFragment.NewInstance(results);
                    //intent.SetClass(Activity, typeof(ResultsActivity));
                    //details.FragmentManager.StartActivity(intent);
                    //details.OnActivityCreated(new Bundle());
                }
            }
            else
            {
                // Otherwise we need to launch a new Activity to display
                // the dialog fragment with selected text.
                Intent intent = new Intent();
                // We have to customize our settings in one way for the JsonSerializer.
                // We need to ignore Self-Refer
                intent.PutExtra(RESULTS, JsonConvert.SerializeObject(results));
                intent.SetClass(Activity, typeof(ResultsActivity));
                StartActivity(intent);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            persistingDiceBagView = diceBagView;
            outState.PutString(DICEBAG, JsonConvert.SerializeObject(diceBag));
            outState.PutInt(GAME_POSITION, gameSpinner.LastVisiblePosition);
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.Fragment_Selector, container, false);
        }
    }
}