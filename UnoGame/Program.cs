using UnoGame;

class Program
{
    static void Main()
    {
        List<IPlayer> players = new List<IPlayer>();

        Console.WriteLine("Enter player information. To stop, type 'exit'.");

        while (true)
        {
            Console.Write("Enter player Id: ");
            string idInput = Console.ReadLine();

            if (idInput.ToLower() == "exit")
                break;

            if (!int.TryParse(idInput, out int playerId))
            {
                Console.WriteLine("Invalid input for player Id. Please enter a valid integer.");
                continue;
            }

            Console.Write("Enter player name: ");
            string playerName = Console.ReadLine();

            players.Add(new Player(playerId, playerName));

            Console.WriteLine("Player added successfully!");
        }

        Console.WriteLine("\nPlayers Information:");
        foreach (var player in players)
        {
            Console.WriteLine($"Player Id: {player.Id}, Player Name: {player.Name}");
        }

        Console.WriteLine("Program ended. Press any key to exit.");
        Console.ReadKey();
    }
}