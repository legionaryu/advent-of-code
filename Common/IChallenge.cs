namespace AdventOfCode.Common
{
    public interface IChallenge
    {
        string Input { get; }

        string SolvePartOne();
        string SolvePartTwo();
    }
}
