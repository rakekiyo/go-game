using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// コピーされた着手点を取得
    /// </summary>
    public Stone[] getPointsCopy()
    {
        return (Stone[])this.points.Clone();
    }

    public Stone getStone(int pointIndex)
    {
        return this.points[pointIndex];
    }

    /// <summary>
    /// 上下左右の位置を取得
    /// </summary>
    public int getNeighborIndex(int pointIndex, Direction dir)
    {
        return pointIndex + movementDistances[dir];
    }
}