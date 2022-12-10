using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public class Goban_Getters_Test
{
    [Theory(DisplayName = "碁盤の初期化")]
    [InlineData(3, new Common.Stone[] {
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge
         })]
    [InlineData(5, new Common.Stone[] {
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge
         })]
    private void Initialize(int gobanSize, Stone[] expectedStones)
    {
        var points = new Goban(gobanSize).getPointsCopy();

        for (int i = 0; i < points.Length; i++)
        {
            var expected = expectedStones[i];
            var actual = points[i].Stone;

            Assert.Equal(expected, actual);
        }
    }

    [Theory(DisplayName = "隣位置を取得")]
    [InlineData(9, 24, Direction.Top, 13)]
    [InlineData(9, 24, Direction.Left, 23)]
    [InlineData(9, 24, Direction.Right, 25)]
    [InlineData(9, 24, Direction.Bottom, 35)]
    private void getNeighborIndex(int gobanSize, int index, Direction direction, int expected)
    {
        var goban = new Goban(gobanSize);
        var actual = goban.getNeighborIndex(index, direction);

        Assert.Equal(expected, actual);
    }
}