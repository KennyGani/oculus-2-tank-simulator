using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetFactory
{
    public JetComponent CreateJet(Vector3 jetPosition, Vector3 jetRotation, float jetScaleFactor, bool mustHideAtBeginning)
    {
        var jetPrefab = PrefabProviderUtility.getJetPrefab();
        var instantiatedJetGameObject = GameObject.Instantiate(jetPrefab, jetPosition, Quaternion.Euler(jetRotation));
        instantiatedJetGameObject.SetActive(!mustHideAtBeginning);
        return instantiatedJetGameObject.AddComponent<JetComponent>();
    }
}
