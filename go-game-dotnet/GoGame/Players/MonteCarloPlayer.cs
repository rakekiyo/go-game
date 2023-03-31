using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public class MonteCarloPlayer : IPlayer
{
    public Stone Stone { get; init; }

    private const int playOutCount = 30; // プレイアウトを繰り返す回数

    private int bestMove; // 最善手

    public MonteCarloPlayer(Regulations regulations, Stone stone)
    {
        this.Stone = stone;
    }

    public int selectNextMove(in Goban goban)
    {
        var points = goban.getPointsCopy();



        return 0;
    }
}