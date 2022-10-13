using System.ComponentModel;

public enum TankSeatingName
{
    [Description("front-right")]
    FrontRight = 1,

    [Description("rear-right")]
    RearRight = 0,

    [Description("rear-left")]
    RearLeft = 2,

    [Description("editor-mode")]
    EditorMode = 3,
}