using System;
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

    [Export]
    public Timer shaderTimer { get; private set; }

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

    private ShaderMaterial shader;

    public override void _Ready()
    {
        shader = (ShaderMaterial)sprite.MaterialOverlay;
        hurtBox.AreaEntered += HandleHurtboxEntered;

        sprite.TextureChanged += HandleTextureChanged;
        shaderTimer.Timeout += HandleShaderTimeout;
    }

    private void HandleShaderTimeout()
    {
        shader.SetShaderParameter("active", false);
    }

    private void HandleTextureChanged()
    {
        shader.SetShaderParameter("tex", sprite.Texture);
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
        if (area is not IHitbox hitbox)
        {
            return;
        }
        StatResource health = GetStatResource(Stat.Health);
        float damage = hitbox.GetDamage();
        health.statValue -= damage;
        shader.SetShaderParameter("active", true);
        shaderTimer.Start();
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
