using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    [Export]
    public Stat statType { get; private set; }

    private float _statValue;

    [Export]
    public float statValue
    {
        get => _statValue;
        set { _statValue = Mathf.Clamp(value, 0, Mathf.Inf); }
    }
}
