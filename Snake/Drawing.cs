using System.Drawing;

namespace Snake
{
    public class Drawing
    {
        Graphics graphics;

        public void SetGraphics(Graphics graphics) => this.graphics = graphics;
        
        public Graphics GetGraphics() => graphics;

        public void DrawImage(Image image, float x, float y, Direction direction = Direction.Right)
        {
            RotateImage(image, direction);
            graphics.DrawImage(image,
                               x * Game.LENGTH_SIDE, y * Game.LENGTH_SIDE,
                               Game.LENGTH_SIDE, Game.LENGTH_SIDE);
        }

        static void RotateImage(Image image, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    break;
                case Direction.Down:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case Direction.Left:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
            }
        }
    }
}
