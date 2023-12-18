namespace UnoGame;


public class WildCard : Card
{
	// public WildCard()
	// {
	// 	Id = 0; // Set an appropriate value for the Id
	// 	Name = "Wild";
	// 	Color = Color.Black;
	// 	CardType = CardType.Wild;
	// 	IsSpecialAbility = true;
	// }

	public WildCard()
	{
		// Id = id;
		// Name = name;
		Color = Color.Black;
		CardType = CardType.Wild;
		IsSpecialAbility = true;
	}

	public override bool SpecialAbility(UnoGameController game)
	{
		// Implement special ability logic for WildCard
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
