using Sork.World;

namespace Sork.Commands;

public class DropCommand : BaseCommand
{
    private readonly IUserInputOutput io;
    public DropCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput)
    {
        return GetCommandFromInput(userInput) == "drop";
    }

    public override CommandResult Execute(string userInput, GameState gameState)
    {
        var parameters = GetParametersFromInput(userInput);
        if (parameters.Count() != 1)
        {
            io.WriteMessageLine("What are you trying to drop again?");
            return new CommandResult { RequestExit = false, IsHandled = true };
        }
        var noun = parameters[0];

        var item = gameState.Player.Inventory.FirstOrDefault(i =>
            i.Name.Equals(noun, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            io.WriteMessageLine($"You have to have to be holding a {noun} to drop it.");
            return new CommandResult { RequestExit = false, IsHandled = true };
        }

        gameState.Player.Inventory.Remove(item);
        gameState.Player.Location.Inventory.Add(item);
        io.WriteMessageLine($"The {item.Name} falls from your hands.");

        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}