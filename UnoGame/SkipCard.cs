namespace UnoGame;

public class SkipCard : Card
{
    public SkipCard()
    {
        Id = 0; // Set an appropriate value for the Id
        Name = "Skip";
        Color = Color.Red; // Set an appropriate default color
        CardType = CardType.Skip;
        IsSpecialAbility = true;
    }

    public SkipCard(int id, string name, Color color)
    {
        Id = id;
        Name = name;
        Color = color;
        CardType = CardType.Skip;
        IsSpecialAbility = true;
    }

    public override bool SpecialAbility(UnoGameController game)
    {
        // Implement special ability logic for SkipCard
        return true;
    }

    public override bool IsCardMatch(Card other)
    {
        // Implement logic to check if this card matches another card
        return true;
    }

    public override int GetHashCode() => Id;
}
