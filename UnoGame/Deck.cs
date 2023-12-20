using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
using System.Globalization;
using Microsoft.Win32.SafeHandles;
using System.Drawing;
namespace UnoGame;

public class Deck : IDeck 
{
	public HashSet<Card> CardsOnDeck { get; set; }

    /// <summary>
    /// A constructor for class deck to generate collection of cards when it's called as an instance
    /// </summary>
    public Deck()
    {
        CardsOnDeck = new HashSet<Card>();
        WildCard wildCard = new WildCard();
        WildDrawFourCard wildDrawFourCard = new WildDrawFourCard();
        CardsOnDeck.Add(wildCard);
        CardsOnDeck.Add(wildDrawFourCard);
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            if(color != Color.Black)
                CardsOnDeck.Add(new DrawTwoCard(color));
        }
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            if(color != Color.Black)
                CardsOnDeck.Add(new SkipCard(color));
        }
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            if(color != Color.Black)
                CardsOnDeck.Add(new ReverseCard(color));
        }
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            foreach (CardType cardtype in Enum.GetValues(typeof(CardType)))
            {
                if(color != Color.Black && cardtype != CardType.Wild && cardtype != CardType.WildDrawFour && cardtype != CardType.DrawTwo && cardtype != CardType.Skip && cardtype != CardType.Reverse)
                    CardsOnDeck.Add(new ValueCard(color,cardtype));
            }
        }
    }
}
