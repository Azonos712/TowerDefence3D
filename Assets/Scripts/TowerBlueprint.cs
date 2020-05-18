using UnityEngine;

//Serializable нужно для того чтобы Unity отображал эти данные в свойствах
[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;
    public int upgradeCost;
    public int sellCost;
}
