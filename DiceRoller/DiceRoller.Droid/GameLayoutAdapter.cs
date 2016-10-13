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

using DiceRoller.Models.Game;

namespace DiceRoller.Droid
{
    public class GameLayoutAdapter : BaseAdapter<BaseGame>
    {
        BaseGame[] games;
        Activity context;

        public GameLayoutAdapter(Activity context, BaseGame[] games) : base()
        {
            this.context = context;
            this.games = games;
        }
        public override BaseGame this[int position]
        {
            get
            {
                return games[position];
            }
        }

        public override int Count
        {
            get
            {
                return games.Length;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.Spinner_GameItem, null);
            }
            var _gameName = view.FindViewById<TextView>(Resource.Id.Game_Title);
            var _gameIcon = view.FindViewById<ImageView>(Resource.Id.Game_Icon);
            _gameName.Text = games[position].Name;
            
            //_gameIcon.SetImageDrawable(null);
            return view;
        }
    }
}