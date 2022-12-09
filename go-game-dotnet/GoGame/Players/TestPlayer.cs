using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class TestPlayer : Player
{
    private Random random;

    public TestPlayer(Regulations regulations, Stone stone) : base(regulations, stone)
    {
        random = new Random(DateTime.Now.Millisecond);
    }

    public override int selectNextMove(in Goban goban)
    {
        var points = goban.createPointsClone();

        Thread.Sleep(500);

        while (true)
        {
            var nextIndex = random.Next(0, points.Length - 1);
            if (points[nextIndex] == Stone.Empty)
            {
                return nextIndex;
            }
        }
    }
}