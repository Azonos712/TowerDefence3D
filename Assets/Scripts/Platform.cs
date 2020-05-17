using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    //цвет выделения
    public Color emissionColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject installedTower;
    [HideInInspector]
    public TowerBlueprint towerBluePrint;

    private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);
    private Renderer r;
    private Color startColor;
    private BuildManager buildManager;
    private void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        //указатель на систему событий (что бы при нажатии на кнопку ничего не происходило на поле)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //Проверка попытки построить башню там где она уже есть
        if (installedTower != null)
        {
            buildManager.SelectPlatform(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        //Создаём башню на данной платформе
        GameObject tower = (GameObject)Instantiate(blueprint.prefab, this.GetBuildPosition(), Quaternion.identity);
        this.installedTower = tower;

        towerBluePrint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 4f);
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        PlayerStats.Money -= towerBluePrint.upgradeCost;

        installedTower.GetComponent<Tower>().level += 0.1f;

        GameObject effect = Instantiate(buildManager.buildEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBluePrint.sellCost;

        GameObject effect = Instantiate(buildManager.sellEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 4f);

        Destroy(installedTower);
        towerBluePrint = null;
    }

    private void OnMouseEnter()
    {
        //указатель на систему событий (что бы при нажатии на кнопку ничего не происходило на поле)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        //TODO: при наведении на платформу с башней
        if (buildManager.HasMoney)
            r.material.color = emissionColor;
        else
            r.material.color = notEnoughMoneyColor;
    }
    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
}
