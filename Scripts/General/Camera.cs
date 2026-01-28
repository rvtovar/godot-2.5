using System;
using Godot;

public partial class Camera : Camera3D
{
    [Export]
    private Node target;

    [Export]
    private Vector3 posFromTarget;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleStartGame()
    {
        Reparent(target);
        Position = posFromTarget;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
}
