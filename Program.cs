using MLBWidget;

var date = DateTime.Now.ToString("yyyy-MM-dd");
Console.WriteLine(date);
var provider = new MLBProvider();
var games = await provider.GetMostRecentGames();

foreach (var game in games)
{
	Console.WriteLine($"{game.Teams.Home.Team.Name} {game.Teams.Home.Score}");
	Console.WriteLine($"{game.Teams.Away.Team.Name} {game.Teams.Away.Score}");
}