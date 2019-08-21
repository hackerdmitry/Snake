namespace Snake
{
    public interface IEntity
    {
        bool Move();
        bool Move(Direction direction);
        void OnPaint(Drawing drawing);
    }
}
