using System;
using Godot;

public partial class EnemyPatrolState : EnemyState
{
    [Export]
    private Timer idleTimerNode;

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float maxIdleTime = 4;
    private int pointIndex = 0;

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_MOVE);
        pointIndex = 1;
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.agentNode.TargetPosition = destination;

        characterNode.agentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimeout;
        characterNode.chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.agentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        characterNode.chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
    }

    private void HandleNavigationFinished()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimerNode.Start();
    }

    private void HandleTimeout()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_MOVE);
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.pathNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.agentNode.TargetPosition = destination;
    }
}
