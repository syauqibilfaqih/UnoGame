namespace UnoGame;

public class UnoGameController
{
		// Delegate
	public Action<GameStatus>? UpdateGameStatus;
	public Action<IEnumerable<IPlayer>>? UpdatePlayerList;
	public Action<IPlayer>? UpdateAddPlayer;
	public Action<IPlayer, Card[]>? UpdatePlayerDrawCard;
	public Action<IPlayer>? UpdatePossibleCard;
	public Action<IPlayer, Card[]>? UpdatePlayedCard;
	public Action<IPlayer, Color>? UpdatePickedColor;
	public Action<IPlayer>? UpdateSayUno;

	// Enum Fields
	private GameStatus _gameStatus;
	private Direction _direction;
	private Color _pickedColor;

	// Player Fields
	private IPlayer? _playerNow;
	private List<IPlayer> _players = new List<IPlayer>();
	private IPlayer? _winner;

	// Card Fields
	private IDeck? _cardsOnDeck;
	private Dictionary<IPlayer, HashSet<Card>>? _cardsOnPlayers;
	private int? _totalMustDrawCard;
	private int _maxPlayers;
	private Stack<Card>? _cardOnTable;

	// Score Fields
	private Dictionary<IPlayer, int>? _playerScoreList;

	// Constructors
	// public UnoGameController(params IPlayer?[] players)
	// {
	// 	_players = players;
	// }

	// public UnoGameController(List<IPlayer> players)
	// {
	// 	_players = players;
	// }

	public UnoGameController(int maxPlayers)
	{
		_maxPlayers = maxPlayers; 
	}
	
	public UnoGameController()
	{
	}

	// Player Methods
	// public bool AddPlayer(IPlayer player)
	// {
	// 	// Implementation
	// 	return false;
	// }

	public void AddPlayer(IPlayer player, int numberOfIndex)
	{
		// Implementation
		_players.Insert(numberOfIndex, player);
	}
	
	public int GetMaxPlayers()
	{
		return _maxPlayers;
	}

	public IEnumerable<IPlayer> GetPlayerList()
	{
		// Implementation
		return _players;//Enumerable.Empty<IPlayer>();
	}

	public IPlayer? SwitchPlayer()
	{
		// Implementation
		return null;
	}

	public bool SwitchPlayer(IPlayer player)
	{
		// Implementation
		return false;
	}

	// Game Direction & Status Methods
	public GameStatus GetGameStatus()
	{
		// Implementation
		return _gameStatus;
	}

	public Direction GetGameDirection()
	{
		// Implementation
		return _direction;
	}

	public bool SwitchGameDirection(Direction direction)
	{
		// Implementation
		return false;
	}

	// Deck Methods
	public IDeck? GetDeck()
	{
		// Implementation
		return _cardsOnDeck;
	}

	public void ShuffleDeck()
	{
		// Implementation
		return false;
	}

	public bool AddCardToDeck(params Card[] cards)
	{
		// Implementation
		return false;
	}

	public bool MoveCardTableToDeck()
	{
		// Implementation
		return false;
	}

	public bool RemoveCardFromDeck(params Card[] cards)
	{
		// Implementation
		return false;
	}

	public bool RemoveCardFromDeck(IPlayer player, params Card[] cards)
	{
		// Implementation
		return false;
	}

	// Player Card Methods
	public IEnumerable<Card> CheckPlayerCard(IPlayer player)
	{
		// Implementation
		return Enumerable.Empty<Card>();
	}

	public Dictionary<IPlayer, HashSet<Card>> CheckAllPlayerCard()
	{
		// Implementation
		return new Dictionary<IPlayer, HashSet<Card>>();
	}

	public bool AddCardToPlayer(IPlayer player, params Card[] cards)
	{
		// Implementation
		return false;
	}

	public bool RemoveCardFromPlayer(IPlayer player, params Card[] cards)
	{
		// Implementation
		return false;
	}

	public IEnumerable<Card> DrawCard(int count)
	{
		// Implementation
		return Enumerable.Empty<Card>();
	}

	// Starting the game Methods
	public bool StartGame()
	{
		// Implementation
		return false;
	}

	public Card? GetFirstCard()
	{
		// Implementation
		Random rand = new Random();
		
        string val = value[rand.Next(0, value.Length)];
        string colour = colours[rand.Next(0, colours.Length)];
        string[] card = { val, colour };
        return card;
	}

	// Checking possible card and play the card Methods
	public bool IsAnyCardPossible(IPlayer player)
	{
		// Implementation
		return false;
	}

	public IEnumerable<Card> CheckPossibleCard(IPlayer player)
	{
		// Implementation
		return Enumerable.Empty<Card>();
	}

	public bool PlayCard(IPlayer player, params Card[] cards)
	{
		// Implementation
		return false;
	}

	public Card? GetPlayedCard()
	{
		// Implementation
		return null;
	}

	public bool PlayerPickColor(IPlayer player, Color color)
	{
		// Implementation
		return false;
	}

	public Color GetPickedColor()
	{
		// Implementation
		return _pickedColor;
	}

	// Ending the game Methods
	public bool IsGameOver(List<IPlayer> players)
	{
		// Implementation
		for (int i = 0; i < players.Count; i++)
        {
            // if (players[i].Deck().Count == 0)
                return true;
			// else
			// 	return false;
		}
		return true;
	}

	public bool PlayerSayUno(IPlayer player)
	{
		// Implementation
		return false;
	}

	public bool PauseGame()
	{
		// Implementation
		return false;
	}

	public bool EndGame()
	{
		// Implementation
		return false;
	}

	// Score and winner Methods
	public IPlayer? GetWinner()
	{
		// Implementation
		return _winner;
	}

	public int GetPlayerScore(IPlayer player)
	{
		// Implementation
		return 0;
	}

	public Dictionary<IPlayer, int> GetAllScore()
	{
		// Implementation
		return new Dictionary<IPlayer, int>();
	}
}
