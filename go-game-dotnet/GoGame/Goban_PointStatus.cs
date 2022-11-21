using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public partial class Goban
{
    /// <summary>
    /// 次に着手したい点の状態
    /// </summary>
    private struct PointStatus
    {
        public int pointIndex;

        public struct DirectionStatus
        {
            public int dameCount;  // ダメ数
            public int stoneCount; // 石数
            public Stone stone;    // 色
        }

        public DirectionStatus[] aroundStatus;   // 4方向の状態
        public Stone friendStone;   // 味方の石色
        public Stone enemyStone;   // 相手の石色
        public int emptyCount;     // 4方向の空白の数
        public int edgeCount;      // 4方向の盤外の数
        public int saftyFriendStoneCount;  // ダメ2以上で安全な見方の数
        public int preyStoneCount;   // 取れる石の数
        public int potentialKoPoint;   // コウになるかもしれない場所

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PointStatus(Stone friendStone, int pointIndex)
        {
            this.aroundStatus = new DirectionStatus[4];

            this.pointIndex = pointIndex;
            this.friendStone = friendStone;
            this.enemyStone = (friendStone == Stone.Black) ? Stone.White : Stone.Black;
            this.emptyCount = 0;
            this.edgeCount = 0;
            this.saftyFriendStoneCount = 0;
            this.preyStoneCount = 0;
            this.potentialKoPoint = 0;
        }

        /// <summary>
        /// 次に着手したい点の状態を取得する関数
        /// </summary>
        public static PointStatus get(in Goban goban, Stone friendStone, int pointIndex)
        {
            var pointStatus = new PointStatus(friendStone, pointIndex);

            if (pointIndex == 0)
            {
                return pointStatus;
            }

            foreach (var dir in Enum.GetValues<Direction>())
            {
                int dirPointIndex = goban.getDirectionPointIndex(pointIndex, dir);
                Stone dirStone = goban.points[dirPointIndex];

                if (dirStone == Stone.Empty || dirStone == Stone.Edge)
                {
                    if (dirStone == Stone.Empty) pointStatus.emptyCount++;
                    if (dirStone == Stone.Edge) pointStatus.edgeCount++;

                    continue;
                }
                else
                {
                    (int dameCount, int stoneCount) = countDame(goban, pointIndex); // (ダメの数, 石の数)

                    pointStatus.aroundStatus[(int)dir].stone = dirStone;
                    pointStatus.aroundStatus[(int)dir].dameCount = dameCount;
                    pointStatus.aroundStatus[(int)dir].stoneCount = stoneCount;

                    if (dirStone == pointStatus.enemyStone && dameCount == 1)
                    {
                        pointStatus.preyStoneCount += stoneCount;
                        pointStatus.potentialKoPoint = dirPointIndex;
                    }

                    if (dirStone == friendStone && dameCount >= 2)
                    {
                        pointStatus.saftyFriendStoneCount++;
                    }
                }
            }

            return pointStatus;
        }

        // 
        /// <summary>
        /// ダメの数を数える関数 
        /// </summary>
        /// <returns>(ダメの数, 石の数)</returns>
        private static (int, int) countDame(in Goban goban, int pointIndex)
        {
            int dameCount = 0;
            int stoneCount = 0;

            var pointsChecked = Enumerable.Repeat<bool>(false, goban.points.Length).ToArray();

            countDameSubroutine(goban, ref pointsChecked, pointIndex, ref dameCount, ref stoneCount);

            return (dameCount, stoneCount);
        }

        /// <summary>
        /// ダメと石数を数える再帰関数
        /// </summary>
        private static void countDameSubroutine(in Goban goban, ref bool[] pointsChecked, int pointIndex, ref int dameCount, ref int stoneCount)
        {
            pointsChecked[pointIndex] = true;    // この位置（石）はチェック済

            stoneCount++;   // 石の数

            // ４方向を調べて、空白なら+1、自分の石なら再帰で、相手の石・壁はそのまま
            foreach (var dir in Enum.GetValues<Direction>())
            {
                int dirPointIndex = goban.getDirectionPointIndex(pointIndex, dir);

                if (pointsChecked[dirPointIndex]) continue;

                if (goban.points[dirPointIndex] == Stone.Empty)
                {
                    pointsChecked[pointIndex] = true;   // この位置（空点）を検索済に
                    dameCount++;    // ダメの数
                }

                // 未探索の自分の石
                if (goban.points[dirPointIndex] == goban.points[pointIndex])
                {
                    countDameSubroutine(goban, ref pointsChecked, dirPointIndex, ref dameCount, ref stoneCount);
                }
            }
        }
    }
}