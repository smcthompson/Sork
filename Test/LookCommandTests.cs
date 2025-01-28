using Sork.Commands;
using Sork.World;

namespace Sork.Tests;

[TestClass]
public sealed class LookCommandTests
{
    [TestMethod]
    public void Handle_ShouldReturnTrue_WhenInputIsCapitalized()
    {
        // Arrange
        var command = new LookCommand(new UserInputOutput());

        // Act
        var result = command.Handles("LOOK");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Execute_ShouldDisplayRoomInfo()
    {
        // Arrange
        var io = new TestInputOutput();
        var command = new LookCommand(io);
        var gameState = GameState.Create(io);

        // Act
        var result = command.Execute("LOOK", gameState);

        // Assert
        var tavernInventoryCount = gameState.Player.Location.Inventory.Count;
        Assert.IsTrue(result.IsHandled);
        Assert.IsFalse(result.RequestExit);
        Assert.AreEqual(8 + tavernInventoryCount, io.Outputs.Count); // Name, blank, desc, blank, "Exits:", exit, blank, "Inventory:", inventory
        Assert.AreEqual("Tavern", io.Outputs[0]);
        Assert.AreEqual("", io.Outputs[1]);
        Assert.AreEqual("A warm, friendly establishment serving food, beverage, and entertainment.", io.Outputs[2]);
        Assert.AreEqual("", io.Outputs[3]);
        Assert.AreEqual("Exits:", io.Outputs[4]);
        Assert.AreEqual("down - A dark, dank crypt that is filled with silence and dread.", io.Outputs[5]);
        Assert.AreEqual("", io.Outputs[6]);
        Assert.AreEqual("Inventory:", io.Outputs[7]);
        Assert.IsTrue(io.Outputs.Skip(7).Any(o => o == "Sword - A double-edged blade that would cleave flesh like a knife through butter."));
    }
}