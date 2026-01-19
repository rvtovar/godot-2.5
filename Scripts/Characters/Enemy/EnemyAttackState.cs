using System.Linq;
using Godot;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPos;

    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIM_ATTACK);

        Node3D target = characterNode.attackAreaNode.GetOverlappingBodies().First();

        targetPos = target.GlobalPosition;

        characterNode.animationPlayer.AnimationFinished += HandleAnimFinished;
    }

    protected override void ExitState()
    {
        characterNode.animationPlayer.AnimationFinished -= HandleAnimFinished;
    }

    private void HandleAnimFinished(StringName animName)
    {
        characterNode.ToggleHitBox(true);
        Node3D target = characterNode.attackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            Node3D ctarget = characterNode.chaseAreaNode.GetOverlappingBodies().FirstOrDefault();

            if (ctarget == null)
            {
                characterNode.stateMachine.SwitchState<EnemyReturnState>();
                return;
            }
            characterNode.stateMachine.SwitchState<EnemyChaseState>();
            return;
        }
        characterNode.animationPlayer.Play(GameConstants.ANIM_ATTACK);
        targetPos = target.GlobalPosition;

        Vector3 dir = characterNode.GlobalPosition.DirectionTo(targetPos);
        characterNode.sprite.FlipH = dir.X < 0;
    }

    private void PerformHit()
    {
        characterNode.ToggleHitBox(false);
        characterNode.hitBox.GlobalPosition = targetPos;
    }
}
