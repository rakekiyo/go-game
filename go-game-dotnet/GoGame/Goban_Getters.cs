using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// 着手点のコピーを取得
    /// </summary>
    public Point[] getPointsCopy()
    {
        return (Point[])this.points.Clone();
    }

    public Point getPoint(int pointIndex)
    {
        return this.points[pointIndex];
    }

    /// <summary>
    /// 天元のindexを取得
    /// </summary>
    public int getTengenIndex()
    {
        return this.points.Length / 2;
    }

    // /// <summary>
    // /// 上下左右の位置を取得
    // /// </summary>
    public int getNeighborIndex(int pointIndex, Direction dir)
    {
        return pointIndex + movementDistances[dir];
    }

    /// <summary>
    /// 指定indexxの上のindexを取得
    /// </summary>
    public int getAboveIndex(int index)
    {
        return index + movementDistances[Direction.Above];
    }

    /// <summary>
    /// 指定indexxの左のindexを取得
    /// </summary>
    public int getLeftIndex(int index)
    {
        return index + movementDistances[Direction.Left];
    }

    /// <summary>
    /// 指定indexxの右のindexを取得
    /// </summary>
    public int getRightIndex(int index)
    {
        return index + movementDistances[Direction.Right];
    }

    /// <summary>
    /// 指定indexxの下のindexを取得
    /// </summary>
    public int getBelowIndex(int index)
    {
        return index + movementDistances[Direction.Below];
    }
}