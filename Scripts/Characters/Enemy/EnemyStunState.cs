using System;
using Godot;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.animationPlayer.Play(GameConstants.ANIM_STUN);
        characterNode.animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.animationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (characterNode.attackAreaNode.HasOverlappingBodies())
        {
            characterNode.stateMachine.SwitchState<EnemyAttackState>();
        }
        else if (characterNode.chaseAreaNode.HasOverlappingBodies())
        {
            characterNode.stateMachine.SwitchState<EnemyChaseState>();
        }
        else
        {
            characterNode.stateMachine.SwitchState<EnemyIdleState>();
        }
    }
}
