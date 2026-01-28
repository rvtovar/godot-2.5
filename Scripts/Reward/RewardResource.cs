using System;
using Godot;

[GlobalClass]
public partial class RewardResource : Resource
{
    [Export]
    public Texture2D spriteTexture { get; private set; }

    [Export]
    public string description { get; private set; }

    [Export]
    public Stat targetStat { get; private set; }

    [Export(PropertyHint.Range, "1,100,1")]
    public float Amount { get; private set; }
}
