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

        sellCost.text = "$" + selectedPlatform.towerBluePrint.sellCost.ToString();

        if (selectedPlatform.installedTower.GetComponent<Tower>().level < 1.5)
        {
            upgradeCost.text = "$" + selectedPlatform.towerBluePrint.upgradeCost.ToString();
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
