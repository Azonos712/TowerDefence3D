using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    //цвет выделения
    public Color emissionColor;
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

        if (!buildManager.CanBuild)
            return;

        //Проверка попытки построить башню там где она уже есть
        if (installedTower != null)
        {
            Debug.Log("TODO: Display on screen!");
            return;
        }

        buildManager.BuildTowerOn(this);              
    }
    private void OnMouseEnter()
    {
        //указатель на систему событий (что бы при нажатии на кнопку ничего не происходило на поле)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        r.material.color = emissionColor;
    }
    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
}
