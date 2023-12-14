namespace UnoGame;

public class Player : IPlayer
{
	public int Id { get;set; }
	public string? Name { get; set; } 
	public Color color {get;set;}

	public Player(int id, string? name)
	{
		Id = id;
		Name = name;
	}
	

	// public override int GetHashCode()
	// {
	//     return Id.GetHashCode();
	// }
}
