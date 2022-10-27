using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public class Goban
{
    public PointState[] points;

    public Goban(int size)
    {
        if (!Utils.isOdd(size))
        {
            throw new Exception("Goban size is only odd-number!");
        }

        this.points = initialize(size);
    }

    // 正常、異常、パス
    public bool putStone(Stone stone, int pointIndex)
    {
        // TODO:判定

        // 碁盤を更新
        this.updatePoints(pointIndex, stone == Stone.Black ? PointState.Black : PointState.White);

        return true;
    }

    private PointState[] initialize(int size)
    {
        var width = size + 2;   // 枠を含めた横幅
        var points = new PointState[width * width];

        for (int i = 0; i < points.Length; i++)
        {
            bool isTopEdge = (i < width);   // 上端判定
            bool isLeftEdge = ((i % width) == 0);   // 左端判定
            bool isRightEdge = (((i - (width - 1)) % width) == 0);  // 右端判定
            bool isBottomEdge = (i > ((width * width - 1) - width));    // 下端判定

            if (isTopEdge || isLeftEdge || isRightEdge || isBottomEdge)
            {
                points[i] = PointState.Edge;
            }
            else
            {
                points[i] = PointState.Empty;
            }
        }

        return points;
    }

    private void updatePoints(int index, PointState state)
    {
        this.points[index] = state;
    }
}