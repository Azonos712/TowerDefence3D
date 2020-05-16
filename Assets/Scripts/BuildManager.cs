using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //создаётся только одна сущность данного класса-Singleton
    public static BuildManager instance;

    public GameObject standartTowerPrefab;
    public GameObject missileLauncherPrefab;

    private GameObject towerToBuild;

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

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }
}
