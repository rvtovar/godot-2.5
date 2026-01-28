using System;
using Godot;

public partial class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();

        hurtBox.AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area3D area)
    {
        if (area is not IHitbox hitbox)
            return;
        if (hitbox.CanStun() && GetStatResource(Stat.Health).statValue != 0)
        {
            stateMachine.SwitchState<EnemyStunState>();
        }
    }
}
