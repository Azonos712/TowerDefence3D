using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradButton;

    private Platform selectedPlatform;

    public void SetTarget(Platform _platform)
    {
        selectedPlatform = _platform;

        transform.position = selectedPlatform.GetBuildPosition();

        if (selectedPlatform.installedTower.GetComponent<Tower>().level < 1.5)
        {
            upgradeCost.text = "$" + selectedPlatform.towerBluePrint.upgradeCost.ToString();
            upgradButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradButton.interactable = false;
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
}
