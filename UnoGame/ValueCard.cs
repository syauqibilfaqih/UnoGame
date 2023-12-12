namespace UnoGame;

public class ValueCard : Card
{
    public ValueCard()
    {
        Id = 0; // Set an appropriate value for the Id
        Name = "ValueCard";
        Color = Color.Red; // Set an appropriate default color
        // Specify a default CardType or set it in the constructor parameters
        CardType = CardType.Value0;
        IsSpecialAbility = false;
    }

    public ValueCard(int id, string name, Color color, CardType cardType)
    {
        Id = id;
        Name = name;
        Color = color;
        CardType = cardType;
        IsSpecialAbility = false;
    }

    public new bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for ValueCard (override the 'new' keyword)
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        return true;
    }

    public override int GetHashCode() => Id;
}
