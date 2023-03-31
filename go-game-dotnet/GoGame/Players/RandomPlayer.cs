using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class RandomPlayer : IPlayer
{
    public Stone Stone { get; init; }

    private Random random;

    public RandomPlayer(Regulations regulations, Stone stone)
    {
        this.Stone = stone;
        this.random = new Random(DateTime.Now.Millisecond);
    }

    public int selectNextMove(in Goban goban)
    {
        Thread.Sleep(100);

        var points = goban.getPointsCopy();
        while (true)
        {
            var nextIndex = random.Next(0, points.Length - 1);
            if (points[nextIndex].isSame(Stone.Empty))
            {
                return nextIndex;
            }
        }
    }
}