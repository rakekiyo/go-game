using System.Text;
using System.Linq;
using rakekiyo.GoGame.Common;

namespace rakekiyo.GoGame;

public static class GobanPrinter
{
    public static void print(in Goban goban)
    {
        var points = goban.getPointsCopy();
        var width = Convert.ToInt32(System.Math.Sqrt(points.Length));

        var buffer = new StringBuilder();
        for (int i = 0; i < width; i++)
        {
            var line = points.Skip(i * width).Take(width)
                .Select(point =>
                {
                    return point.Stone switch
                    {
                        Stone.Black => "1",
                        Stone.White => "2",
                        Stone.Edge => "■",
                        _ => "."
                    };
                })
                .ToArray();
            buffer.AppendLine(String.Join(" ", line));
        }

        Console.WriteLine(buffer.ToString());
    }
}