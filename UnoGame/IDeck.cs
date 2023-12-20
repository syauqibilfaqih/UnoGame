namespace UnoGame;

public interface IDeck
{
	/// <summary>
	/// Gets or sets the set of cards on the deck.
	/// </summary>
	HashSet<Card> CardsOnDeck { get; set; }

}
