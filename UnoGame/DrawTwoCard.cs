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
