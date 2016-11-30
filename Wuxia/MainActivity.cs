
using Android.App;
using Android.OS;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Wuxia.Helper;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using static Wuxia.ViewHolders.MenuViewHodler;
using Wuxia.Adapters;
using System.Collections.Generic;
using Wuxia.Fragments;
using Android.Widget;
using System.IO;

namespace Wuxia
{
    [Activity(Label = "MyAttandance", MainLauncher = true, Theme = "@style/StandardTheme", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, IMenuClicked
    {
        private V7Toolbar toolbar;
        private NavigationView navigationView;
        private DrawerLayout drawerLayout;
        private ExpandableListView listview;
        private static Activity activity;
        private MenuAdapter mAdapter;
        private static Fragment runningFragment = new OneView();
        private SmootherDrawerToggle toggle;

        private Dictionary<string, string> mainFileAndFolders = new Dictionary<string, string>();
        private Dictionary<string, string> filesAndFolders = new Dictionary<string, string>();

        private string wuxiaFolder = "/Wuxia";
        private string readmeFolder = "/Wuxia/Readme";
        public string wuxiaPDFFodler = ""; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            activity = this;

            toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);

            navigationView = FindViewById<NavigationView>(Resource.Id.navigation_view);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.navigation_drawer);

            toggle = new SmootherDrawerToggle(this, drawerLayout, toolbar, Resource.String.openDrawer, Resource.String.closeDrawer);
            drawerLayout.AddDrawerListener(toggle);
            toggle.SyncState();

            listview = FindViewById<ExpandableListView>(Resource.Id.nav_menu);

            mAdapter = new MenuAdapter(ApplicationContext, mainFileAndFolders, filesAndFolders, this);
            listview.SetAdapter(mAdapter);

            replaceFragment(runningFragment);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        private void replaceFragment(Fragment frag)
        {
            FragmentManager.BeginTransaction().Replace(Resource.Id.container_frame, frag).AddToBackStack(null).Commit();
        }

        public void runReplaceFragment()
        {
            replaceFragment(runningFragment);
        }

        protected override void OnSaveInstanceState(Bundle bundle)
        {
            base.OnSaveInstanceState(bundle);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void clicked(string button)
        {
            drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
        }

        private void sweepFolder(bool SDCardExist)
        {
            var directory = new Java.IO.File((SDCardExist) ? Environment.ExternalStorageDirectory.ToString() : Environment.DataDirectory.ToString() + wuxiaFolder);
            var files = directory.ListFiles();
                
            foreach(var file in files)
            {
                isOurDirectory(file, null);
            }

        }

        private void establishedFolder(bool SDCardExist)
        {
            var directory = new Java.IO.File( (SDCardExist)? Environment.ExternalStorageDirectory.ToString() : Environment.DataDirectory.ToString() + wuxiaFolder);

            if (!directory.Exists())
            {
                directory.Mkdir();
                var Readme = new Java.IO.File((SDCardExist) ? Environment.ExternalStorageDirectory.ToString() : Environment.DataDirectory.ToString() + readmeFolder);

                if (!Readme.Exists())
                {
                    Readme.Mkdir();
                    var fileWithinMyDir = Path.Combine(Readme.Path);
                    using (var streamwriter = new StreamWriter(fileWithinMyDir))
                    {
                        streamwriter.WriteLine(Resource.String.ReadmeWelcome);
                        streamwriter.WriteLine("<< "+ directory.Path+" >>");
                        streamwriter.WriteLine(Resource.String.ReadmeExplanation);
                        streamwriter.WriteLine(Resource.String.ReadmeExplanation2);
                    }
                }
            }
        }


        private bool checkFolderExist(out bool SDCardExist)
        {
            bool result = false;
            SDCardExist = false;

            if (Environment.GetExternalStorageState(new Java.IO.File("/")) == null)
            {
                var directory = new Java.IO.File(Environment.DataDirectory.ToString(),"/Wuxia");
                result = (directory.Exists()) ? true : false;

            }else
            {
                SDCardExist = true;
                var directory = new Java.IO.File(Environment.ExternalStorageDirectory.ToString() + "/Wuxia");
                result = (directory.Exists()) ? true : false;
            }

            return result;
        }


        private void isOurDirectory(Java.IO.File path, Java.IO.File parentPath)
        {
            bool isFolder = false; 

            if (path.IsDirectory)
            {
                var files = path.ListFiles();
                
                foreach(var file in files)
                {
                    isOurDirectory(file, path);
                }

                isFolder = true;
            }

            if (parentPath != null)
            {
                filesAndFolders.Add(path.Path, parentPath.Path);
            }else
            {
                mainFileAndFolders.Add(path.Path, (isFolder)? "Folder" : "File");
            }
        }

    }
}

