using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //создаётся только одна сущность данного класса-Singleton
    public static BuildManager instance;

    private TowerBlueprint towerToBuild;

    public bool CanBuild { get { return towerToBuild != null; } }

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
        if(PlayerStats.Money < towerToBuild.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= towerToBuild.cost;

        //Создаём башню на данной платформе
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, platform.GetBuildPosition(), Quaternion.identity);
        platform.installedTower = tower;
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }
}
