using System.Security.Cryptography.X509Certificates;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame.Players;

public interface IPlayer
{
    public Stone Stone { get; }
    public int selectNextMove(in Goban goban);
}