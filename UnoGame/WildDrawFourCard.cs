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
        int index = game.GetPlayerNow().Id;
        index = index+(int)game.GetGameDirection();
        if(index > game.GetMaxPlayers()-1)
			index = index-(game.GetMaxPlayers());
		if(index < 0)
			index = (game.GetMaxPlayers())+index;
        game.SwitchPlayer(index);
        var player = game.GetPlayerNow();
        game.DrawCard(player);
        game.DrawCard(player);
        game.DrawCard(player);
        game.DrawCard(player);
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        return true;
    }
}
