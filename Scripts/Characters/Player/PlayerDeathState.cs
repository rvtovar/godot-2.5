using System;
using Godot;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        characterNode.animationPlayer.Play(GameConstants.ANIME_DEATH);
        characterNode.animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        GameEvents.RaiseEndGame();
        characterNode.QueueFree();
    }
}
