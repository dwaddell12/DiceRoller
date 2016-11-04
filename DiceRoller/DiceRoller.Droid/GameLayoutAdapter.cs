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
        List<BaseGame> games;
        Activity context;

        public GameLayoutAdapter(Activity context, List<BaseGame> games) : base()
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
                return games.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        private class ViewHolder : Java.Lang.Object
        {
            internal TextView gameName;
            internal ImageView gameIcon;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder = null;
            if (convertView == null)
            {
                holder = new ViewHolder();
                convertView = context.LayoutInflater.Inflate(Resource.Layout.Spinner_GameItem, null);
                holder.gameName = convertView.FindViewById<TextView>(Resource.Id.Game_Title);
                holder.gameIcon = convertView.FindViewById<ImageView>(Resource.Id.Game_Icon);
                convertView.Tag = holder;
            }
            else
                holder = (ViewHolder) convertView.Tag;
            holder.gameName.Text = games[position].Name;
            //_gameIcon.SetImageDrawable(null);
            return convertView;
        }
    }
}