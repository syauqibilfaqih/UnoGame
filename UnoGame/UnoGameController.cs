using System.Collections.Generic;
using NLog;
namespace UnoGame;

public class UnoGameController
{

	/// <summary>
    /// Instance of UnoGameController logger.
    /// </summary>
    private readonly ILogger _logger;

	/// <summary>
	/// Event raised to update the game status.
	/// </summary>
	public Action<GameStatus>? UpdateGameStatus;

	/// <summary>
	/// Event raised to update the list of players in the game.
	/// </summary>
	public Action<IEnumerable<IPlayer>>? UpdatePlayerList;

	/// <summary>
	/// Event raised when a new player is added to the game.
	/// </summary>
	public Action<IPlayer>? UpdateAddPlayer;

	/// <summary>
	/// Event raised when a player draws one or more cards.
	/// </summary>
	public Action<IPlayer, IEnumerable<Card>>? UpdatePlayerDrawCard;

	/// <summary>
	/// Event raised to update the possible cards for a player.
	/// </summary>
	public Action<IPlayer, IEnumerable<Card>>? UpdatePossibleCard;

	/// <summary>
	/// Event raised when a player plays one or more cards.
	/// </summary>
	public Action<IPlayer, Card>? UpdatePlayedCard;

	/// <summary>
	/// Event raised to update the color picked by a player.
	/// </summary>
	public Action<IPlayer, Color>? UpdatePickedColor;

	/// <summary>
	/// Event raised when a player says "Uno."
	/// </summary>
	public Action<IPlayer>? UpdateSayUno;
	
	/// <summary>
	/// Represents the current status of the Uno game.
	/// </summary>
	private GameStatus _gameStatus;

	/// <summary>
	/// Represents the current direction of the Uno game (Clockwise or CounterClockwise).
	/// </summary>
	private Direction _direction;

	/// <summary>
	/// Represents the color that has been picked in the Uno game.
	/// </summary>
	private Color _pickedColor;

	/// <summary>
	/// Represents the current player in focus during the Uno game.
	/// </summary>
	private IPlayer? _playerNow;

	/// <summary>
	/// Represents the list of players participating in the Uno game.
	/// </summary>
	private List<IPlayer> _players = new List<IPlayer>();

	/// <summary>
	/// Represents the player who has won the Uno game.
	/// </summary>
	private IPlayer? _winner;

	/// <summary>
	/// Represents a flag indicating whether a player has said "Uno" during their turn.
	/// </summary>
	private bool _sayUno;

	/// <summary>
	/// Represents the index of the current player in the list of players.
	/// </summary>
	private int _indexPlayer;

	/// <summary>
	/// Represents the deck of cards in the Uno game.
	/// </summary>
	private IDeck? _cardsOnDeck;

	/// <summary>
	/// Represents the cards held by each player in the Uno game.
	/// </summary>
	private Dictionary<IPlayer, List<Card>>? _cardsOnPlayers;

	/// <summary>
	/// Represents the total number of cards that must be drawn by a player in a turn.
	/// </summary>
	private int? _totalMustDrawCard;

	/// <summary>
	/// Represents the maximum number of players allowed in the Uno game.
	/// </summary>
	private int _maxPlayers;

	/// <summary>
	/// Represents the stack of cards placed on the table during the Uno game.
	/// </summary>
	private Stack<Card>? _cardOnTable;

