using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Text sellCost;
    public Button upgradeButton;

    private Platform selectedPlatform;

    public void SetTarget(Platform _platform)
    {
        selectedPlatform = _platform;

        transform.position = selectedPlatform.GetBuildPosition();
        var lvl = selectedPlatform.installedTower.GetComponent<Tower>().level;

        sellCost.text = "$" + (int)(selectedPlatform.towerBluePrint.sellCost * lvl);

        if (lvl < 1.5)
        {
            upgradeCost.text = "$" + (int)(selectedPlatform.towerBluePrint.upgradeCost * lvl);
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        selectedPlatform.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        selectedPlatform.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
