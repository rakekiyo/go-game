using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class RandomPlayer : Player
{
    private Random random;

    public RandomPlayer(Regulations regulations, Stone stone) : base(regulations, stone)
    {
        random = new Random(DateTime.Now.Millisecond);
    }

    public override int selectNextMove(in Goban goban)
    {
        Thread.Sleep(100);

        var points = goban.getPointsRef();
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