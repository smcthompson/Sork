using Sork.World;

namespace Sork.Commands;

public class MoveCommand : BaseCommand
{
    private readonly IUserInputOutput io;

    public MoveCommand(IUserInputOutput io)
    {
        this.io = io;
    }

    public override bool Handles(string userInput, Player player)
    {
        return player.Location.GetExit(userInput) != null;
    }

    public override CommandResult Execute(string userInput, Player player)
    {
        var success = player.Location.MovePlayer(player, userInput);
        if (success == false) throw new Exception("You can't go that way.");
        io.WriteMessageLine($"You move to {player.Location.Name}.");
        return new CommandResult { RequestExit = false, IsHandled = true };
    }
}
