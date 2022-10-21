using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class TestPlayer : Player
{

    public TestPlayer(Regulations regulations, Teban teban) : base(regulations, teban)
    { }

    public override int selectNextMove(in PointState[] points)
    {
        var nextMove = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == PointState.Empty)
            {
                nextMove = i;
                break;
            }
        }

        return nextMove;
    }
}