using MoreMountains.Tools;
/// <summary>
/// Called whenever a controller rebind event occurs.
/// </summary>
/// 
public struct RebindEvent
{
    public string displayString, deviceLayoutName, controlPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoreMountains.TopDownEngine.TopDownEngineEvent"/> struct.
    /// </summary>
    /// <param name="eventType">Event type.</param>
    public RebindEvent(string displayString, string deviceLayoutName, string controlPath)
    {
        this.displayString = displayString;
        this.deviceLayoutName = deviceLayoutName;
        this.controlPath = controlPath;
    }

    static RebindEvent e;
    public static void Trigger(string displayString, string deviceLayoutName, string controlPath)
    {
        e.displayString = displayString;
        e.deviceLayoutName = deviceLayoutName;
        e.controlPath = controlPath;
        MMEventManager.TriggerEvent(e);
    }
}

