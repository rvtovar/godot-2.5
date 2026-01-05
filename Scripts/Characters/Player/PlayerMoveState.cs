using Godot;

public partial class PlayerMoveState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= 5;
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
            characterNode.stateMachine.SwitchState<PlayerDashState>();
    }

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_RUNNING);
    }
}
