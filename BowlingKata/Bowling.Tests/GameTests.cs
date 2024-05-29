namespace Bowling.Tests;

public class GameTests
{
    private readonly Game _game = new();
    
    [Fact]
    public void AlwaysRollZeroShouldScoreZero()
    {
        RollOnlyZero();
        Assert.Equal(0, _game.Score());
    }

    [Theory]
    [InlineData(1, 20)]
    [InlineData(2, 40)]
    [InlineData(3, 60)]
    [InlineData(9, 180)]
    public void GameWithSimpleScoreShouldScoreRollScoreSum(
        int actualRollScore,
        int expectedScore
    )
    {
        RollXTimes(actualRollScore, 20);
        Assert.Equal(expectedScore, _game.Score());
    }

    [Fact]
    public void RollOneSpareShouldCountNextRollBonus()
    {
        _game.Roll(5);
        _game.Roll(5);
        RollXTimes(1, 1);
        Assert.Equal(12, _game.Score());
    }

    [Theory]
    [InlineData(1, 14)]
    [InlineData(0, 10)]
    public void RollOneStrikeShouldCountNextTwoRollsBonus(
        int nextRollScore,
        int expectedScore
    )
    {
        _game.Roll(10);
        RollXTimes(nextRollScore, 2);
        Assert.Equal(expectedScore, _game.Score());
    }

    [Theory]
    [InlineData(1, 20)]
    [InlineData(2, 40)]
    [InlineData(3, 60)]
    [InlineData(9, 180)]
    public void GameWithSimpleRoll(
        int actualRollScore,
        int expectedScore
    )
    {
        RollXTimes(actualRollScore, 20);
        Assert.Equal(expectedScore, _game.Score());
    }

    [Theory]
    [InlineData(1, 2, 30)]
    [InlineData(2, 4, 50)]
    [InlineData(3, 10, 74)]
    public void GameWithSimpleRollAndLastSpareWithSimpleBonus(
        int actualRollScore,
        int actualBonusRollScore,
        int expectedScore
    )
    {
        RollXTimes(actualRollScore, 18);
        RollXTimes(5, 2);
        RollXTimes(actualBonusRollScore, 1);
        Assert.Equal(expectedScore, _game.Score());
    }

    [Fact]
    public void GameWithOnlyStrikesShouldReturnMaxScore()
    {
        RollXTimes(10, 12);
        Assert.Equal(300, _game.Score());
    }

    private void RollOnlyZero()
    {
        RollXTimes(0, 20);
    }

    private void RollXTimes(int rollScore, int countRoll)
    {
        for (int i = 0; i < countRoll; i++)
        {
            _game.Roll(rollScore);
        }
    }
}