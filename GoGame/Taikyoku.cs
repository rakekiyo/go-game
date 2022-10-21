using rakekiyo.GoGame.Common;
using rakekiyo.GoGame.Players;

namespace rakekiyo.GoGame;

public class Taikyoku
{
    private Goban goban;

    private Player sentePlayer;
    private Player gotePlayer;

    public Taikyoku(Regulations regulations)
    {
        this.goban = new Goban(Convert.ToInt32(regulations.BoradSize));
        this.sentePlayer = new TestPlayer(regulations, Teban.Sente);
        this.gotePlayer = new TestPlayer(regulations, Teban.Gote);
    }

    /// <summary>
    /// 対局開始
    /// </summary>
    public void start()
    {
        var tesuu = 0;
        var currentPlayer = sentePlayer;

        GobanPrinter.print(this.goban);

        while (true)
        {
            // 手を選ぶ
            var selectedMove = currentPlayer.selectNextMove(this.goban.points);

            // 石を置く
            if (this.goban.putStone(currentPlayer.Teban, selectedMove))
            {
                tesuu++;

                currentPlayer = this.changePlayer(currentPlayer.Teban);
                GobanPrinter.print(this.goban);
            }

            // とりあえずテストは100手で終了
            if (tesuu == 100)
            {
                break;
            }
        }
    }

    private Player changePlayer(Teban currentTeban)
    {
        return (currentTeban == Teban.Sente) ? this.gotePlayer : this.sentePlayer;
    }
}