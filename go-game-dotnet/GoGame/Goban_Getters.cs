using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// コピーされた着手点を取得
    /// </summary>
    public Stone[] getCopyOfPoints()
    {
        var copiedPoints = new Stone[this.points.Length];

        Array.Copy(this.points, copiedPoints, this.points.Length);

        return copiedPoints;
    }

    // TODO:これのテストもしたい
    public int getDirectionPointIndex(int pointIndex, Direction dir)
    {
        return pointIndex + movementPoints[dir];
    }
}