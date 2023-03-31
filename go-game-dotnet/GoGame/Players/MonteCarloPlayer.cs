using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class MonteCarloPlayer : IPlayer
{
    public Stone Stone { get; init; }

    public MonteCarloPlayer(Regulations regulations, Stone stone)
    {
    }

    public int selectNextMove(in Goban goban)
    {
        return 0;
    }
}