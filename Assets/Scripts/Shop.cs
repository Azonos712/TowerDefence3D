using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint standartTower;
    public TowerBlueprint missileLauncher;
    public TowerBlueprint laserBeamer;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        SetBorder(0);
        buildManager.SelectTowerToBuild(standartTower);
    }

    public void SelectMissileLauncher()
    {
        SetBorder(1);
        buildManager.SelectTowerToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        SetBorder(2);
        buildManager.SelectTowerToBuild(laserBeamer);
    }

    public void SetBorder(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.transform.GetChild(i).gameObject;

            if (i == index)
                child.transform.GetChild(1).gameObject.SetActive(true);
            else
                child.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
