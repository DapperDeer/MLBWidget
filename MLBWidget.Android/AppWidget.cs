#nullable enable
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MLBWidget.Android
{
	[BroadcastReceiver(Label = "Dodgers Score", Exported = false)]
	[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
	[MetaData("android.appwidget.provider", Resource = "@layout/appwidgetprovider")]
	public class AppWidget : AppWidgetProvider
	{
		MLBClient _client = new MLBClient();
		private Game? _game;
		private RemoteViews _views;

		public AppWidget() : base()
		{
		}

		public AppWidget(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public override void OnEnabled(Context context)
		{
			base.OnEnabled(context);
		}

		public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
		{
			var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
			Task.Run(async () =>
			{
				_game = await _client.GetMostRecentGame();
				MainThread.BeginInvokeOnMainThread(() =>
				{
					_views = BuildRemoteViews(context, appWidgetIds);
					appWidgetManager.UpdateAppWidget(me, _views);
				});
			});
		}

		public override void OnReceive(Context context, Intent intent)
		{
			base.OnReceive(context, intent);
			var mgr = AppWidgetManager.GetInstance(context);
			OnUpdate(context, mgr, new[] { Resource.Layout.widget });
		}

		private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
		{
			var widgetView = new RemoteViews(context.PackageName, appWidgetIds[0]);
			if (_game != null)
			{
				widgetView.SetTextViewText(Resource.Id.teamOne, _game.Teams.Home.Team.Name ?? "Unlimited games, but ");
				widgetView.SetTextViewText(Resource.Id.teamOneScore, $"{_game.Teams.Home.Score}" ?? "no games.");
				widgetView.SetTextViewText(Resource.Id.teamTwo, _game.Teams.Away.Team.Name ?? "Unlimited games, but ");
				widgetView.SetTextViewText(Resource.Id.teamTwoScore, $"{_game.Teams.Away.Score}" ?? "no games.");
				widgetView.SetTextViewText(Resource.Id.teamTwoPitcher, $"Pitcher: {_game.Teams.Away.ProbablePitcher.FullName}" ?? "no games.");
				widgetView.SetTextViewText(Resource.Id.teamOnePitcher, $"Pitcher: {_game.Teams.Home.ProbablePitcher.FullName}" ?? "no games.");
			}

			//Refresh button logic
			var intent = new Intent(context, typeof(AppWidget));
			intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
			intent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetIds);
			var piRefresh = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent);
			widgetView.SetOnClickPendingIntent(Resource.Id.widgetBackground, piRefresh);
			return widgetView;
		}
	}
}