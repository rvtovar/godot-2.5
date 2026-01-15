using System;
using System.Linq;
using Godot;

public partial class EnemyChaseState : EnemyState
{
    [Export]
    private Timer timerNode;

    private CharacterBody3D target;

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_MOVE);

        target = characterNode.chaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        timerNode.Timeout += HandleTimeout;
        characterNode.attackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.chaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        timerNode.Timeout -= HandleTimeout;

        characterNode.attackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.chaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.agentNode.TargetPosition = destination;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.stateMachine.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.stateMachine.SwitchState<EnemyReturnState>();
    }
}
