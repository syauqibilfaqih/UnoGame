using System;
using UnoGame;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Runtime.CompilerServices;

class Program
{
	
	static void Main()
	{
		// SampleGame1 newGame = new();
		SampleGame2 newGame = new();
	}

	
}

		// //============ Serialization =========
		
		// string fileName = "PlayersData.json"; 
		// string jsonString = JsonSerializer.Serialize(players);
		// File.WriteAllText(fileName, jsonString);
		
		// Console.WriteLine("\n\nSearlizing JSON......\n\n");

		// Console.WriteLine(File.ReadAllText(fileName));
		
		// //============ Multiple Data Deserialization =========
		// Console.WriteLine("\n\nDesearlizing JSON......\n\n");
		// string jsonFromFile = File.ReadAllText(fileName);
		// List<Player>? deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(jsonFromFile);
		
		// foreach (var person in deserializedPlayers ?? new List<Player>())
		// {
		// 	Console.WriteLine($"Deserialized Players: {person.Id}, {person.Name}");
		// }