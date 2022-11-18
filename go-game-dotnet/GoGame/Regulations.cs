namespace rakekiyo.GoGame;

public class Regulations
{
    public enum BoardSize : int
    {
        _9 = 9,
        _13 = 13,
        _19 = 19
    }

    public double Komi { get; init; }    // コミ
    public BoardSize BoradSize { get; init; }   // 碁盤の大きさ

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Regulations(BoardSize boradSize, double komi)
    {
        this.BoradSize = boradSize;
        this.Komi = komi;
    }
}