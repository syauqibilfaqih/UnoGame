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
		
		//================ Generating Deck and First Card on Table =========================
		foreach (var player in unoGameMaster.GetPlayerList())
		{
			unoGameMaster.AddCardToPlayer(player, 5);
		}

		unoGameMaster.AddCardToTable();

		Console.WriteLine($"Card on table: \n");
		ChangeColorConsole(unoGameMaster.GetPlayedCard().Color);
		Console.WriteLine($"[{unoGameMaster.GetPlayedCard().Color} {unoGameMaster.GetPlayedCard().CardType}]\n");
		ResetColorConsole();

		Console.ReadKey();
		Console.Clear();

		unoGameMaster.SwitchGameDirection(Direction.CounterClockwise);

		int indexPlayer = 0;
		//================ Loop Game ===================================
		while(true) 
		{
			//================== Giving turn based on the state ========
			unoGameMaster.SwitchPlayer(indexPlayer);
			var _playerNow = unoGameMaster.GetPlayerNow();
			Console.WriteLine($"Card on table: \n");
			ChangeColorConsole(unoGameMaster.GetPlayedCard().Color);
			Console.WriteLine($"[{unoGameMaster.GetPlayedCard().Color} {unoGameMaster.GetPlayedCard().CardType}]\n");
			//================== Show list of card per Player ==========
			var cards = unoGameMaster.CheckPlayerCard(_playerNow);
			int count = 1;
			foreach (var c in cards)
			{
				ChangeColorConsole(c.Color);
				Console.Write($"[{count}. {c.Color} {c.CardType}]  ");
				count=count+1;
			}
			ResetColorConsole();
			Console.WriteLine($"\n\n                          {_playerNow.Name}\n");
			//================== Ask user to give their current card ===
			//================== or at least take more card ============
			//int numberOfCardIndex;
			while(true)
			{
				string snumofCardIndex;
				Console.Write("Input the index of your card or just click enter to pass it: ");
				snumofCardIndex = Console.ReadLine();
				if(snumofCardIndex == ""){
					unoGameMaster.AddCardToPlayer(_playerNow);
					break;
				}
				//================== Check if the card is playable =========
				else if (!int.TryParse(snumofCardIndex, out int numOfCardIndex) || numOfCardIndex == 0)
				{
					Console.WriteLine("Invalid input. Please enter a valid integer.");
					continue;
				}
				//================== Add or Remove card from user ==========
				else if(!unoGameMaster.IsAnyCardPossible(_playerNow, numOfCardIndex-1, unoGameMaster.GetPlayedCard()))
				{
					Console.WriteLine("Invalid input. Please enter a valid index.");
					continue;
				}
				else{
					unoGameMaster.AddCardToTable(unoGameMaster.CheckPlayerCard(_playerNow).ElementAt(numOfCardIndex-1));
					unoGameMaster.RemoveCardFromPlayer(_playerNow, unoGameMaster.CheckPlayerCard(_playerNow).ElementAt(numOfCardIndex-1));
					break;
				}
			}
			// Console.WriteLine("")
			// Console.ReadKey();

			Console.Clear();

			//================== Check if there's a winner ========
			if(unoGameMaster.IsGameOver(_playerNow))
			{
				Console.WriteLine($"Congratulation {_playerNow.Name}!");
				Console.ReadKey();
				Console.Clear();
				break;
			}
				
			if(indexPlayer+1<numberOfPlayer)
				indexPlayer=indexPlayer+1;
			else
				indexPlayer=0;
		//================ Game Ends ===================================
		}

		Console.WriteLine("Program ended. Press any key to exit.");
		Console.ReadKey();
		Console.Clear();
		
	}

	static void ChangeColorConsole(Color color)
	{
		if(color == Color.Red)
			Console.ForegroundColor = ConsoleColor.Red;
		if(color == Color.Yellow)
			Console.ForegroundColor = ConsoleColor.Yellow;
		if(color == Color.Green)
			Console.ForegroundColor = ConsoleColor.Green;
		if(color == Color.Blue)
			Console.ForegroundColor = ConsoleColor.Blue;
		if(color == Color.Black)
			Console.ResetColor();
	}
	static void ResetColorConsole()
	{
		Console.ResetColor();
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