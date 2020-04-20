using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //Singleton
    public static BuildManager instance;

    private void Awake()
    {
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
