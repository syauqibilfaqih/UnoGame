namespace UnoGame;

public class ValueCard : Card
{
    public ValueCard()
    {
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.Value0;
        IsSpecialAbility = false;
    }

    public ValueCard(Color color, CardType cardType)
    {
        Color = color;
        CardType = cardType;
        IsSpecialAbility = false;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for ValueCard (override the 'new' keyword)
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        if(other.Color == this.Color || other.CardType == this.CardType || other.CardType == CardType.Wild|| other.CardType == CardType.WildDrawFour)
            return true;
        else
            return false;
    }
}
