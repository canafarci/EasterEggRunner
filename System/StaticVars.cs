using UnityEngine;

public enum RaycastState
{
    Inactive,
    Click,
    Drag,
    Swipe
}

public enum RepairStage
{
    Wall,
    Floor,
    Window,
    Paint,
    Mopping,
    Reset
}
public static class CameraStrings
{
    public static string FirstCamera = "FirstCamera";
    public static string SecondCamera = "SecondCamera";
    public static string SecondCameraLeft = "SecondCameraLeft";
    public static string SecondCameraRight = "SecondCameraRight";
    public static string SecondCameraFinal = "SecondCameraFinal";
}
public static class PrefKeys
{
    public static string Money = "Money";
    public static string GameplayLoop = "GameplayLoop";
}

public static class AnimationHashes
{
    
}