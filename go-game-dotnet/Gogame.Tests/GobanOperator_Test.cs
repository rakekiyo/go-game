using rakekiyo.GoGame.Common;
using static rakekiyo.GoGame.GobanOperator;

namespace rakekiyo.GoGame;

public class GobanOperator_Test
{
    private struct MovingTestCase
    {
        public MovingResult Result { get; init; }
        public Stone Stone { get; init; }
        public int Index { get; init; }

        public MovingTestCase(MovingResult result, Stone stone, int index)
        {
            this.Result = result;
            this.Stone = stone;
            this.Index = index;
        }
    }

    [Fact(DisplayName = "石を取る")]
    private void move_takeup()
    {
        var goban9 = new Goban(9);
        var gobanOperator = new GobanOperator(goban9);
        var tengenIndex = goban9.getTengenIndex();

        foreach (var testCase in new MovingTestCase[] {
            new MovingTestCase(MovingResult.OK, Stone.White, tengenIndex ),  // 天元 -> 白
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getAboveIndex(tengenIndex) ), // 上 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getLeftIndex(tengenIndex) ), // 左 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getRightIndex(tengenIndex) ), // 右 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getBelowIndex(tengenIndex) ), // 下 -> 黒
        })
        {
            var expected = testCase.Result;
            var actual = gobanOperator.move(testCase.Stone, testCase.Index);
            Assert.Equal(expected, actual);
            GobanPrinter.print(goban9); // 動作確認用
        }

        Assert.Equal(Stone.Empty, goban9.getStone(tengenIndex));
    }
}