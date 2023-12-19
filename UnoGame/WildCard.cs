namespace UnoGame;


public class WildCard : Card
{
	public WildCard()
	{
		Color = Color.Black;
		CardType = CardType.Wild;
		IsSpecialAbility = true;
	}

	public override bool SpecialAbility(UnoGameController game)
	{
		// Implement special ability logic for WildCard
		return true;
	}

	public bool SpecialAbility(UnoGameController game, Color color)
	{
		game.PlayerPickColor(color);
		return true;
	}

	public override bool IsCardMatch(Card other)
	{
		// Implement logic to check if this card matches another card
		return true;
	}
}
