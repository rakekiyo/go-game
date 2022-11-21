using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban : ICloneable
{
    private Stone[] points;

    private Dictionary<Direction, int> movementPoints;

    private int koPoint = 0;

    public Goban(int size)
    {
        if (!Utils.isOdd(size))
        {
            throw new Exception("Goban size is only odd-number!");
        }

        var width = size + 2;   // 枠を含めた横幅
        this.points = this.newPoints(width);
        this.movementPoints = this.newMovementPoints(width);

        this.koPoint = 0;
    }

    private Stone[] newPoints(int width)
    {
        var newPoints = new Stone[width * width];

        for (int i = 0; i < newPoints.Length; i++)
        {
            bool isTopEdge = (i < width);   // 上端判定
            bool isLeftEdge = ((i % width) == 0);   // 左端判定
            bool isRightEdge = (((i - (width - 1)) % width) == 0);  // 右端判定
            bool isBottomEdge = (i > ((width * width - 1) - width));    // 下端判定

            if (isTopEdge || isLeftEdge || isRightEdge || isBottomEdge)
            {
                newPoints[i] = Stone.Edge;
            }
            else
            {
                newPoints[i] = Stone.Empty;
            }
        }

        return newPoints;
    }

    private Dictionary<Direction, int> newMovementPoints(int boardWidth)
    {
        return new Dictionary<Direction, int> {
            {Direction.Top, -boardWidth},
            {Direction.Left, -1},
            {Direction.Right, +1},
            {Direction.Bottom, + boardWidth},
        };
    }

    public Object Clone()
    {
        return new Goban(1) //引数は奇数なら何でもいい
        {
            points = (Stone[])this.points.Clone(),
            movementPoints = new Dictionary<Direction, int>(this.movementPoints),
            koPoint = this.koPoint
        };
    }
}