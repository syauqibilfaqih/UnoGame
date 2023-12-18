namespace UnoGame;

public class ReverseCard : Card
{
    public ReverseCard()
    {
        // Id = 0; // Set an appropriate value for the Id
        // Name = "Reverse";
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.Reverse;
        IsSpecialAbility = true;
    }

    public ReverseCard(/*int id, string name,*/ Color color)
    {
        // Id = id;
        // Name = name;
        Color = color;
        CardType = CardType.Reverse;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for ReverseCard
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        if(other.Color == this.Color || other.CardType == this.CardType || other.CardType == CardType.Wild || other.CardType == CardType.WildDrawFour) 
            return true;
        else
            return false;
    }

    // public override int GetHashCode() => Id;
}
