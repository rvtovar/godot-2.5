using Godot;

public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer animationPlayer { get; private set; }

    [Export]
    public Sprite3D sprite { get; private set; }

    [Export]
    public StateMachine stateMachine { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export]
    public Path3D pathNode { get; private set; }

    [Export]
    public NavigationAgent3D agentNode { get; private set; }

    [Export]
    public Area3D chaseAreaNode { get; private set; }

    [Export]
    public Area3D attackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready() { }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally)
            return;
        bool isMovingLeft = Velocity.X < 0;
        sprite.FlipH = isMovingLeft;
    }
}
