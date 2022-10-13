using System.ComponentModel;

public enum PlayerDeviceControllerActionType
{
    [Description("Trigger")]
    Trigger = 0,

    [Description("Analog-Axis-Right")]
    AnalogAxisRight = 1,

    [Description("Analog-Axis-Left")]
    AnalogAxisLeft = 2,

    [Description("Analog-Axis-Reset")]
    AnalogAxisReset = 3
}


