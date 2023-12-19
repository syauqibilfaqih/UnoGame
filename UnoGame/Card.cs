namespace UnoGame;

public abstract class Card
{
    public Color Color { get; protected set; }
    public CardType CardType { get; protected set; }
    public bool IsSpecialAbility { get; set; }
    
    public abstract bool SpecialAbility(UnoGameController game);
    public abstract bool IsCardMatch(Card other);
}
