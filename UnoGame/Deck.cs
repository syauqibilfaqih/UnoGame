namespace UnoGame;

public class Deck : IDeck 
{
	public HashSet<Card> CardsOnDeck { get; set; }

    public Deck()
    {
        CardsOnDeck = new HashSet<Card>();
    }

    public Deck(params Card[] cards)
    {
        CardsOnDeck = new HashSet<Card>(cards);
    }
}
