using rakekiyo.GoGame.Common;
using rakekiyo.GoGame.Players;
using static rakekiyo.GoGame.GobanOperator;

namespace rakekiyo.GoGame;

public class Taikyoku
{
    private Goban goban;
    private Player[] players;
    public Taikyoku(Regulations regulations)
    {
        this.goban = new Goban(Convert.ToInt32(regulations.BoradSize));
        this.players = new Player[2]
        {
            new RandomPlayer(regulations, Stone.Black),
            new RandomPlayer(regulations, Stone.White)
        };
    }

    /// <summary>
    /// 対局開始
    /// </summary>
    public void start()
    {
        var tesuu = 0;
        var gobanOperator = new GobanOperator(this.goban);

        var currentPlayer = players.First();

        while (true)
        {
            GobanPrinter.print((Goban)this.goban.Clone());

            // とりあえずテストは100手で終了
            if (tesuu == 100)
            {
                break;
            }

            // 手を選ぶ
            var nextIndex = currentPlayer.selectNextMove((Goban)this.goban.Clone());

            // 石を置く
            switch (gobanOperator.move(currentPlayer.Stone, nextIndex))
            {

                case MovingResult.PASS:
                case MovingResult.OK:
                    tesuu++;
                    currentPlayer = this.switchPlayer(currentPlayer);
                    GobanPrinter.print(this.goban);
                    break;
                case MovingResult.SUISIDE:
                case MovingResult.KO:
                case MovingResult.NOT_EMPTY:
                case MovingResult.EYE:  //現状、眼の数に関わらず打ててしまう
                default:
                    break;
            }
        }
    }

    private Player switchPlayer(in Player currentPlayer)
    {
        var currentStone = currentPlayer.Stone;
        var nextPlayer = this.players.Where(player => player.Stone != currentStone).First();
        return nextPlayer;
    }
}