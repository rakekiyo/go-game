using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    private Stone[] points;

    private Dictionary<Direction, int> movementPoints;

    public Goban(int size)
    {
        if (!Utils.isOdd(size))
        {
            throw new Exception("Goban size is only odd-number!");
        }

        var width = size + 2;   // 枠を含めた横幅

        this.points = initialize(width);

        this.movementPoints = new Dictionary<Common.Direction, int> {
            {Direction.Top, -width},
            {Direction.Left, -1},
            {Direction.Right, +1},
            {Direction.Bottom, + width},
        };
    }

    private Stone[] initialize(int width)
    {
        var points = new Stone[width * width];

        for (int i = 0; i < points.Length; i++)
        {
            bool isTopEdge = (i < width);   // 上端判定
            bool isLeftEdge = ((i % width) == 0);   // 左端判定
            bool isRightEdge = (((i - (width - 1)) % width) == 0);  // 右端判定
            bool isBottomEdge = (i > ((width * width - 1) - width));    // 下端判定

            if (isTopEdge || isLeftEdge || isRightEdge || isBottomEdge)
            {
                points[i] = Stone.Edge;
            }
            else
            {
                points[i] = Stone.Empty;
            }
        }

        return points;
    }
}