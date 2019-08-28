using System.Drawing;
using Snake.Properties;

namespace Snake.Eat
{
    public class SmallFood : IFood
    {
        public int Witdh { get; } = 2;
        public int Height { get; } = 2;
        public Bitmap Image { get; } = images.smallFood;
    }
}
