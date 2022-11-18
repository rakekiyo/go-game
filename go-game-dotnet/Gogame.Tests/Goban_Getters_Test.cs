using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public class Goban_Getters_Test
{
    [Fact(DisplayName = "初期化碁盤を取得(size=3)")]
    public void getCopyOfPoints_InitializeofSize3_ReturnPoints()
    {
        var goban = new Goban(3);
        var actual = goban.getCopyOfPoints();

        Assert.Equal(new Common.Stone[] {
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge
         }, actual);
    }

    [Fact(DisplayName = "初期化碁盤を取得(size=5)")]
    public void getCopyOfPoints_InitializeofSize5_ReturnPoints()
    {
        var goban = new Goban(5);
        var actual = goban.getCopyOfPoints();

        Assert.Equal(new Common.Stone[] {
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Empty, Stone.Edge,
            Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge, Stone.Edge
         }, actual);
    }

    [Theory(DisplayName = "上下左右の着手点位置を取得")]
    [InlineData(9, 24, Direction.Top, 13)]
    [InlineData(9, 24, Direction.Left, 23)]
    [InlineData(9, 24, Direction.Right, 25)]
    [InlineData(9, 24, Direction.Bottom, 35)]
    public void getDirectionPointIndex_AnyDirection_ReturnPoint(int gobanSize, int currentIndex, Direction actDirection, int expectedIndex)
    {
        var goban = new Goban(gobanSize);
        var actual = goban.getDirectionPointIndex(currentIndex, actDirection);

        Assert.Equal(expectedIndex, actual);
    }
}