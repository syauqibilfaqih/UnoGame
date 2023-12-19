using System.Collections.Generic;
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
	private Dictionary<IPlayer, List<Card>>? _cardsOnPlayers;
	private int? _totalMustDrawCard;
	private int _maxPlayers;
	private Stack<Card>? _cardOnTable;

	// Score Fields
	private Dictionary<IPlayer, int>? _playerScoreList;

	public UnoGameController()
	{
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>();  
		_winner = new Player();
		_maxPlayers = 2; // default maximum number of player
	}

	public UnoGameController(int maxPlayers)
	{
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>(); 
		_winner = new Player();
		_maxPlayers = maxPlayers; 
	}

	public UnoGameController(int maxPlayers, IEnumerable<Card> customCard)
	{
		_cardsOnDeck = new Deck();
		_cardsOnPlayers = new Dictionary<IPlayer, List<Card>>();
		_cardOnTable = new Stack<Card>(); 
		_maxPlayers = maxPlayers; 
		_winner = new Player();
		_cardsOnDeck.CardsOnDeck = (HashSet<Card>)customCard;
	}

	public void AddPlayer(IPlayer player, int numberOfIndex)
	{
		// Implementation
		if(_players.Count <= _maxPlayers)
			_players.Insert(numberOfIndex, player);
		else 
			throw new ArgumentOutOfRangeException(
                 nameof(_players),
                "The maximum number of players is already fullfiled"
             );
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

	public IEnumerable<Card> GetAllPlayerCard()
	{
		List<Card> allPlayerCards = new List<Card>();
		for (int i = 0; i < _maxPlayers; i++)
		{
			allPlayerCards.AddRange(_cardsOnPlayers.Values.ElementAt(i));	
		}
		return allPlayerCards;
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
	public bool IsGameOver(IPlayer player)
	{
		// Implementation
		List<Card> cards = _cardsOnPlayers[player];
		if(!cards.Any())
		{
			_winner = player;
			return true;
		}
		else
		{
			return false;
		}
	}

	public IPlayer? GetPlayerNow()
	{
		// Implementation
		return _playerNow;
	}

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

	public void SwitchGameDirection()
	{
		if(_direction == Direction.Clockwise)
		{
			_direction = Direction.CounterClockwise;
		}
		else
		{
			_direction = Direction.Clockwise;
		}
	}

	// Deck Methods
	public IDeck? GetDeck()
	{
		// Implementation
		return _cardsOnDeck;
	}

	public void RemoveCardFromDeck(Card card)
	{
		// Implementation
		_cardsOnDeck.CardsOnDeck.Remove(card);
	}

	// Player Card Methods
	public IEnumerable<Card> CheckPlayerCard(IPlayer player)
	{
		// Implementation
		if(_players.Contains(player))
			return _cardsOnPlayers[player];
		else
			return null;
	}

	public void RemoveCardFromPlayer(IPlayer player, Card card)
	{
		// Implementation
		// TO DO : give validation and make a boolean return
		var listCard =_cardsOnPlayers[player];
		listCard.Remove(card);
		_cardsOnPlayers[player] = listCard;
	}

	public void DrawCard(IPlayer player)
	{
		AddCardToPlayer(player);
	}

	// Checking possible card and play the card Methods
	public bool IsAnyCardPossible(IPlayer player, int indexOfCard, Card otherCard)
	{
		// Implementation
		List<Card> cards = _cardsOnPlayers[player];
		if(cards.ElementAt(indexOfCard).IsCardMatch(otherCard))
			return true;
		else
			return false;
	}

	public IEnumerable<Card> CheckPossibleCard(IPlayer player, Card otherCard)
	{
		// Implementation
		List<Card> possibleCards = new List<Card>();
		if(_players.Contains(player))
		{
			foreach (var card in CheckPlayerCard(player))
			{
				if(card.IsCardMatch(otherCard))
				{
					possibleCards.Add(card);
				}
				else
				{
					continue;
				}
			}
			return possibleCards;
		}
		else
		{
			return null;
		}	
	}

	public void PlayerPickColor(Color color)
	{
		// Implementation
		_pickedColor = color;
	}

	public Color GetPickedColor()
	{
		// Implementation
		return _pickedColor;
	}

	public bool PauseGame()
	{
		// Implementation
		_gameStatus = GameStatus.GamePause;
		return true;
	}

	public bool EndGame()
	{
		// Implementation
		_gameStatus = GameStatus.GameOver;
		return true;
	}

	// Score and winner Methods
	public IPlayer? GetWinner()
	{
		// Implementation
		return _winner;
	}

	public int GetPlayerScore()
	{
		// Implementation
		Card card;
		int totalScore = 0;
		foreach (var item in GetAllPlayerCard())
		{
			card = item;
			totalScore = totalScore + (int) card.CardType;
		}
		return totalScore;
	}

	public void AddCardToDeck(Card card)
	{
		_cardsOnDeck.CardsOnDeck.Add(card);
	}

	public bool StartGame()
	{
		// Implementation
		_gameStatus = GameStatus.GameRunning;
		return true;
	}

	// public bool PlayerSayUno(IPlayer player)
	// {
	// 	// Implementation
	// 	return true;
	// }

	// public bool MoveCardTableToDeck()
	// {
	// 	// Implementation
	// 	return false;
	// }
	// public bool PlayCard(IPlayer player, params Card[] cards)
	// {
	// 	// Implementation
	// 	return false;
	// }
	// Starting the game Methods
}
