﻿using UnityEngine;

public class Platform : MonoBehaviour
{
    //цвет выделения
    public Color emissionColor;

    private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);
    private GameObject installedTower;
    private Renderer r;
    private Color startColor;
    private void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
    }
    private void OnMouseDown()
    {
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
        r.material.color = emissionColor;
    }
    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
}
