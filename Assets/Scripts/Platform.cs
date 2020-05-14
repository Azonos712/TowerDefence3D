﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    //цвет выделения
    public Color emissionColor;

    private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);
    private GameObject installedTower;
    private Renderer r;
    private Color startColor;
    BuildManager buildManager;
    private void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        //указатель на систему событий (что бы при нажатии на кнопку ничего не происходило на поле)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTowerToBuild() == null)
            return;

        //Проверка попытки построить башню там где она уже есть
        if (installedTower != null)
        {
            Debug.Log("TODO: Display on screen!");
            return;
        }

        //Получаем башню из класса управления строительством
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        //Создаём башню на данной платформе
        installedTower = Instantiate(towerToBuild, transform.position + positionOffset, transform.rotation);
    }
    private void OnMouseEnter()
    {
        //указатель на систему событий (что бы при нажатии на кнопку ничего не происходило на поле)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTowerToBuild() == null)
            return;

        r.material.color = emissionColor;
    }
    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
}
