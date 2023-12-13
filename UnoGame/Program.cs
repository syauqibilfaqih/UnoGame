using UnoGame;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Runtime.CompilerServices;

class Program
{
	static void Main()
	{
		//============ Receiving Players Data =========
		
		List<IPlayer> players = new List<IPlayer>();

		Console.WriteLine("Enter player information. To stop, type 'exit'.");

		while (true)
		{
			Console.Write("Enter player Id: ");
			string? idInput = Console.ReadLine();

			if (idInput?.ToLower() == "exit")
				break;

			if (!int.TryParse(idInput, out int playerId))
			{
				Console.WriteLine("Invalid input for player Id. Please enter a valid integer.");
				continue;
			}

			Console.Write("Enter player name: ");
			string? playerName = Console.ReadLine();

			players.Add(new Player(playerId, playerName));

			Console.WriteLine("Player added successfully!");
		}

		Console.WriteLine("\nPlayers Information:");
		foreach (var player in players)
		{
			Console.WriteLine($"Player Id: {player.Id}, Player Name: {player.Name}");
		}
		
		//============ Serialization =========
		
		string fileName = "PlayersData.json"; 
		string jsonString = JsonSerializer.Serialize(players);
		File.WriteAllText(fileName, jsonString);
		
		Console.WriteLine("\n\nSearlizing JSON......\n\n");

		Console.WriteLine(File.ReadAllText(fileName));
		
		//============== Single Data Deserialization ==========
		// string fileName = "PlayersData.json"; 
		// string jsonFromFile = File.ReadAllText(fileName);
		// IPlayer? players = JsonSerializer.Deserialize<Player>(jsonFromFile);
		// Console.WriteLine($"Name: {players?.Name}");
		
		//============ Multiple Data Deserialization =========
		Console.WriteLine("\n\nDesearlizing JSON......\n\n");
		string jsonFromFile = File.ReadAllText(fileName);
		List<Player>? deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(jsonFromFile);
		
		foreach (var person in deserializedPlayers ?? new List<Player>())
		{
			Console.WriteLine($"Deserialized Players: {person.Id}, {person.Name}");
		}
		
		Console.WriteLine("Program ended. Press any key to exit.");
		Console.ReadKey();
		
	}
}