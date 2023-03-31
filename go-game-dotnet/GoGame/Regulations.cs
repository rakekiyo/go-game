namespace rakekiyo.GoGame;

public class Regulations
{
    public double Komi { get; init; }    // コミ
    public int BoardSize { get; init; }   // 碁盤の大きさ

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Regulations(int boardSize, double komi)
    {
        this.BoardSize = boardSize;
        this.Komi = komi;
    }
}