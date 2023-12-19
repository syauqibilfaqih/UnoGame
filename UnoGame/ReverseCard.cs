namespace UnoGame;

public class ReverseCard : Card
{
    public ReverseCard()
    {
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.Reverse;
        IsSpecialAbility = true;
    }

    public ReverseCard(Color color)
    {
        Color = color;
        CardType = CardType.Reverse;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for ReverseCard
        game.SwitchGameDirection();
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        if(other.Color == this.Color || other.CardType == this.CardType) 
            return true;
        else
            return false;
    }
}
