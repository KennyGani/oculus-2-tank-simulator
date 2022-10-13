using System;

[Serializable]
public class TankTransformPath
{
    public string PathTransformName;
    public float SpeedTransform;

    public TankTransformPath(string pathTransformName, float speedTransform)
    {
        this.PathTransformName = pathTransformName;
        this.SpeedTransform = speedTransform;
    }
}