using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public abstract class Player
{
    private Regulations regulations;
    public Teban Teban { get; init; }

    public Player(Regulations regulations, Teban teban)
    {
        this.regulations = regulations;
        this.Teban = teban;
    }

    public abstract int selectNextMove(in PointState[] points);
}