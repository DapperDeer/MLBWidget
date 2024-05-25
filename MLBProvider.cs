using System.Text.Json;
using System.Text.Json.Nodes;

namespace MLBWidget
{
	public class MLBProvider
	{
		// https://statsapi.mlb.com/api/v1/schedule?sportId=1&startDate=2024-04-26&endDate=2024-04-26&hydrate=broadcasts(all),game(content(media(epg)),editorial(preview,recap)),linescore,team,probablePitcher(note)
		private readonly Uri _apiUrl = new Uri("https://statsapi.mlb.com");
		private const string _hydrate = "hydrate=broadcasts(all),game(content(media(epg)),editorial(preview,recap)),linescore,team,probablePitcher(note)";

		public async Task<IEnumerable<Game>> GetMostRecentGames()
		{
			using var client = new HttpClient() { BaseAddress = _apiUrl };
			var date = DateTime.Now.ToString("yyyy-MM-dd");
			string requestUri = $@"api/v1/schedule?sportId=1&startDate={date}&endDate={date}&{_hydrate}";
			try
			{
				var request = await client.GetAsync(requestUri);
				var json = await request.Content.ReadAsStringAsync();
				var deserialized = JsonSerializer.Deserialize<Root>(json, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true });
				if (deserialized != null)
				{
					var games = deserialized.Dates.SelectMany(dates => dates.Games);
					var dodgersGames = games.Where(game => game.Teams.Away.Team.Name.Contains("Dodgers") || game.Teams.Home.Team.Name.Contains("Dodgers"));
					if (dodgersGames.Any())
					{
						return dodgersGames;
					}
					else
					{
						Console.WriteLine("No dodgers games.");
						return Array.Empty<Game>();
					}
				}
				else
				{
					Console.WriteLine(request);
					return Array.Empty<Game>();
				}
			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.ToString());
				return Array.Empty<Game>();
			}
		}
	}
}
