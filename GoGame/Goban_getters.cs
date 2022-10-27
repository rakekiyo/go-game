using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    public Stone[] getPoints()
    {
        var copiedPoints = new Stone[this.points.Length];

        Array.Copy(this.points, copiedPoints, this.points.Length);

        return copiedPoints;
    }

    public int getMovementPoint(Direction dir)
    {
        return movementPoints[dir];
    }
}