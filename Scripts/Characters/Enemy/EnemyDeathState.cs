using System;
using Godot;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIME_DEATH);
        characterNode.animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.QueueFree();
    }
}
