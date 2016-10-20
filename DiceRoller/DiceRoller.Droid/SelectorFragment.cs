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
using DiceRoller.Models.Game;

namespace DiceRoller.Droid
{
    public class SelectorFragment : Fragment
    {
        Spinner _gameSpinner;
        ListView _dieList;
        Button _rollButton;
        List<BaseDie> _dice;
        List<BaseGame> _games;

        private const string CURRENT_POSITION = "Current Position";
        private const string RESULTS = "Results";
        int _currentPosition;
        bool _isDualPane;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _gameSpinner = Activity.FindViewById<Spinner>(Resource.Id.Game_Spinner);
            _dieList = Activity.FindViewById<ListView>(Resource.Id.Die_List);
            _rollButton = Activity.FindViewById<Button>(Resource.Id.Roll_Button);

            _dice = DiceHelper.InitializeGenericDice();

            _games = new List<BaseGame>(DiceHelper.InitializeGames());
            _gameSpinner.Adapter = new GameLayoutAdapter(Activity, _games);
            _gameSpinner.ItemSelected += _gameSpinner_ItemSelected;
            _gameSpinner.SetSelection(0);
            _rollButton.Click += _rollButton_Click;
            if (savedInstanceState != null)
            {
                _currentPosition = savedInstanceState.GetInt(CURRENT_POSITION, 0);
            }
            var detailsFrame = Activity.FindViewById<View>(Resource.Id.Results_FrameLayout);
            _isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
            if (_isDualPane)
            {
                _dieList.ChoiceMode = ChoiceMode.Single;
                ShowDetails();
            }
        }

        private void _rollButton_Click(object sender, EventArgs e)
        {
            List<BaseDie> diceToRoll = new List<BaseDie>();
            for (int i = 0; i < _dieList.Count; i++)
            {
                DiceLayoutAdapter adapter = (DiceLayoutAdapter) _dieList.Adapter;
                var currentDie = adapter.GetDieItem(i);
                var numberField = adapter.GetDiceAmount(i);
                _rollButton.Text += (currentDie.Name + numberField + " ").ToString();

            }
        }

        private void _gameSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _dieList.Adapter = new DiceLayoutAdapter(Activity, _games[e.Position].Dice);
        }
       
        private void ShowDetails()
        {
            if (_isDualPane)
            {
                // We can display everything in place with fragments.
                // Have the list highlight this item and show the data.
                //_dieList.SetItemChecked(pos, true);
                // Check what fragment is shown, replace if needed.
                var details = FragmentManager.FindFragmentById(Resource.Id.ResultItem_Layout) as ResultsFragment;
                if (details == null)
                {
                    // Make new fragment to show this selection.
                    details = ResultsFragment.NewInstance();
                    // Execute a transaction, replacing any existing
                    // fragment with this one inside the frame.
                    var ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.ResultItem_Layout, details);
                    ft.SetTransition(FragmentTransit.FragmentFade);
                    ft.Commit();
                }
            }
            else
            {
                // Otherwise we need to launch a new Activity to display
                // the dialog fragment with selected text.
                var intent = new Intent();
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