	/// <summary>
	/// Represents the score associated with the player in the Uno game.
	/// </summary>
	private Dictionary<IPlayer, int>? _playerScoreList;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="UnoGameController"/> class with default settings.
	/// </summary>
	public UnoGameController()
	{
		_logger?.Info("Creating game...");
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>();
		_winner = new Player();
		_maxPlayers = 2; // Default maximum number of players.
		_logger?.Info("Game created!");
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="UnoGameController"/> class with a specified maximum number of players.
	/// </summary>
	/// <param name="maxPlayers">The maximum number of players.</param>
	public UnoGameController(int maxPlayers)
	{
		_logger?.Info("Creating game...");
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>();
		_winner = new Player();
		_maxPlayers = maxPlayers;
		_logger?.Info("Game created!");
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="UnoGameController"/> class with a specified maximum number of players
	/// and a custom set of cards.
	/// </summary>
	/// <param name="maxPlayers">The maximum number of players.</param>
	/// <param name="customCard">Custom set of cards to use in the game.</param>
	public UnoGameController(int maxPlayers, IEnumerable<Card> customCard)
	{
		_logger?.Info("Creating game...");
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>();
		_maxPlayers = maxPlayers;
		_winner = new Player();
		_cardsOnDeck.CardsOnDeck = new HashSet<Card>(customCard);
		_logger?.Info("Game created!");
	}

	/// <summary>
	/// Adds a player to the game at the specified index.
	/// </summary>
	/// <param name="player">The player to add.</param>
	/// <param name="numberOfIndex">The index at which the player should be added.</param>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the maximum number of players is already reached.</exception>
	public void AddPlayer(IPlayer player, int numberOfIndex)
	{
		_logger?.Info("Adding player...");
		if (_players.Count <= _maxPlayers)
		{
			_players.Insert(numberOfIndex, player);
			UpdatePlayerList?.Invoke(_players);
			UpdateAddPlayer?.Invoke(player);
			_logger?.Info("Player added");
		}
		else
		{
			_logger?.Error("The maximum number of players is already fulfilled.");
			// throw new ArgumentOutOfRangeException(
			// 	nameof(_players),
			// 	"The maximum number of players is already fulfilled."
			// );
		}
	}

	/// <summary>
	/// To set index of player's turn
	/// /// </summary>
	/// <param name="index">the value of index</param>
	public void SetIndexPlayer(int index)
	{
		_indexPlayer = index;
	}

	/// <summary>
	/// To get the current index
	/// </summary>
	/// <returns></returns>
	public int GetIndexPlayer()
	{
		return _indexPlayer;
	}

	/// <summary>
	/// To shift the player turn
	/// </summary>
	public void ShiftIndexPlayer()
	{
		_indexPlayer = _indexPlayer + (int)GetGameDirection();

		if(_indexPlayer > _maxPlayers-1)
			_indexPlayer = _indexPlayer-(_maxPlayers);
		if(_indexPlayer < 0)
			_indexPlayer = (_maxPlayers)+_indexPlayer;
	}


	/// <summary>
	/// Gets the maximum number of players allowed in the game.
	/// </summary>
	/// <returns>The maximum number of players.</returns>
	public int GetMaxPlayers()
	{
		return _maxPlayers;
	}

	/// <summary>
	/// Gets the list of players currently in the game.
	/// </summary>
	/// <returns>An IEnumerable of <see cref="IPlayer"/>.</returns>
	public IEnumerable<IPlayer> GetPlayerList()
	{
		return _players;
	}

	/// <summary>
	/// Gets the current player in the game.
	/// </summary>
	/// <returns>The current player, or null if no player is currently set.</returns>
	public IPlayer? GetPlayerNow()
	{
		return _playerNow;
	}

	/// <summary>
	/// Switches the current player to the one at the specified index.
	/// </summary>
	/// <param name="indexOfPlayer">The index of the player to switch to.</param>
	public void SwitchPlayer(int indexOfPlayer)
	{
		_logger?.Info("Switching player turn...");
		_playerNow = _players.ElementAt(indexOfPlayer);
		_logger?.Info("Switch player succeed.");
	}

	/// <summary>
	/// Adds a specified number of random cards from the deck to a player.
	/// </summary>
	/// <param name="player">The player to whom the cards should be added.</param>
	/// <param name="numberOfCard">The number of cards to add to the player.</param>
	public void AddCardToPlayer(IPlayer player, int numberOfCard)
	{
		_logger?.Info("Adding cards to player...");
		Random random = new();
		List<Card> cards = new();
		for (int i = 0; i < numberOfCard; i++)
		{
			cards.Add(_cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count)));
		}
		_cardsOnPlayers.Add(player, cards);
		_logger?.Info("Cards added.");
	}

