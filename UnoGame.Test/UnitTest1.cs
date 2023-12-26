namespace UnoGame.Test;

[TestFixture]
public class Tests
{
    private UnoGameController unoGameMaster;

    [SetUp]
    public void Setup()
    {
        unoGameMaster = new UnoGameController();
    }

    [Test]
    public void Get_ReturnCorrectDirection_SwitchAndGetGameDirection()
    {
        Direction desiredDirection = Direction.CounterClockwise;
        unoGameMaster.SwitchGameDirection(desiredDirection);
        Direction savedDirection = unoGameMaster.GetGameDirection();
        Assert.AreEqual(desiredDirection, savedDirection);
    }

    [TestCase(Color.Green)]
	[TestCase(Color.Blue)]
    [TestCase(Color.Red)]

    public void Get_ReturnCorrectColor_PickAndGetPickedColor(Color c)
    {
        Color pickedColor = c;
        unoGameMaster.PlayerPickColor(Color.Blue);
        Color actualColor = unoGameMaster.GetPickedColor();
        Assert.AreEqual(pickedColor, actualColor);
    }
}