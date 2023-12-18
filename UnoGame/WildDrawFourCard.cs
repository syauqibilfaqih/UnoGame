namespace UnoGame;

public class WildDrawFourCard : Card
{
    public WildDrawFourCard()
    {
        // Id = 0; // Set an appropriate value for the Id
        // Name = "Wild Draw Four";
        Color = Color.Black;
        CardType = CardType.WildDrawFour;
        IsSpecialAbility = true;
    }

    // public WildDrawFourCard(int id, string name)
    // {
    //     Id = id;
    //     Name = name;
    //     Color = Color.Black;
    //     CardType = CardType.WildDrawFour;
    //     IsSpecialAbility = true;
    // }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for WildDrawFourCard
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