	/// <summary>
	/// Adds a specific card to a player.
	/// </summary>
	/// <param name="player">The player to whom the card should be added.</param>
	/// <param name="card">The card to add to the player.</param>
	public void AddCardToPlayer(IPlayer player, Card card)
	{
		_logger?.Info("Adding card to player...");
		List<Card> cards = _cardsOnPlayers[player];
		cards.Add(card);
		_cardsOnPlayers[player] = cards;
		UpdatePlayerDrawCard?.Invoke(player, cards);
		_logger?.Info("The card added to player.");
	}

	/// <summary>
	/// Adds a random card from the deck to a player.
	/// </summary>
	/// <param name="player">The player to whom the card should be added.</param>
	public void AddCardToPlayer(IPlayer player)
	{
		_logger?.Info("Adding card to player...");
		Random random = new();
		List<Card> cards = _cardsOnPlayers[player];
		cards.Add(_cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count)));
		_cardsOnPlayers[player] = cards;
		_logger?.Info("A card added to player.");
	}

	/// <summary>
	/// Retrieves a dictionary containing the cards held by each player.
	/// </summary>
	/// <returns>A dictionary where the key is a player and the value is a list of cards held by that player.</returns>
	public Dictionary<IPlayer, List<Card>> CheckAllPlayerCard()
	{
		return _cardsOnPlayers;
	}

	/// <summary>
	/// Retrieves all cards held by all players.
	/// </summary>
	/// <returns>An IEnumerable of all cards held by all players.</returns>
	public IEnumerable<Card> GetAllPlayerCard()
	{
		List<Card> allPlayerCards = new List<Card>();
		for (int i = 0; i < _maxPlayers; i++)
		{
			allPlayerCards.AddRange(_cardsOnPlayers.Values.ElementAt(i));
		}
		return allPlayerCards;
	}

	/// <summary>
	/// Adds a specific card to the table.
	/// </summary>
	/// <param name="card">The card to add to the table.</param>
	public void AddCardToTable(Card card)
	{
		_logger?.Info("Adding the card to the table...");
		_cardOnTable.Push(card);
		UpdatePlayedCard?.Invoke(_playerNow, _cardOnTable.Peek());
		_logger?.Info("The card added successfully.");
	}

	/// <summary>
	/// Adds a random value card from the deck to the table.
	/// </summary>
	public void AddCardToTable()
	{
		_logger?.Info("Adding a card to table...");
		Random random = new();
		while (true)
		{
			var getRandomCard = _cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count));
			if (getRandomCard is ValueCard)
			{
				_cardOnTable.Push(getRandomCard);
				break;
			}
		}
		_logger?.Info("A card added successfully.");
	}

	/// <summary>
	/// Gets the card currently played on the table.
	/// </summary>
	/// <returns>The card currently played on the table, or null if no card has been played.</returns>
	public Card? GetPlayedCard()
	{
		return _cardOnTable.Peek();
	}

	/// <summary>
	/// Gets the current game status.
	/// </summary>
	/// <returns>The current game status.</returns>
	public GameStatus GetGameStatus()
	{
		return _gameStatus;
	}

	/// <summary>
	/// Gets the current game direction.
	/// </summary>
	/// <returns>The current game direction.</returns>
	public Direction GetGameDirection()
	{
		return _direction;
	}

	/// <summary>
	/// Switches the game direction to the specified direction.
	/// </summary>
	/// <param name="direction">The direction to switch to.</param>
	public void SwitchGameDirection(Direction direction)
	{
		_logger?.Info("Reverse direction...");
		_direction = direction;
		_logger?.Info("Game direction reversed.");
	}

	/// <summary>
	/// Switches the game direction between clockwise and counterclockwise.
	/// </summary>
	public void SwitchGameDirection()
	{
		_logger?.Info("Reverse direction...");
		if (_direction == Direction.Clockwise)
		{
			_direction = Direction.CounterClockwise;
		}
		else
		{
			_direction = Direction.Clockwise;
		}
		_logger?.Info("Game direction reversed.");
	}

	/// <summary>
	/// Gets the current deck of cards.
	/// </summary>
	/// <returns>The current deck of cards.</returns>
	public IDeck? GetDeck()
	{
		return _cardsOnDeck;
	}

	/// <summary>
	/// Removes the specified card from the deck.
	/// </summary>
	/// <param name="card">The card to be removed.</param>
	public void RemoveCardFromDeck(Card card)
	{
		_logger?.Info("Removing card from deck...");
		_cardsOnDeck.CardsOnDeck.Remove(card);
		_logger?.Info("Card removed.");
	}

	/// <summary>
	/// Adds the specified card to the deck.
	/// </summary>
	/// <param name="card">The card to be added.</param>
	public void AddCardToDeck(Card card)
	{
		_logger?.Info("Adding the card to the deck...");
		_cardsOnDeck.CardsOnDeck.Add(card);
		_logger?.Info("The card was added successfully.");
	}

	/// <summary>
	/// Checks and returns the cards held by the specified player.
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <returns>The cards held by the player, or null if the player is not found.</returns>
	public IEnumerable<Card>? CheckPlayerCard(IPlayer player)
	{
		_logger?.Info("Checking player cards...");
		if (_players.Contains(player))
		{
			_logger?.Info("Successfully check the player's cards.");
			return _cardsOnPlayers[player];
		}
		else
		{
			_logger?.Warn("There's no player you meant.");
			return null;
		}
	}

	/// <summary>
	/// Removes the specified card from the player's hand.
	/// </summary>
	/// <param name="player">The player whose card needs to be removed.</param>
	/// <param name="card">The card to be removed.</param>
	public void RemoveCardFromPlayer(IPlayer player, Card card)
	{
		_logger?.Info("Removing card from player...");
		var listCard = _cardsOnPlayers[player];
		listCard.Remove(card);
		_cardsOnPlayers[player] = listCard;
		_logger?.Info("Card: {card}, was removed successfully.", card);
	}

	/// <summary>
	/// Draws a card for the specified player.
	/// </summary>
	/// <param name="player">The player to draw a card for.</param>
	public void DrawCard(IPlayer player)
	{
		AddCardToPlayer(player);
	}

	/// <summary>
	/// Checks if any card in the player's hand is possible to play.
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <param name="indexOfCard">The index of the card to check.</param>
	/// <param name="otherCard">The card to match against.</param>
	/// <returns>True if the card is possible to play, false otherwise.</returns>
	public bool IsAnyCardPossible(IPlayer player, int indexOfCard, Card otherCard)
	{
		_logger?.Info("Check if there's any possible card...");
		List<Card> cards = _cardsOnPlayers[player];
		return cards.ElementAt(indexOfCard).IsCardMatch(otherCard) || cards.ElementAt(indexOfCard).Color == _pickedColor;
	}

	/// <summary>
	/// Checks and returns the possible cards that the player can play against the specified card.
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <param name="otherCard">The card to match against.</param>
	/// <returns>The list of possible cards, or null if the player is not found.</returns>
	public IEnumerable<Card>? CheckPossibleCard(IPlayer player, Card otherCard)
	{
		_logger?.Info("Check possible cards...");
		List<Card> possibleCards = new List<Card>();
		if (_players.Contains(player))
		{
			foreach (var card in CheckPlayerCard(player))
			{
				if (card.IsCardMatch(otherCard))
				{
					possibleCards.Add(card);
				}
			}
			UpdatePossibleCard?.Invoke(player, possibleCards);
			_logger?.Info("Possible cards are including: {logpossibleCards}.", possibleCards);
			return possibleCards;
		}
		else
		{
			_logger?.Warn("There's no player you meant.");
			return null;
		}
	}

	/// <summary>
	/// Allows the player to pick a color.
	/// </summary>
	/// <param name="color">The color picked by the player.</param>
	public void PlayerPickColor(Color color)
	{
		_logger?.Info("Player picking color...");
		_pickedColor = color;
		UpdatePickedColor?.Invoke(_playerNow, _pickedColor);
		_logger?.Info("Color: {color} successfully picked", _pickedColor);
	}

	/// <summary>
	/// Gets the currently picked color.
	/// </summary>
	/// <returns>The currently picked color.</returns>
	public Color GetPickedColor()
	{
		return _pickedColor;
	}
	
	/// <summary>
	/// Starts the Uno game.
	/// </summary>
	/// <returns>True if the game started successfully, false otherwise.</returns>
	public bool StartGame()
	{
		_logger?.Info("Starting the game...");
		_gameStatus = GameStatus.GameRunning;
		UpdateGameStatus?.Invoke(_gameStatus);
		_logger?.Info("Game started!");
		return true;
	}

	/// <summary>
	/// Pauses the Uno game.
	/// </summary>
	/// <returns>True if the game paused successfully, false otherwise.</returns>
	public bool PauseGame()
	{
		_logger?.Info("Pausing the game...");
		_gameStatus = GameStatus.GamePause;
		UpdateGameStatus?.Invoke(_gameStatus);
		_logger?.Info("Game paused!");
		return true;
	}

	/// <summary>
	/// Ends the Uno game.
	/// </summary>
	/// <returns>True if the game ended successfully, false otherwise.</returns>
	public bool EndGame()
	{
		_logger?.Info("Ending the game...");
		_gameStatus = GameStatus.GameOver;
		UpdateGameStatus?.Invoke(_gameStatus);
		_logger?.Info("Game ended");
		return true;
	}

	/// <summary>
	/// Gets the winner of the Uno game.
	/// </summary>
	/// <returns>The player who won the game, or null if there is no winner yet.</returns>
	public IPlayer? GetWinner()
	{
		return _winner;
	}

	/// <summary>
	/// Gets the total score of a player based on the cards they have.
	/// </summary>
	/// <returns>The total score of the player.</returns>
	public int GetPlayerScore()
	{
		Card card;
		int totalScore = 0;
		foreach (var item in GetAllPlayerCard())
		{
			card = item;
			totalScore = totalScore + (int)card.CardType;
		}
		return totalScore;
	}

	/// <summary>
	/// Indicates whether a player has said "UNO."
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <param name="state">The state of the "UNO" announcement.</param>
	/// <returns>True if the player has said "UNO," false otherwise.</returns>
	public bool PlayerSayUno(IPlayer player, bool state)
	{
		_logger?.Info("Check if player said UNO...");
		if (state)
		{
			_sayUno = true;
			_logger?.Info("Player said UNO.");
		}
		else
		{
			_sayUno = false;
			_logger?.Info("Player didn't say UNO.");
		}
		UpdateSayUno?.Invoke(player);
		return true;
	}

	/// <summary>
	/// Checks if any player has said "UNO."
	/// </summary>
	/// <returns>True if any player has said "UNO," false otherwise.</returns>
	public bool IsPlayerSayUNO()
	{
		return _sayUno;
	}

	/// <summary>
	/// Checks if the game is over for a specific player.
	/// </summary>
	/// <param name="player">The player to check.</param>
	/// <returns>True if the game is over for the player, false otherwise.</returns>
	public bool IsGameOver(IPlayer player)
	{
		_logger?.Info("Checking if the game is over...");
		List<Card> cards = _cardsOnPlayers[player];
		if (!cards.Any())
		{
			_winner = player;
			_logger?.Info("Game over.");
			return true;
		}
		else
		{
			_logger?.Info("Game continue.");
			return false;
		}
	}	
}
