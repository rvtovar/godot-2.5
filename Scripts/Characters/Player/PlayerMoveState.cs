using Godot;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")]
    private float speed = 5;

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= speed;
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
