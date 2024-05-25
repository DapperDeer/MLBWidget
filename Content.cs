using System.Text.Json.Serialization;

namespace MLBWidget
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class Away
	{
		public LeagueRecord LeagueRecord { get; set; }
		public int Score { get; set; }
		public Team Team { get; set; }
		public bool IsWinner { get; set; }
		public bool SplitSquad { get; set; }
		public int SeriesNumber { get; set; }
	}

	public class Content
	{
		public string Link { get; set; }
	}

	public class Date
	{
		public string date { get; set; }
		public int TotalItems { get; set; }
		public int TotalEvents { get; set; }
		public int TotalGames { get; set; }
		public int TotalGamesInProgress { get; set; }
		public List<Game> Games { get; set; }
		public List<object> Events { get; set; }
	}

	public class Game
	{
		public int GamePk { get; set; }
		public string GameGuid { get; set; }
		public string Link { get; set; }
		public string GameType { get; set; }
		public string Season { get; set; }
		public DateTime GameDate { get; set; }
		public string OfficialDate { get; set; }
		public Status Status { get; set; }
		public Teams Teams { get; set; }
		public Venue Venue { get; set; }
		public Content Content { get; set; }
		public bool IsTie { get; set; }
		public int GameNumber { get; set; }
		public bool PublicFacing { get; set; }
		public string DoubleHeader { get; set; }
		public string GamedayType { get; set; }
		public string Tiebreaker { get; set; }
		public string CalendarEventID { get; set; }
		public string SeasonDisplay { get; set; }
		public string DayNight { get; set; }
		public int ScheduledInnings { get; set; }
		public bool ReverseHomeAwayStatus { get; set; }
		public int InningBreakLength { get; set; }
		public int GamesInSeries { get; set; }
		public int SeriesGameNumber { get; set; }
		public string SeriesDescription { get; set; }
		public string RecordSource { get; set; }
		public string IfNecessary { get; set; }
		public string IfNecessaryDescription { get; set; }
	}

	public class Home
	{
		public LeagueRecord LeagueRecord { get; set; }
		public int Score { get; set; }
		public Team Team { get; set; }
		public bool IsWinner { get; set; }
		public bool SplitSquad { get; set; }
		public int SeriesNumber { get; set; }
	}

	public class LeagueRecord
	{
		public int Wins { get; set; }
		public int Losses { get; set; }
		public string Pct { get; set; }
	}

	public class Root
	{
		public string Copyright { get; set; }
		public int TotalItems { get; set; }
		public int TotalEvents { get; set; }
		public int TotalGames { get; set; }
		public int TotalGamesInProgress { get; set; }
		public List<Date> Dates { get; set; }
	}

	public class Status
	{
		public string AbstractGameState { get; set; }
		public string CodedGameState { get; set; }
		public string DetailedState { get; set; }
		public string StatusCode { get; set; }
		public bool StartTimeTBD { get; set; }
		public string AbstractGameCode { get; set; }
	}

	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
	}

	public class Teams
	{
		public Away Away { get; set; }
		public Home Home { get; set; }
	}

	public class Venue
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
	}
}
