/// <summary>
/// For classes that have some sort of reset functionality (e.g. at end of game)
/// </summary>
public interface IReset
{
    void Reset ();
}

/// <summary>
/// For companion components of the UI_Button system. Replaces Unity's in-built UI Button system.
/// </summary>
public interface IUserInterfaceInteractionCompanion
{
    // Called when a UI element is hovered over
    void OnHover ();
    // Called when a UI element is clicked or otherwise first selected
    void OnPressed ();
    // Called on the first frame that the mouse button is pressed down on a UI element
    void OnSelect ();
    // Called when a UI element is deselected
    void OnDeselect ();
    // Called in order to Reset the component back to its starting state
    void Reset ();
}