namespace UnoGame;

public class Player
{
	public int Id { get; }
    public string Name { get; }

    public Player(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
