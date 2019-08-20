namespace Snake
{
    public interface IEntity
    {
        void Move();
        void Move(Direction direction);
        void OnPaint(Drawing drawing);
    }
}
