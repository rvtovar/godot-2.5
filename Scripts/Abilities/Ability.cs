using System;
using Godot;

public abstract partial class Ability : Node3D
{
    [Export]
    protected AnimationPlayer playerNode { get; private set; }

    [Export]
    public float damage { get; private set; } = 10;
}
