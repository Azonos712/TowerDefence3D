using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serializable нужно для того чтобы Unity отображал эти данные в свойствах
[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;
}
