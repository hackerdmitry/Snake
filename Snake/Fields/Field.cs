using System.Drawing;
using Snake.Properties;

namespace Snake
{
    public class Field : IField
    {
        public bool IsDeadField => false;
        public Bitmap Image => images.field;
    }
}
