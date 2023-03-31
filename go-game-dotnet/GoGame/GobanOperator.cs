using System.Xml.Serialization;
using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public class GobanOperator
{
    private Goban goban;

    public GobanOperator(Goban goban)
    {
        this.goban = goban;
    }

    public enum MovingResult
    {
        OK,
        SUICIDE,    // 自殺手
        KO,         // コウ
        EYE,        // 目（ルール違反ではない）
        NOT_EMPTY,  // 既に石がある
        PASS
    }

    /// <summary>
    /// 石を置く関数
    /// </summary>
    public MovingResult Move(Stone stone, int pointIndex)
    {
        // 着手したい点の状態を取得
        var pointStatus = PointStatus.get(this.goban, stone, pointIndex);

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
            result = MovingResult.SUICIDE;  // 自殺手
            return false;
        }
        else if (pointStatus.Index == this.goban.KoPoint)
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
            if (neighbor.Stone == pointStatus.EnemyStone && this.goban.Points[neighbor.Index].Stone != Stone.Empty
            && neighbor.DameCount == 1)
            {
                this.takeUp(neighbor.Index, neighbor.Stone);    // 石を取る
                this.goban.Agehama[neighbor.Stone] += neighbor.StoneCount;
            }
        }

        this.goban.Points[pointStatus.Index].put(pointStatus.FriendStone);  // 石を置く

        this.goban.KoPoint = pointStatus.getKoPoint(this.goban) ?? 0;   // コウ判定
    }

    /// <summary>
    /// 石を取る関数
    /// </summary>
    private void takeUp(int index, Stone stone)
    {
        var aboveIndex = this.goban.getAboveIndex(index);
        var leftIndex = this.goban.getLeftIndex(index);
        var rightIndex = this.goban.getRightIndex(index);
        var belowIndex = this.goban.getBelowIndex(index);

        this.goban.Points[index].put(Stone.Empty);

        foreach (var neighborIndex in new int[] { aboveIndex, leftIndex, rightIndex, belowIndex })
        {
            if (this.goban.Points[neighborIndex].isSame(stone))
            {
                takeUp(neighborIndex, stone);
            }
        }
    }

    /// <summary>
    /// 8.5 地を数えて勝ちか負けかを返す関数
    /// </summary>
    public bool judgeWinOrLose(in Stone currentStone, in double komi, out double finalScore)
    {
        var score = 0;
        var existCount = new Dictionary<Stone, int>() {
            { Stone.Black, 0 },
            { Stone.White, 0 },
            { Stone.Empty, 0 }
        };

        for (int x = 0; x < this.goban.Size; x++)
            for (int y = 0; y < this.goban.Size; y++)
            {
                int pointIndex = this.goban.getPointIndex(x, y);
                Stone stone = this.goban.getStone(pointIndex);
                existCount[stone]++;

                if (stone != Stone.Empty)
                    continue;

                // 空点は周囲の石を種類別に数える
                var aroundCount = new Dictionary<Stone, int>() {
                    { Stone.Empty, 0 },
                    { Stone.Black, 0 },
                    { Stone.White, 0 },
                    { Stone.Edge, 0}
                };

                foreach (var neighbor in this.goban.getNeighbors(pointIndex))
                {
                    aroundCount[neighbor.Value]++;
                }

                if (aroundCount[Stone.Black] > 0 && aroundCount[Stone.White] == 0) score++; // 黒だけに囲まれていれば黒地
                if (aroundCount[Stone.White] > 0 && aroundCount[Stone.Black] == 0) score--; // 白だけに囲まれていれば白地
            }

        score += existCount[Stone.Black] - existCount[Stone.White];  // 現在の手番の石の数を加算

        finalScore = ((double)score) - komi;

        if (currentStone == Stone.Black)
            return finalScore > 0d;
        else
            return finalScore < 0d;
    }
}
