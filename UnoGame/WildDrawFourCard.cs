namespace UnoGame;

public class WildDrawFourCard : Card
{
    public WildDrawFourCard()
    {
        Color = Color.Black;
        CardType = CardType.WildDrawFour;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for WildDrawFourCard
        return true;
    }

    public bool SpecialAbility(UnoGameController game, IPlayer player, Color color)
    {
        game.DrawCard(player);
        game.DrawCard(player);
        game.DrawCard(player);
        game.DrawCard(player);
        game.PlayerPickColor(color);
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        return true;
    }
}
