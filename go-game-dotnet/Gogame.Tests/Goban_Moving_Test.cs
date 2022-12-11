using rakekiyo.GoGame.Common;
using static rakekiyo.GoGame.Goban;

namespace rakekiyo.GoGame;

public class Goban_Moving_Test
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
        var tengenIndex = goban9.getTengenIndex();

        foreach (var testCase in new MovingTestCase[] {
            new MovingTestCase(MovingResult.OK, Stone.White, tengenIndex ),  // 天元 -> 白
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getAboveIndex(tengenIndex) ), // 上 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getLeftIndex(tengenIndex) ), // 左 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getRightIndex(tengenIndex) ), // 右 -> 黒
            new MovingTestCase(MovingResult.OK, Stone.Black, goban9.getBelowIndex(tengenIndex) ), // 下 -> 黒
            new MovingTestCase(MovingResult.SUISIDE, Stone.White, tengenIndex ), // 天元 -> 白（自殺手）
            new MovingTestCase(MovingResult.EYE, Stone.Black, tengenIndex ), // 天元 -> 黒（眼を埋める）
        })
        {
            var expected = testCase.Result;
            var actual = goban9.move(testCase.Stone, testCase.Index);
            Assert.Equal(expected, actual);
            // GobanPrinter.print(goban9); // 動作確認用
        }
    }
}