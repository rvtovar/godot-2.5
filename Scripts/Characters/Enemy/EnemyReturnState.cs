using Godot;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_MOVE);

        characterNode.agentNode.TargetPosition = destination;
        characterNode.chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.agentNode.IsNavigationFinished())
        {
            characterNode.stateMachine.SwitchState<EnemyPatrolState>();
            return;
        }
        Move();
    }
}
