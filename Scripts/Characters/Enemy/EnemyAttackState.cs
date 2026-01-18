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
    }

    private void PerformHit()
    {
        characterNode.ToggleHitBox(false);
        characterNode.hitBox.GlobalPosition = targetPos;
    }
}
