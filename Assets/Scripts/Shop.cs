﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint standartTower;
    public TowerBlueprint missileLauncher;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        buildManager.SelectTowerToBuild(standartTower);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTowerToBuild(missileLauncher);
    }
}
