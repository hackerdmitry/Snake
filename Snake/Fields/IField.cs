using System.Drawing;

namespace Snake
{
    public interface IField
    {
        bool IsDeadField { get; }
        Bitmap Image { get; }
    }
}
