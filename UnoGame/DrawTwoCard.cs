namespace UnoGame;

public class DrawTwoCard : Card
{
    public DrawTwoCard()
    {
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.DrawTwo;
        IsSpecialAbility = true;
    }

    public DrawTwoCard(Color color)
    {
        Color = color;
        CardType = CardType.DrawTwo;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for DrawTwoCard
        return true;
    }

    public bool SpecialAbility(UnoGameController game, IPlayer player)
    {
        game.DrawCard(player);
        game.DrawCard(player);
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
