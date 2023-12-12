namespace UnoGame;

public class DrawTwoCard : Card
{
    public DrawTwoCard()
    {
        Id = 0; // Set an appropriate value for the Id
        Name = "Draw Two";
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.DrawTwo;
        IsSpecialAbility = true;
    }

    public DrawTwoCard(int id, string name, Color color)
    {
        Id = id;
        Name = name;
        Color = color;
        CardType = CardType.DrawTwo;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for DrawTwoCard
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        return true;
    }

    public override int GetHashCode() => Id;
}
