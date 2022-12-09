using System.Drawing;
using System.Diagnostics;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

/// <summary>
/// 次に着手したい点の状態
/// </summary>
internal struct PointStatus
{
    public struct Neighbor
    {
        public Direction Direction { get; init; }
        public int Index { get; init; }
        public Stone Stone { get; init; }    // 色
        public int DameCount { get; internal set; }  // ダメ数
        public int StoneCount { get; internal set; } // 石数
    }

    public int Index { get; init; }
    public Stone FriendStone { get; init; }   // 味方の石色
    public Stone EnemyStone { get; init; }   // 相手の石色
    public Neighbor[] Neighbors { get; init; }
    public int EmptyCount { get; init; }     // 4方向の空白の数
    public int EdgeCount { get; init; }      // 4方向の盤外の数
    public int SaftyFriendStoneCount { get; init; }  // ダメ2以上で安全な見方の数
    public int TakeupStoneCount { get; init; }   // 取れる石の数
    public int MaybeKoPoint { get; init; }   // コウになるかもしれない場所

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PointStatus(in Goban goban, Stone friendStone, int index)
    {
        this.Index = index;
        this.FriendStone = friendStone;
        this.EnemyStone = (friendStone == Stone.Black) ? Stone.White : Stone.Black;
        this.Neighbors = new Neighbor[4];
        this.EmptyCount = 0;
        this.EdgeCount = 0;
        this.SaftyFriendStoneCount = 0;
        this.TakeupStoneCount = 0;
        this.MaybeKoPoint = 0;

        if (index == 0)
        {
            return;
        }

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            int neighborIndex = goban.getNeighborIndex(index, direction);
            Neighbor neighbor = new()
            {
                Direction = direction,
                Index = neighborIndex,
                Stone = goban.getStone(neighborIndex)
            };

            this.Neighbors[(int)direction] = neighbor;

            if (neighbor.Stone == Stone.Empty || neighbor.Stone == Stone.Edge)
            {
                if (neighbor.Stone == Stone.Empty) this.EmptyCount++;
                if (neighbor.Stone == Stone.Edge) this.EdgeCount++;

                continue;
            }
            else
            {
                (neighbor.DameCount, neighbor.StoneCount)
                    = countDame(goban, neighbor.Index); // (ダメの数, 石の数)

                if (neighbor.Stone == this.EnemyStone && neighbor.DameCount == 1)
                {
                    this.TakeupStoneCount += neighbor.StoneCount;
                    this.MaybeKoPoint = neighbor.Index;
                }

                if (neighbor.Stone == friendStone && neighbor.DameCount >= 2)
                {
                    this.SaftyFriendStoneCount++;
                }
            }
        }
    }

    /// <summary>
    /// 次に着手したい点の状態を取得する関数
    /// </summary>
    public static PointStatus get(in Goban goban, Stone friendStone, int pointIndex)
    {
        var pointStatus = new PointStatus(goban, friendStone, pointIndex);
        return pointStatus;
    }

    /// <summary>
    /// コウかどうか判定する
    /// </summary>
    /// <returns>()</returns>
    public int? getKoPoint(in Goban goban)
    {
        if (this.TakeupStoneCount == 1 && countDame(goban, this.Index).Equals((1, 1)))
        {
            return MaybeKoPoint;
        }

        return null;
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

        Stone[] points = goban.createPointsClone();
        bool[] pointsChecked = Enumerable.Repeat<bool>(false, points.Length).ToArray(); // 検索済フラグ

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
            int dirPointIndex = goban.getNeighborIndex(pointIndex, dir);
            Stone[] points = goban.createPointsClone();

            if (pointsChecked[dirPointIndex]) continue;

            if (points[dirPointIndex] == Stone.Empty)
            {
                pointsChecked[pointIndex] = true;   // この位置（空点）を検索済に
                dameCount++;    // ダメの数
            }

            // 未探索の自分の石
            if (points[dirPointIndex] == points[pointIndex])
            {
                countDameSubroutine(goban, ref pointsChecked, dirPointIndex, ref dameCount, ref stoneCount);
            }
        }
    }
}