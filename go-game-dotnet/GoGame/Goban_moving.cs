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
        if (pointStatus.Index == 0)
        {
            result = MovingResult.PASS;  // パス
            return false;
        }
        else if (pointStatus.TakeupStoneCount == 0 && pointStatus.EmptyCount == 0 && pointStatus.SaftyFriendStoneCount == 0)
        {
            result = MovingResult.SUISIDE;  // 自殺手
            return false;
        }
        else if (pointStatus.Index == this.koPoint)
        {
            result = MovingResult.KO;   // コウ
            return false;
        }
        else if ((pointStatus.EdgeCount + pointStatus.SaftyFriendStoneCount) == 4)
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


    /// <summary>
    /// 碁盤を更新する
    /// </summary>
    /// <param name="pointStatus"></param>
    private void updatePoints(PointStatus pointStatus)
    {
        foreach (var neighbor in pointStatus.Neighbors)
        {
            if (neighbor.Stone == pointStatus.EnemyStone && this.points[neighbor.Index] != Stone.Empty
            && neighbor.DameCount == 1)
            {
                this.takeUp(neighbor.Index, neighbor.Stone);    // 石を取る
                this.agehama[(int)neighbor.Stone] += neighbor.StoneCount;
            }
        }

        this.points[pointStatus.Index] = pointStatus.FriendStone;  // 石を置く

        this.koPoint = pointStatus.getKoPoint(this) ?? 0;   // コウ判定
    }

    /// <summary>
    /// 石を取る関数
    /// </summary>
    private void takeUp(int index, Stone stone)
    {
        // TODO
    }
}