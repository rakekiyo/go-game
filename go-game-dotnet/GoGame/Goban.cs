using System.Security.Principal;
using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban : ICloneable
{
    public int width { private get; init; }
    public Point[] Points { get; init; }
    public Dictionary<Stone, int> Agehama { get; init; }
    public int KoPoint { get; set; }

    public Goban(int size)
    {
        if (!Utils.isOdd(size))
        {
            throw new Exception("Goban size is only odd-number!");
        }

        this.width = size + 2;   // 枠を含めた横幅
        this.Points = this.newPoints();
        this.Agehama = this.newAgehama();

        this.KoPoint = 0;
    }

    private Point[] newPoints()
    {
        var width = this.width;
        var points = new Point[width * width];

        for (int i = 0; i < points.Length; i++)
        {
            bool isTopEdge = (i < width);   // 上端判定
            bool isLeftEdge = ((i % width) == 0);   // 左端判定
            bool isRightEdge = (((i - (width - 1)) % width) == 0);  // 右端判定
            bool isBottomEdge = (i > ((width * width - 1) - width));    // 下端判定

            bool isEdge = (isTopEdge || isLeftEdge || isRightEdge || isBottomEdge);
            points[i].put(isEdge ? Stone.Edge : Stone.Empty);
        }

        return points;
    }

    private Dictionary<Stone, int> newAgehama()
    {
        return new Dictionary<Stone, int>
        {
            {Stone.Black, 0},
            {Stone.White, 0}
        };
    }

    public Object Clone()
    {
        return new Goban(1) //引数は奇数なら何でもいい
        {
            Points = (Point[])this.Points.Clone(),
            Agehama = new Dictionary<Stone, int>(this.Agehama),
            KoPoint = this.KoPoint
        };
    }

    /// <summary>
    /// 着手点のコピーを取得
    /// </summary>
    public Point[] getPointsCopy()
    {
        return (Point[])this.Points.Clone();
    }

    public Stone getStone(int pointIndex)
    {
        return this.Points[pointIndex].Stone;
    }

    /// <summary>
    /// 天元のindexを取得
    /// </summary>
    public int getTengenIndex()
    {
        return this.Points.Length / 2;
    }

    // /// <summary>
    // /// 上下左右の位置を取得
    // /// </summary>
    public int getNeighborIndex(int pointIndex, Direction direction)
    {
        return direction switch
        {
            Direction.Above => this.getAboveIndex(pointIndex),
            Direction.Left => this.getLeftIndex(pointIndex),
            Direction.Right => this.getRightIndex(pointIndex),
            _ => this.getBelowIndex(pointIndex),
        };
    }

    /// <summary>
    /// 指定indexxの上のindexを取得
    /// </summary>
    public int getAboveIndex(int index)
    {
        return index - this.width; ;
    }

    /// <summary>
    /// 指定indexxの左のindexを取得
    /// </summary>
    public int getLeftIndex(int index)
    {
        return index - 1;
    }

    /// <summary>
    /// 指定indexxの右のindexを取得
    /// </summary>
    public int getRightIndex(int index)
    {
        return index + 1;
    }

    /// <summary>
    /// 指定indexxの下のindexを取得
    /// </summary>
    public int getBelowIndex(int index)
    {
        return index + this.width;
    }
}