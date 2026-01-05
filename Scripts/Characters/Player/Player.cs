using Godot;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer animationPlayer = null;

    [Export]
    public Sprite3D sprite = null;

    [Export]
    public StateMachine stateMachine = null;
    public Vector2 direction = new();

    public override void _Ready() { }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_UP,
            GameConstants.INPUT_MOVE_DOWN
        );
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally)
            return;
        bool isMovingLeft = Velocity.X < 0;
        sprite.FlipH = isMovingLeft;
    }
}
