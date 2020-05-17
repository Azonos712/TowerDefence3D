using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //создаётся только одна сущность данного класса-Singleton
    public static BuildManager instance;

    public GameObject buildEffect;
    public SelectUI platformUI;

    private TowerBlueprint towerToBuild;
    private Platform selectedPlatfrom;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    private void Awake()
    {
        //Awake - вызывается до начала любых функций, а также сразу после инициализации префаба.
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public void BuildTowerOn(Platform platform)
    {
        if (PlayerStats.Money < towerToBuild.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= towerToBuild.cost;

        //Создаём башню на данной платформе
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, platform.GetBuildPosition(), Quaternion.identity);
        platform.installedTower = tower;

        GameObject effect = Instantiate(buildEffect, platform.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 4f);
    }

    public void SelectPlatform(Platform platform)
    {
        if(selectedPlatfrom == platform)
        {
            DeselectNode();
            return;
        }

        selectedPlatfrom = platform;
        towerToBuild = null;

        platformUI.SetTarget(platform);
    }


    public void DeselectNode()
    {
        selectedPlatfrom = null;
        platformUI.Hide();
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;

        DeselectNode();
    }
}
