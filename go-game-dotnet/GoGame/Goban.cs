using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban : ICloneable
{
    private Point[] points;
    private Dictionary<Stone, int> agehama;

    private Dictionary<Direction, int> movementDistances;

    private int koPoint = 0;

    public Goban(int size)
    {
        if (!Utils.isOdd(size))
        {
            throw new Exception("Goban size is only odd-number!");
        }

        int width = size + 2;   // 枠を含めた横幅

        this.points = this.newPoints(width);
        this.agehama = this.newAgehama();
        this.movementDistances = this.newMovementDistances(width);

        this.koPoint = 0;
    }

    private Point[] newPoints(int width)
    {
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

    private Dictionary<Direction, int> newMovementDistances(int boardWidth)
    {
        return new Dictionary<Direction, int> {
            {Direction.Above, -boardWidth},
            {Direction.Left, -1},
            {Direction.Right, +1},
            {Direction.Below, + boardWidth},
        };
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
            points = (Point[])this.points.Clone(),
            movementDistances = new Dictionary<Direction, int>(this.movementDistances),
            koPoint = this.koPoint
        };
    }
}