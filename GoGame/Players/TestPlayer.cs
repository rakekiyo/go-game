using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class TestPlayer : Player
{

    public TestPlayer(Regulations regulations, Stone teban) : base(regulations, teban)
    { }

    public override int selectNextMove(in Stone[] points)
    {
        var nextMove = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == Stone.Empty)
            {
                nextMove = i;
                break;
            }
        }

        return nextMove;
    }
}