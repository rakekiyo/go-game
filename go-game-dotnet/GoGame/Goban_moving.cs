using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    public enum MovingResult
    {
        OK,
        SUISIDE,    // 自殺手
        KO,         // コウ
        EYE,        // 目（ルール違反ではない）
        NOT_EMPTY,  // 既に石がある
        PASS
    }

    /// <summary>
    /// 石を置く関数
    /// </summary>
    public MovingResult move(Stone stone, int pointIndex)
    {
        // 着手したい点の状態を取得
        var pointStatus = PointStatus.get(this, stone, pointIndex);

        if (this.canMove(pointStatus, out MovingResult result))
        {
            // 碁盤を更新
            this.updatePoints(pointStatus);
        }

        return result;
    }

    private bool canMove(in PointStatus pointStatus, out MovingResult result)
    {
        if (pointStatus.pointIndex == 0)
        {
            result = MovingResult.PASS;  // パス
            return false;
        }
        else if (pointStatus.preyStoneCount == 0 && pointStatus.emptyCount == 0 && pointStatus.saftyFriendStoneCount == 0)
        {
            result = MovingResult.SUISIDE;  // 自殺手
            return false;
        }
        else if (pointStatus.pointIndex == this.koPoint)
        {
            result = MovingResult.KO;   // コウ
            return false;
        }
        else if ((pointStatus.edgeCount + pointStatus.saftyFriendStoneCount) == 4)
        {
            result = MovingResult.EYE;  // 眼（ルール違反ではない）
            return true;
        }
        else
        {
            result = MovingResult.OK;
            return true;
        }
    }


    private void updatePoints(PointStatus pointStatus)
    {
        foreach (var dirStatus in pointStatus.aroundStatus)
        // {
        //     if (dirStatus == 1 && dirStatus.stone == pointStatus.enemyStone
        //     && this.points[this.getDirectionPointIndex(pointStatus.pointIndex, pointStatus.di)])
        // }


        /* スタブ */
        {
            this.points[pointStatus.pointIndex] = pointStatus.friendStone;
        }
    }


}