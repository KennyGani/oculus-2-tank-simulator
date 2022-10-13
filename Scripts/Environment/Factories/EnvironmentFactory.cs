using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFactory
{
    public EnvironmentComponent CreateEnvironment()
    {
        var environmentPrefab = PrefabProviderUtility.getEnvironmentPrefab();
        var instantiatedEnvironmentGameObject = GameObject.Instantiate(environmentPrefab);

        return instantiatedEnvironmentGameObject.AddComponent<EnvironmentComponent>();
    }
}
