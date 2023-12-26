namespace UnoGame;

public class SampleGame2
{
    public SampleGame2()
    {
        //============ Receiving Players Data =========
		int numberOfPlayer;
		Card currentCard;
		List<IPlayer> players = new List<IPlayer>();
		Console.Clear();

		Console.WriteLine("__________________________");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|         UNO             |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("|                         |");
		Console.WriteLine("__________________________");

		Console.WriteLine("\n\n\nWelcome to Uno Game!!!\n");

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
			unoGameMaster.AddCardToPlayer(player,5);
		}

		unoGameMaster.AddCardToTable();

		Console.WriteLine($"Card on table: \n");
		ChangeColorConsole(unoGameMaster.GetPlayedCard().Color);
		Console.WriteLine($"[{unoGameMaster.GetPlayedCard().Color} {unoGameMaster.GetPlayedCard().CardType}]\n");
		ResetColorConsole();

		Console.ReadKey();
		Console.Clear();

		unoGameMaster.SwitchGameDirection(Direction.CounterClockwise);

		// int indexPlayer = 0;

        unoGameMaster.SetIndexPlayer(0);
		
		//================ Loop Game ===================================
		while(true) 
		{
			//================== Giving turn based on the state ========
			unoGameMaster.SwitchPlayer(unoGameMaster.GetIndexPlayer());
			var _playerNow = unoGameMaster.GetPlayerNow();
			Console.WriteLine($"Press any key if {_playerNow.Name} is ready!");
			Console.ReadKey();
			Console.Clear();

			Console.Write($"Card on table: ");
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
			
			while(true)
			{
				string snumofCardIndex;
				Console.Write("Input the index of your card or just click enter to pass it: ");
				snumofCardIndex = Console.ReadLine();
				//================== Add Card to Player =========
				if(snumofCardIndex == ""){
					unoGameMaster.AddCardToPlayer(_playerNow);
					break;
				}
				//================== Check if the input was valid ==========
				else if (!int.TryParse(snumofCardIndex, out int numOfCardIndex) || numOfCardIndex == 0)
				{
					Console.WriteLine("Invalid input. Please enter a valid integer.");
					continue;
				}
				//================== Check if the card is playable =========
				else if(!unoGameMaster.IsAnyCardPossible(_playerNow, numOfCardIndex-1, unoGameMaster.GetPlayedCard()))
				{
					Console.WriteLine("Invalid input. Please enter a valid index.");
					continue;
				}
				//================== Remove card from user ==========
				else{
					currentCard = unoGameMaster.CheckPlayerCard(_playerNow).ElementAt(numOfCardIndex-1); 
					if(currentCard.IsSpecialAbility)
					{
						currentCard.SpecialAbility(unoGameMaster);
						if(currentCard.CardType == CardType.Wild || currentCard.CardType == CardType.WildDrawFour)
						{
							while(true)
							{
								Console.Write("Insert what color you want: ");
								string sinputColor = Console.ReadLine();
								try
								{
									Color inputColor = (Color)Enum.Parse(typeof(Color), sinputColor, true);
									unoGameMaster.PlayerPickColor(inputColor);
									break;
								}
								catch (System.Exception)
								{
									Console.WriteLine("Invalid input, try again!");
								}
							}
						}
						// if(currentCard.CardType == CardType.Skip || currentCard.CardType == CardType.DrawTwo || currentCard.CardType == CardType.WildDrawFour)
						// {
						// 	indexPlayer = indexPlayer + (int)unoGameMaster.GetGameDirection();
						// }
					}

					unoGameMaster.AddCardToTable(unoGameMaster.CheckPlayerCard(_playerNow).ElementAt(numOfCardIndex-1));
					unoGameMaster.RemoveCardFromPlayer(_playerNow, unoGameMaster.CheckPlayerCard(_playerNow).ElementAt(numOfCardIndex-1));
					break;
				}
			}

			Console.Clear();

			//================== Check if there's a winner ========
			if(unoGameMaster.IsGameOver(_playerNow))
			{
				Console.WriteLine($"Congratulation {_playerNow.Name}!");
				int scorePlayer = unoGameMaster.GetPlayerScore();
				Console.WriteLine($"Your score : {scorePlayer}!");
				Console.ReadKey();
				Console.Clear();
				break;
			}
				
			// indexPlayer = indexPlayer + (int)unoGameMaster.GetGameDirection();

			// if(indexPlayer > numberOfPlayer-1)
			// 	indexPlayer = indexPlayer-(numberOfPlayer);
			// if(indexPlayer < 0)
			// 	indexPlayer = (numberOfPlayer)+indexPlayer;
            unoGameMaster.ShiftIndexPlayer();

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
