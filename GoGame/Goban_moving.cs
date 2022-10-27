using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    public enum MovingResult
    {
        OK,
        NG,
        PASS
    }

    private struct PointStatus
    {
        public struct DirectionStatus
        {
            int dameCount;  // ダメ数
            int stoneCount; // 石数
            Stone stone;    // 色
        }

        public Dictionary<Direction, DirectionStatus> aroundStatus;   // 4方向の状態
        public Stone enemyStone;   // 相手の石色
        public int emptyCount;     // 4方向の空白の数
        public int edgeCount;      // 4方向の盤外の数
        public int saftyFriendStoneCount;  // ダメ2以上で安全な見方の数
        public int preyStoneCount;   // 取れる石の数
        public int potentialKoPoint;   // コウになるかもしれない場所

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PointStatus()
        {
            this.aroundStatus = new Dictionary<Direction, DirectionStatus>() {
                {Direction.Top, new DirectionStatus()},
                {Direction.Right, new DirectionStatus()},
                {Direction.Left, new DirectionStatus()},
                {Direction.Botom, new DirectionStatus()},
            };

            this.enemyStone = Stone.Empty;
            this.emptyCount = 0;
            this.edgeCount = 0;
            this.saftyFriendStoneCount = 0;
            this.preyStoneCount = 0;
            this.potentialKoPoint = 0;
        }
    }

    public MovingResult move(Stone stone, int pointIndex)
    {
        if (pointIndex == 0) return MovingResult.PASS;

        // 碁盤を更新
        this.updatePoints(pointIndex, stone);

        return MovingResult.OK;
    }

    private static PointStatus getPointStatus(in Goban goban, Stone stone, int pointIndex)
    {
        var pointStatus = new PointStatus();

        foreach (var dir in Enum.GetValues<Direction>())
        {
            int dirPointIndex = pointIndex + goban.getMovementPoint(dir);
            Stone dirStone = goban.getPoints()[dirPointIndex];

            if (dirStone == Stone.Empty || dirStone == Stone.Edge)
            {
                if (dirStone == Stone.Empty) pointStatus.emptyCount++;
                if (dirStone == Stone.Edge) pointStatus.edgeCount++;

                continue;
            }
            else
            {

            }
        }


        return pointStatus;
    }

    private void updatePoints(int index, Stone stone)
    {
        this.points[index] = stone;
    }
}