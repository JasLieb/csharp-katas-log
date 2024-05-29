namespace Bowling;

using System.Linq;

public class Game
{
    private const int Unlucky = 0;
    private const int Strike = 10;
    private const int LastPossibleRoll = 20;

    private readonly List<int> _savedRolls = new();

    public void Roll(int rollScore) =>
        _savedRolls.Add(rollScore);

    public int Score() =>
        _savedRolls.Select(ScoreRoll).Sum();

    private int ScoreRoll(int rollValue, int indexRoll)
    {
        if (IsSpare(rollValue, indexRoll))
            return rollValue + GetRollValueAt(indexRoll + 1);
        if (rollValue is Strike && indexRoll < LastPossibleRoll - 10)
            return ScoreStrikeWithBonus(indexRoll);
        return rollValue is not Strike &&  indexRoll != LastPossibleRoll 
            ? rollValue : 0 ;
    }

    private int ScoreStrikeWithBonus(int indexRoll) =>
        Strike + GetRollValueAt(indexRoll + 1) + GetRollValueAt(indexRoll + 2);

    private bool IsSpare(int rollValue, int indexRoll) =>
        rollValue is not Unlucky
        && indexRoll % 2 != 0
        && rollValue + GetRollValueAt(indexRoll - 1) is Strike;

    private int GetRollValueAt(int indexRoll) =>
        _savedRolls.ElementAtOrDefault(indexRoll);
}
