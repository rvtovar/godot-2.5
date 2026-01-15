using System;
using Godot;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_IDLE);
        characterNode.chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.stateMachine.SwitchState<EnemyReturnState>();
    }
}
