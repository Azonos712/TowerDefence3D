using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandartTurret()
    {
        buildManager.SetTowerToBuild(buildManager.standartTowerPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        buildManager.SetTowerToBuild(buildManager.missileLauncherPrefab);
    }
}
