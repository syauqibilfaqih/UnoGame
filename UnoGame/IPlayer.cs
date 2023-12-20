namespace UnoGame;

public interface IPlayer
{
    /// <summary>
	/// Gets or sets the Id of player.
	/// </summary>
	public int Id { get;set; }

    /// <summary>
	/// Gets or sets the Name of player.
	/// </summary>
    public string? Name { get; set;}
}
