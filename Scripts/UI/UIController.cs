using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class UIController : Control
{
    // -----------------------------------------------------------------
    // 1️⃣  Enum that mirrors the ContainerType you already use
    // -----------------------------------------------------------------
    public enum UiState
    {
        Start,
        Reward,
        Pause, /* add more here */
    }

    // -----------------------------------------------------------------
    // 2️⃣  Small struct that describes how each UI reacts to input
    // -----------------------------------------------------------------
    private struct UiHandler
    {
        public Func<bool> IsVisible; // current visibility test
        public Func<bool> IsClosePressed; // which input should close it
        public Action CloseAction; // what to run when closed
    }

    // -----------------------------------------------------------------
    // 3️⃣  Fields
    // -----------------------------------------------------------------
    private Dictionary<ContainerType, UIContainer> containers;
    private bool canPause = false;
    private readonly Dictionary<UiState, UiHandler> _uiHandlers = new();

    // -----------------------------------------------------------------
    // 4️⃣  Scene‑setup
    // -----------------------------------------------------------------
    public override void _Ready()
    {
        // Grab every UIContainer child and index it by its ContainerType
        containers = GetChildren()
            .Where(c => c is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary(c => c.container);

        // -----------------------------------------------------------------
        // 4a️⃣  Wire button signals (unchanged from your original code)
        // -----------------------------------------------------------------
        containers[ContainerType.Start].buttonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].buttonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Reward].buttonNode.Pressed += handleRewardPressed;

        // -----------------------------------------------------------------
        // 4b️⃣  Subscribe to game events (unchanged)
        // -----------------------------------------------------------------
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnReward += handleReward;

        // -----------------------------------------------------------------
        // 4c️⃣  Register UI‑handlers (visibility + input mapping)
        // -----------------------------------------------------------------
        RegisterHandlers();

        // -----------------------------------------------------------------
        // 4d️⃣  **Show the initial screen** – all panels are hidden in the
        //      scene, so we explicitly turn the Start panel on here.
        // -----------------------------------------------------------------
        containers[ContainerType.Start].Visible = true;
    }

    // -----------------------------------------------------------------
    // 5️⃣  Register each UI state once
    // -----------------------------------------------------------------
    private void RegisterHandlers()
    {
        // ----- Start screen -------------------------------------------------
        _uiHandlers[UiState.Start] = new UiHandler
        {
            IsVisible = () => containers[ContainerType.Start].Visible,
            IsClosePressed = () => Input.IsActionJustPressed(GameConstants.INPUT_INTERACT),
            CloseAction = HandleStartPressed,
        };

        // ----- Reward popup -------------------------------------------------
        _uiHandlers[UiState.Reward] = new UiHandler
        {
            IsVisible = () => containers[ContainerType.Reward].Visible,
            // You can close the reward with either START or INTERACT – pick what feels best
            IsClosePressed = () => Input.IsActionJustPressed(GameConstants.INPUT_START),
            CloseAction = handleRewardPressed,
        };

        // ----- Global pause toggle (no dedicated UI node) --------------------
        _uiHandlers[UiState.Pause] = new UiHandler
        {
            // Pause handling is only relevant when pausing is allowed
            IsVisible = () => canPause,
            IsClosePressed = () => Input.IsActionJustPressed(GameConstants.INPUT_START),
            CloseAction = TogglePause,
        };

        // -----------------------------------------------------------------
        // 👉 Add more UI states here (e.g., Settings, Tutorial, Shop) by
        //    creating a new entry with its own visibility test, input
        //    trigger, and callback.
        // -----------------------------------------------------------------
    }

    // -----------------------------------------------------------------
    // 6️⃣  Centralized input handling – loops over the registry
    // -----------------------------------------------------------------
    public override void _Input(InputEvent @event)
    {
        foreach (var handler in _uiHandlers.Values)
        {
            if (handler.IsVisible() && handler.IsClosePressed())
            {
                handler.CloseAction();
                // Stop after the first matching UI consumes the input
                return;
            }
        }
    }

    // -----------------------------------------------------------------
    // 7️⃣  Individual UI actions (unchanged from your original code)
    // -----------------------------------------------------------------
    private void TogglePause()
    {
        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        // Detach the button listener – we don’t need it after the game has started
        containers[ContainerType.Start].buttonNode.Pressed -= HandleStartPressed;
        GameEvents.RaiseStartGame();
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void handleReward(RewardResource resource)
    {
        canPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;

        containers[ContainerType.Reward].textureNode.Texture = resource.spriteTexture;
        containers[ContainerType.Reward].labelNode.Text = resource.description;
    }

    private void handleRewardPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }
}
