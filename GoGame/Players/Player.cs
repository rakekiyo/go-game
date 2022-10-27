using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public abstract class Player
{
    private Regulations regulations;
    public Stone Stone { get; init; }

    public Player(Regulations regulations, Stone stone)
    {
        this.regulations = regulations;
        this.Stone = stone;
    }

    public abstract int selectNextMove(in PointState[] points);
}