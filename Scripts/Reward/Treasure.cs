using System;
using Godot;

public partial class Treasure : StaticBody3D
{
    [Export]
    private Area3D areaNode;

    [Export]
    private Sprite3D spriteNode;

    [Export]
    private RewardResource reward;

    public override void _Ready()
    {
        areaNode.BodyEntered += (body) => spriteNode.Visible = true;
        areaNode.BodyExited += (body) => spriteNode.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        // Check if overlapping
        // Check if interact was pushed
        if (
            !areaNode.Monitoring
            || !areaNode.HasOverlappingBodies()
            || !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)
        )
        {
            return;
        }

        areaNode.Monitoring = false;
        GameEvents.RaiseReward(reward);
    }
}
