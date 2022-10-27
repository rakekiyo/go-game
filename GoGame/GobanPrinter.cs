using System.Text;
using System.Linq;

namespace rakekiyo.GoGame;

public static class GobanPrinter
{
    public static void print(Goban goban)
    {
        var points = goban.getPoints();
        var width = Convert.ToInt32(System.Math.Sqrt(points.Length));

        var buffer = new StringBuilder();
        for (int i = 0; i < width; i++)
        {
            var line = points.Skip(i * width).Take(width).Select(state => Convert.ToInt32(state)).ToArray();
            buffer.AppendLine(String.Join(" ", line));
        }

        Console.WriteLine(buffer.ToString());
    }
}