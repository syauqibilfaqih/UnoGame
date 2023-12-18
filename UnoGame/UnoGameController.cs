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
	// private Dictionary<IPlayer, HashSet<Card>>? _cardsOnPlayers;
	private Dictionary<IPlayer, List<Card>>? _cardsOnPlayers;
	private int? _totalMustDrawCard;
	private int _maxPlayers;
	private Stack<Card>? _cardOnTable;

	// Score Fields
	private Dictionary<IPlayer, int>? _playerScoreList;

	public UnoGameController(int maxPlayers)
	{
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>(); 
		_maxPlayers = maxPlayers; 
	}
	//TO DO : overload constructor with dictionary of custom cards
	// public UnoGameController()
	
	public UnoGameController()
	{
		// _cardsOnDeck = new Deck();
		// _cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		// _cardOnTable = new Stack<Card>();  
		// _maxPlayers = 2; // default maximum number of player
	}

	public void AddPlayer(IPlayer player, int numberOfIndex)
	{
		// Implementation
		// TO DO : validation add player based on maximum number of player
		_players.Insert(numberOfIndex, player);
	}
	
	public int GetMaxPlayers()
	{
		return _maxPlayers;
	}

	public IEnumerable<IPlayer> GetPlayerList()
	{
		// Implementation
		return _players;
	}

	public void AddCardToPlayer(IPlayer player, int numberOfCard)
	{
		// Implementation
		Random random = new();
		List<Card> cards = new();
		for (int i = 0; i < numberOfCard; i++)
		{
			cards.Add(_cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count)));
		}
		_cardsOnPlayers.Add(player, cards);
	}

	public void AddCardToPlayer(IPlayer player, Card card)
	{
		// Implementation
		List<Card> cards = _cardsOnPlayers[player];
		cards.Add(card);
		_cardsOnPlayers[player] = cards;
	}

	public void AddCardToPlayer(IPlayer player)
	{
		Random random = new();
		List<Card> cards = _cardsOnPlayers[player];
		cards.Add(_cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count)));
		_cardsOnPlayers[player] = cards;
	}

	public Dictionary<IPlayer, List<Card>> CheckAllPlayerCard()
	{
		return _cardsOnPlayers;
	}

	public void AddCardToTable(Card card)
	{
		_cardOnTable.Push(card);
	}

	public void AddCardToTable()
	{
		Random random = new();
		while(true)
		{
			var getRandomCard = _cardsOnDeck.CardsOnDeck.ElementAt(random.Next(_cardsOnDeck.CardsOnDeck.Count));
			if(getRandomCard is ValueCard)
				{
					_cardOnTable.Push(getRandomCard);
					break;
				}
				
		}
	}

	public Card? GetPlayedCard()
	{
		// Implementation
		return _cardOnTable.Peek();
	}

	// Ending the game Methods
	// public bool IsGameOver(IEnumerable<IPlayer> players)
	// {
	// 	// Implementation
		
	// 	return false;
	// }

	public bool IsGameOver(IPlayer player)
	{
		// Implementation
		List<Card> cards = _cardsOnPlayers[player];
		if(!cards.Any())
			// _winner = player;
			return true;
		else
			return false;
	}

	// public IPlayer? SwitchPlayer()
	// {
	// 	// Implementation
	// 	return _playerNow;
	// }

	public IPlayer? GetPlayerNow()
	{
		// Implementation
		return _playerNow;
	}

	// public void SwitchPlayer(IPlayer player)
	// {
	// 	// Implementation
	// 	_playerNow = player;
	// }

	public void SwitchPlayer(int indexOfPlayer)
	{
		// Implementation
		_playerNow = _players.ElementAt(indexOfPlayer);
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

	public void SwitchGameDirection(Direction direction)
	{
		// Implementation
		_direction = direction;
	}

	// Deck Methods
	public IDeck? GetDeck()
	{
		// Implementation
		return _cardsOnDeck;
	}

	// public void ShuffleDeck()
	// {
	// 	// Implementation
	// 	return false;
	// }

	// public void AddCardToDeck(params Card[] cards)
	// {
	// 	// Implementation
	// 	_cardsOnDeck = cards;
		
	// }

	// public bool MoveCardTableToDeck()
	// {
	// 	// Implementation
	// 	return false;
	// }

	public void RemoveCardFromDeck(Card card)
	{
		// Implementation
		_cardsOnDeck.CardsOnDeck.Remove(card);
	}

	// public bool RemoveCardFromDeck(IPlayer player, params Card[] cards)
	// {
	// 	// Implementation
	// 	return false;
	// }

	// Player Card Methods
	public IEnumerable<Card> CheckPlayerCard(IPlayer player)
	{
		// Implementation
		// TO DO : give validation to know whether the player is already there or not
		return _cardsOnPlayers[player];
	}

	// public Dictionary<IPlayer, HashSet<Card>> CheckAllPlayerCard()
	// {
	// 	// Implementation
	// 	return new Dictionary<IPlayer, HashSet<Card>>();
	// }

	public void RemoveCardFromPlayer(IPlayer player, Card card)
	{
		// Implementation
		// TO DO : give validation and make a boolean return
		var listCard =_cardsOnPlayers[player];
		listCard.Remove(card);
		_cardsOnPlayers[player] = listCard;
	}

	// public IEnumerable<Card> DrawCard(int count)
	// {
	// 	// Implementation
	// 	return Enumerable.Empty<Card>();
	// }

	public void DrawCard(IPlayer player)
	{
		AddCardToPlayer(player);
	}
	// Starting the game Methods
	// public bool StartGame()
	// {
	// 	// Implementation
	// 	return false;
	// }

	// public Card? GetFirstCard()
	// {
	// 	// Implementation
	// 	// Random rand = new Random();
		
    //     // string val = value[rand.Next(0, value.Length)];
    //     // string colour = colours[rand.Next(0, colours.Length)];
    //     // string[] card = { val, colour };
    //     // return card;

	// }

	// Checking possible card and play the card Methods
	public bool IsAnyCardPossible(IPlayer player, int indexOfCard, Card otherCard)//, Dictionary<IPlayer, List<Card>>? cardsOnPlayers)
	{
		// Implementation
		List<Card> cards = _cardsOnPlayers[player];
		if(cards.ElementAt(indexOfCard).IsCardMatch(otherCard))
			return true;
		else
			return false;
	}

	public IEnumerable<Card> CheckPossibleCard(IPlayer player)
	{
		// Implementation
		return Enumerable.Empty<Card>();
	}

	// public bool PlayCard(IPlayer player, params Card[] cards)
	// {
	// 	// Implementation
	// 	return false;
	// }

	public void PlayerPickColor(IPlayer player, Color color)
	{
		// Implementation
		_pickedColor = color;
	}

	public Color GetPickedColor()
	{
		// Implementation
		return _pickedColor;
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
