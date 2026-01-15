using Godot;

public partial class Player : Character
{
    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_UP,
            GameConstants.INPUT_MOVE_DOWN
        );
    }
}
