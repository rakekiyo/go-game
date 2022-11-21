using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public class Goban_Getters_Test
{
    [Theory(DisplayName = "上下左右の着手点位置を取得")]
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
    public void getPoints_Initialize_ReturnPoints(int gobanSize, Stone[] expectedPoints)
    {
        var goban = new Goban(gobanSize);
        var actual = goban.getPoints();

        Assert.Equal(expectedPoints, actual);
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