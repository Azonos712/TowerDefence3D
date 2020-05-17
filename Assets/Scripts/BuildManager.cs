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

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
