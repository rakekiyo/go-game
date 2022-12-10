using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// 着手点の参照取得
    /// </summary>
    public Point[] getPointsRef()
    {
        return this.points;
    }
    /// <summary>
    /// 着手点のコピーを取得
    /// </summary>
    public Point[] getPointsCopy()
    {
        return (Point[])this.points.Clone();
    }

    public Stone getStone(int pointIndex)
    {
        return this.points[pointIndex].Stone;
    }

    /// <summary>
    /// 上下左右の位置を取得
    /// </summary>
    public int getNeighborIndex(int pointIndex, Direction dir)
    {
        return pointIndex + movementDistances[dir];
    }
}