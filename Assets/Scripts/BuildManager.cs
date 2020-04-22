using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //Singleton
    public static BuildManager instance;

    private void Awake()
    {
        //Awake - вызывается до начала любых функций, а также сразу после инициализации префаба.
        //создаётся только одна сущность данного класса
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standartTowerPrefab;

    private void Start()
    {
        towerToBuild = standartTowerPrefab;
    }

    private GameObject towerToBuild;

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }
}
