using Godot;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.stateMachine.SwitchState<PlayerAttackState>();
        }
    }

    private void HandleZeroHealth()
    {
        characterNode.stateMachine.SwitchState<PlayerDeathState>();
    }
}
