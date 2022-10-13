using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory
{
    public ObstacleComponent CreateObstacle()
    {
        var obstaclePrefab = PrefabProviderUtility.getObstaclePrefab();
        var instantiatedObstacleGameObject = GameObject.Instantiate(obstaclePrefab);

        return instantiatedObstacleGameObject.AddComponent<ObstacleComponent>();
    }
}
