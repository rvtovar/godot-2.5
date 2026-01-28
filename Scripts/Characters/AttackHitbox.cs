using System;
using Godot;

public partial class AttackHitbox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return false;
    }

    public float GetDamage()
    {
        return GetOwner<Character>().GetStatResource(Stat.Strength).statValue;
    }
}
