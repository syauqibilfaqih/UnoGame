using System;
using UnoGame;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Runtime.CompilerServices;

class Program
{
	
	static void Main()
	{
		//============ Receiving Players Data =========
		int numberOfPlayer;
		List<IPlayer> players = new List<IPlayer>();
		Console.Clear();
		Console.WriteLine("Welcome to Uno Game!!!\n");

		while(true)
		{
			string? snumofPlayer;
			Console.Write("Enter the maximum number of player: ");
			snumofPlayer = Console.ReadLine();
			if (!int.TryParse(snumofPlayer, out int numOfPlayer))
			{
				Console.WriteLine("Invalid input for player Id. Please enter a valid integer.");
				continue;
			}
			else{
				numberOfPlayer = numOfPlayer;
				break;
			}
		}

		UnoGameController unoGameMaster = new(numberOfPlayer);
		

		Console.Clear();

		Console.WriteLine("Enter player information.\n");
		
		

		for(int id = 0; id<unoGameMaster.GetMaxPlayers(); id++)
		{
			Console.Write($"Enter Player {id+1} name: ");
			string? playerName = Console.ReadLine();
			unoGameMaster.AddPlayer(new Player(id,playerName),id);
		}

		Console.Clear();

		Console.WriteLine("Players Information:\n");
		foreach (var player in unoGameMaster.GetPlayerList())
		{
			Console.WriteLine($"Player Id: {player.Id}, Player Name: {player.Name}");
		}

		Console.ReadKey();
		Console.Clear();

		//================ Generating Deck =============================

		//================ Loop Game ===================================

			//================== Check if there's no winner yet ========

			//================== Giving turn based on the state ========

			//================== Show list of card per Player ==========

			//================== Ask user to give their current card ===
			//================== or at least take more card ============

			//================== Check if the card is playable =========

			//================== Add or Remove card from user ==========
			
		//================ Game Ends ===================================

		Console.WriteLine("Program ended. Press any key to exit.");
		Console.ReadKey();
		Console.Clear();
		
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