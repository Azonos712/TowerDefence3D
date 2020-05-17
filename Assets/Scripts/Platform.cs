using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    //цвет выделения
    public Color emissionColor;
    public Color notEnoughMoneyColor;
    [Header("Optional")]
    public GameObject installedTower;

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

        buildManager.BuildTowerOn(this);              
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
