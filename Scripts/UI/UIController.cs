using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((elem) => elem is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary((elem) => elem.container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].buttonNode.Pressed += HandleStartPressed;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Start].Visible = !containers[ContainerType.Start].Visible;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
}
