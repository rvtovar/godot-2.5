using System;
using Godot;

public partial class PlayerAttackState : PlayerState
{
    [Export]
    private Timer comboTimer;

    [Export(PropertyHint.Range, "0.5,1.0,0.05")]
    private float distanceMult = 0.75f;

    // Called when the node enters the scene tree for the first time.
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();

        comboTimer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 2.5f);

        characterNode.animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.animationPlayer.AnimationFinished -= HandleAnimationFinished;
        comboTimer.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.ToggleHitBox(true);
        characterNode.stateMachine.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPos = characterNode.sprite.FlipH ? Vector3.Left : Vector3.Right;
        newPos *= distanceMult;
        characterNode.hitBox.Position = newPos;
        characterNode.ToggleHitBox(false);
    }
}
