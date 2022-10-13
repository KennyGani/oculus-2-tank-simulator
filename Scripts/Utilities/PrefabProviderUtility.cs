using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProviderUtility : MonoBehaviour
{
    [SerializeField]
    private GameObject tankPrefab;

    [SerializeField]
    private GameObject tankNpcPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject menuPrefab;

    [SerializeField]
    private GameObject jetPrefab;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private GameObject environmentPrefab;

    private static GameObject tankPrefabStatic;
    private static GameObject tankNpcPrefabStatic;
    private static GameObject playerPrefabStatic;
    private static GameObject menuPrefabStatic;
    private static GameObject jetPrefabStatic;
    private static GameObject obstaclePrefabStatic;
    private static GameObject environmentPrefabStatic;

    void Awake()
    {
        PrefabProviderUtility.tankPrefabStatic = this.tankPrefab;
        PrefabProviderUtility.tankNpcPrefabStatic = this.tankNpcPrefab;
        PrefabProviderUtility.playerPrefabStatic = this.playerPrefab;
        PrefabProviderUtility.menuPrefabStatic = this.menuPrefab;
        PrefabProviderUtility.jetPrefabStatic = this.jetPrefab;
        PrefabProviderUtility.obstaclePrefabStatic = this.obstaclePrefab;
        PrefabProviderUtility.environmentPrefabStatic = this.environmentPrefab;
    }

    public static GameObject getTankPrefab()
    {
        return PrefabProviderUtility.tankPrefabStatic;
    }

    public static GameObject getTankNpcPrefab()
    {
        return PrefabProviderUtility.tankNpcPrefabStatic;
    }

    public static GameObject getPlayerPrefab()
    {
        return PrefabProviderUtility.playerPrefabStatic;
    }

    public static GameObject getMenuPrefab()
    {
        return PrefabProviderUtility.menuPrefabStatic;
    }

    public static GameObject getJetPrefab()
    {
        return PrefabProviderUtility.jetPrefabStatic;
    }

    public static GameObject getObstaclePrefab()
    {
        return PrefabProviderUtility.obstaclePrefabStatic;
    }

    public static GameObject getEnvironmentPrefab()
    {
        return PrefabProviderUtility.environmentPrefabStatic;
    }
}
