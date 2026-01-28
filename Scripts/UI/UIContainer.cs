using System;
using Godot;

public partial class UIContainer : Container
{
    [Export]
    public ContainerType container { get; private set; }

    [Export]
    public Button buttonNode { get; private set; }

    [Export]
    public TextureRect textureNode { get; private set; }

    [Export]
    public Label labelNode { get; private set; }
}
