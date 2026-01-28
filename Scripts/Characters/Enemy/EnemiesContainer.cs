using System;
using Godot;

public partial class EnemiesContainer : Node3D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        int totalEnemies = GetChildCount();
        GameEvents.RaiseEnemyCount(totalEnemies);

        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        int totalEnemies = GetChildCount() - 1;
        GameEvents.RaiseEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }
}
