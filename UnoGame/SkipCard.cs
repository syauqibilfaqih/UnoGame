namespace UnoGame;

public class SkipCard : Card
{
    public SkipCard()
    {
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.Skip;
        IsSpecialAbility = true;
    }

    public SkipCard(Color color)
    {
        Color = color;
        CardType = CardType.Skip;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for SkipCard
        return true;
    }

    public bool SpecialAbility(UnoGameController game, int indexPlayer)
    {
        game.SwitchPlayer(indexPlayer);
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
}
