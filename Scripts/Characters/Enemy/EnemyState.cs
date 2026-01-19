using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPos = characterNode.pathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = characterNode.pathNode.GlobalPosition;
        return localPos + globalPos;
    }

    protected void Move()
    {
        characterNode.agentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        characterNode.stateMachine.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        characterNode.stateMachine.SwitchState<EnemyDeathState>();
    }
}
