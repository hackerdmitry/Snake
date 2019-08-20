using System.Drawing;
using Snake.Properties;

namespace Snake
{
    public class Wall : IField
    {
        public bool IsDeadField => true;
        public Bitmap Image => images.wall;
    }
}
