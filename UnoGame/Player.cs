namespace UnoGame;

public class Player : IPlayer
{
	public int Id { get;set; }
	public string? Name { get; set; } 

	/// <summary>
	/// A parametered constructor of class Player.
	/// </summary>
	/// <param name="id">The ID of player.</param>
	/// <param name="name">The Name of player.</param>
	public Player(int id, string? name)
	{
		Id = id;
		Name = name;
	}

	/// <summary>
	/// A constructor of class Player which assigns default value of Player's ID and Name.
	/// </summary>
	public Player()
	{
		Id = 0;
		Name = null;
	}
}
