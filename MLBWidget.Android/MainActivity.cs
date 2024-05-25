using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Content.PM;
using Android.Widget;
using AndroidX.Lifecycle;
using Xamarin.Essentials;

namespace MLBWidget.Android
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		private Button _button;
		private TextView _textView;
		private MLBScoreViewModel _scoreViewModel;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += FabOnClick;

			_textView = FindViewById<TextView>(Resource.Id.textView1);
			var vmProvider = new ViewModelProvider(new ViewModelStore(), new ViewModelProvider.NewInstanceFactory());
			_scoreViewModel = (MLBScoreViewModel)vmProvider.Get(Java.Lang.Class.FromType(typeof(MLBScoreViewModel)));
			_scoreViewModel.PropertyChanged += ScoreViewModel_PropertyChanged;
			_button = FindViewById<Button>(Resource.Id.button1);
			_button.Click += async (sender, args) =>
			{
				await _scoreViewModel.GetUpdatedScores();
			};
		}

		protected override void OnResume()
		{
			base.OnResume();
			MainThread.BeginInvokeOnMainThread(() =>
			{
				_textView.Text = $"{_scoreViewModel.TeamOneScore} || {_scoreViewModel.TeamTwoScore}";
			});
		}

		protected override void OnPause()
		{
			base.OnPause();
			_scoreViewModel.PropertyChanged -= ScoreViewModel_PropertyChanged;
		}

		private void ScoreViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(_scoreViewModel.TeamOneScore) || e.PropertyName == nameof(_scoreViewModel.TeamTwoScore))
			{
				MainThread.BeginInvokeOnMainThread(() =>
				{
					_textView.Text = $"{_scoreViewModel.TeamOneName} {_scoreViewModel.TeamOneScore} || {_scoreViewModel.TeamTwoName} {_scoreViewModel.TeamTwoScore}";
				});
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_settings)
			{
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		private void FabOnClick(object sender, EventArgs eventArgs)
		{
			View view = (View)sender;
			Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
				.SetAction("Action", (View.IOnClickListener)null).Show();
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}
