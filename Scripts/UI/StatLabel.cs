using System;
using Godot;

public partial class StatLabel : Label
{
    // Called when the node enters the scene tree for the first time.
    [Export]
    private StatResource statResource;

    public override void _Ready()
    {
        statResource.OnUpdate += HandleUpdate;
        Text = statResource.statValue.ToString();
    }

    private void HandleUpdate()
    {
        Text = statResource.statValue.ToString();
    }
}
