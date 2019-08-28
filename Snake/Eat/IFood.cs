using System.Drawing;

namespace Snake.Eat
{
    public interface IFood
    {
        int Witdh { get; }
        int Height { get; }
        Bitmap Image { get; }
    }
}
