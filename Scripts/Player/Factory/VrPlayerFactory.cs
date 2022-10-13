using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VrPlayerFactory
{
    private VrPlayerComponent vrPlayerComponent;

    public VrPlayerComponent CreateWithParentTransform(Transform parentTransform)
    {
        var playerPrefab = PrefabProviderUtility.getPlayerPrefab();
        var instantiatedPlayerGameObject = GameObject.Instantiate(playerPrefab, parentTransform);
        instantiatedPlayerGameObject.AddComponent<VrPlayerControllerComponent>();

        return instantiatedPlayerGameObject.AddComponent<VrPlayerComponent>();
    }

    public void ChangePlayerScale(float playerScaleMultiplier)
    {
        this.vrPlayerComponent.transform.localScale *= playerScaleMultiplier;
    }

}
