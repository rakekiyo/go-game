using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// コピーされた着手点を取得
    /// </summary>
    public Stone[] getPoints()
    {
        return this.points;
    }

    public int getDirectionPointIndex(int pointIndex, Direction dir)
    {
        return pointIndex + movementPoints[dir];
    }
}