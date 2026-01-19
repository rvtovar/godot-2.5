using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody3D
{
    [Export]
    private StatResource[] stats;

    // Required Nodes
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer animationPlayer { get; private set; }

    [Export]
    public Sprite3D sprite { get; private set; }

    [Export]
    public StateMachine stateMachine { get; private set; }

    [Export]
    public Area3D hurtBox { get; private set; }

    [Export]
    public Area3D hitBox { get; private set; }

    [Export]
    public CollisionShape3D hitboxShape { get; private set; }

    // AI Nodes
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

    public override void _Ready()
    {
        hurtBox.AreaEntered += HandleHurtboxEntered;
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally)
            return;
        bool isMovingLeft = Velocity.X < 0;
        sprite.FlipH = isMovingLeft;
    }

    private void HandleHurtboxEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        Character player = area.GetOwner<Character>();
        health.statValue -= player.GetStatResource(Stat.Strength).statValue;
        GD.Print(health.statValue);
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where((elem) => elem.statType == stat).FirstOrDefault();
    }

    public void ToggleHitBox(bool flag)
    {
        hitboxShape.Disabled = flag;
    }
}
