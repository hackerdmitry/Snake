using System.Drawing;

namespace Snake
{
    public class Drawing
    {
        Graphics graphics;

        public void SetGraphics(Graphics graphics) => this.graphics = graphics;

        public void DrawImage(Image image, int x, int y)
        {
            graphics.DrawImage(image,
                               x * Game.LENGTH_SIDE, y * Game.LENGTH_SIDE,
                               Game.LENGTH_SIDE, Game.LENGTH_SIDE);
        }
    }
}
