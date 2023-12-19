namespace UnoGame;

public class Player : IPlayer
{
	public int Id { get;set; }
	public string? Name { get; set; } 

	public Player(int id, string? name)
	{
		Id = id;
		Name = name;
	}

	public Player()
	{
		Id = 0;
		Name = null;
	}
}
