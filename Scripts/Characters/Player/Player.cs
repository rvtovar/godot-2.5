using Godot;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer animationPlayer { get; private set; }

    [Export]
    public Sprite3D sprite { get; private set; }

    [Export]
    public StateMachine stateMachine { get; private set; }
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
