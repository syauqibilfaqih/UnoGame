namespace UnoGame;

public abstract class Card
{
    /// <summary>
    /// Gets or sets the color of the card.
    /// </summary>
    public Color Color { get; protected set; }

    /// <summary>
    /// Gets or sets the type of the card.
    /// </summary>
    public CardType CardType { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether the card has special abilities.
    /// </summary>
    public bool IsSpecialAbility { get; set; }

    /// <summary>
    /// Executes the special ability of the card in the context of the Uno game.
    /// </summary>
    /// <param name="game">The Uno game controller.</param>
    /// <returns>True if the special ability is successfully executed, false otherwise.</returns>
    public abstract bool SpecialAbility(UnoGameController game);

    /// <summary>
    /// Checks if the card matches another card.
    /// </summary>
    /// <param name="other">The other card to compare.</param>
    /// <returns>True if the cards match, false otherwise.</returns>
    public abstract bool IsCardMatch(Card other);
}
