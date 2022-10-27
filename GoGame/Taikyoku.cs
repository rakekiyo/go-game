using rakekiyo.GoGame.Common;
using rakekiyo.GoGame.Players;

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
            new TestPlayer(regulations, Stone.Black),
            new TestPlayer(regulations, Stone.White)
        };
    }

    /// <summary>
    /// 対局開始
    /// </summary>
    public void start()
    {
        var tesuu = 0;
        var currentPlayer = players.First();

        GobanPrinter.print(this.goban);

        while (true)
        {
            // 手を選ぶ
            var selectedMove = currentPlayer.selectNextMove(this.goban.points);

            // 石を置く
            if (this.goban.putStone(currentPlayer.Stone, selectedMove))
            {
                tesuu++;

                currentPlayer = this.changePlayer(currentPlayer.Stone);
                GobanPrinter.print(this.goban);
            }

            // とりあえずテストは100手で終了
            if (tesuu == 100)
            {
                break;
            }
        }
    }

    private Player changePlayer(Stone currentStone)
    {
        return this.players.Where(player => player.Stone != currentStone).First();
    }
}