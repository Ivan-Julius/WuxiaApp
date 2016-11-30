
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Wuxia.ViewHolders.MenuViewHodler;
using System;

namespace Wuxia.Adapters
{
    class MenuAdapter : BaseExpandableListAdapter
    {
        private static string LOG_TAG = typeof(MenuAdapter).Name;
        private List<int> mList;
        private LayoutInflater inflater;
        private IMenuClicked delegator;

        public MenuAdapter(Context context, Dictionary<string, string> mainFileFolder, Dictionary<string, string> filesAndFolders, IMenuClicked delegator)
        {
            string methodName = "Constructor";
            Log.Debug(LOG_TAG, string.Format("{0}: --- start ---", methodName));
            this.inflater = LayoutInflater.From(context);
            this.delegator = delegator;

            Log.Debug(LOG_TAG, string.Format("{0}: --- end ---", methodName));
        }

        public override int GroupCount
        {
            get
            {
                return 0;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return 0;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return 0;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 0;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return 0;
        }

        public override long GetGroupId(int groupPosition)
        {
            return 0;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}