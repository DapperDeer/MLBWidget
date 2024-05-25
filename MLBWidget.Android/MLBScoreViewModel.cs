using AndroidX.Lifecycle;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MLBWidget.Android
{
	public class MLBScoreViewModel : ViewModel
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly MLBClient _mlbClient = new MLBClient();

		public MLBScoreViewModel()
		{
			Task.Run(async () => await GetUpdatedScores());
		}

		public string TeamOneName
		{
			get { return _teamOneName; }
			set
			{
				if (value != _teamOneName)
				{
					_teamOneName = value;
					OnPropertyChanged(nameof(TeamOneName));
				}
			}
		}
		private string _teamOneName;

		public string TeamTwoName
		{
			get { return _teamTwoName; }
			set
			{
				if (value != _teamTwoName)
				{
					_teamTwoName = value;
					OnPropertyChanged(nameof(TeamTwoName));
				}
			}
		}
		private string _teamTwoName;

		public int TeamOneScore
		{
			get { return _teamOneScore; }
			set
			{
				if (value != _teamOneScore)
				{
					_teamOneScore = value;
					OnPropertyChanged(nameof(TeamOneScore));
				}
			}
		}
		private int _teamOneScore;

		public int TeamTwoScore
		{
			get { return _teamTwoScore; }
			set
			{
				if (value != _teamTwoScore)
				{
					_teamTwoScore = value;
					OnPropertyChanged(nameof(TeamTwoScore));
				}
			}
		}
		private int _teamTwoScore;

		public async Task GetUpdatedScores()
		{
			var game = await _mlbClient.GetMostRecentGame();
			if (game != null)
			{
				TeamOneName = game.Teams.Home.Team.Name;
				TeamTwoName = game.Teams.Away.Team.Name;
				TeamOneScore = game.Teams.Home.Score;
				TeamTwoScore = game.Teams.Away.Score;
			}
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}