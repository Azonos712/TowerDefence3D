using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //создаётся только одна сущность данного класса-Singleton
    public static BuildManager instance;

    public GameObject buildEffect;
    public GameObject sellEffect;
    public SelectUI platformUI;

    private TowerBlueprint towerToBuild;
    public Platform selectedPlatform;

    public Component leftShop;
    public TowerBlueprint standartTower;
    public TowerBlueprint missileLauncher;
    public TowerBlueprint laserBeamer;

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
        if(selectedPlatform == platform)
        {
            DeselectNode();
            return;
        }

        selectedPlatform = platform;
        towerToBuild = null;

        SetBorder(-1);
        platformUI.SetTarget(platform);
    }

    public void DeselectNode()
    {
        selectedPlatform = null;
        platformUI.SetShowStatus(false);
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

    public void SelectStandartTurret()
    {
        SetBorder(0);
        SelectTowerToBuild(standartTower);
    }

    public void SelectMissileLauncher()
    {
        SetBorder(1);
        SelectTowerToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        SetBorder(2);
        SelectTowerToBuild(laserBeamer);
    }

    public void SetBorder(int index)
    {
        for (int i = 0; i < leftShop.transform.childCount; i++)
        {
            var child = leftShop.transform.transform.GetChild(i).gameObject;

            if (i == index)
                child.transform.GetChild(1).gameObject.SetActive(true);
            else
                child.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
