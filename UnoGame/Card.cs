namespace UnoGame;

public abstract class Card
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public Color Color { get; protected set; }
    public CardType CardType { get; protected set; }
    public bool IsSpecialAbility { get; set; }

    public abstract bool SpecialAbility(UnoGame game);
    public abstract bool IsCardMatch(Card other);

    public override int GetHashCode() => Id;
}
