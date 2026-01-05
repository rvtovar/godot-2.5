using Godot;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_IDLE);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachine.SwitchState<PlayerDashState>();
        }
    }
}
