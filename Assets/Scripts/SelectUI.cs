using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class SelectUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Text sellCost;
    public Button upgradeButton;
    public Animator anim;

    private Platform selectedPlatform;
    Color oldColor;
    

    int segments = 50;
    float xradius = 1;
    float yradius = 1;
    public LineRenderer line;

    private float lvl;
    
    void Start()
    {
        oldColor = upgradeButton.image.color;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
    }
    private void Update()
    {
        if (line.enabled)
            CreateRangeCircle();
    }
    void CreateRangeCircle()
    {
        float x;
        float y;
        float z = 0;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));
            
            angle += (360f / segments);
        }
    }

    public void SetTarget(Platform _platform)
    {
        selectedPlatform = _platform;

        lvl = selectedPlatform.installedTower.GetComponent<Tower>().level;

        DrawRange();

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

        SetShowStatus(true);
    }

    void DrawRange()
    {
        transform.position = selectedPlatform.GetBuildPosition() + Vector3.up;
        var range = selectedPlatform.installedTower.GetComponent<Tower>().range;
        xradius = yradius = range;
    }

    public void SetShowStatus(bool status)
    {
        upgradeButton.image.color = oldColor;
        line.enabled = status;
        ui.SetActive(status);
    }

    public void Upgrade()
    {
        if (PlayerStats.Money < (selectedPlatform.towerBluePrint.upgradeCost * lvl)){
            anim.Play("NoUpgrade");
            return;
        }

        selectedPlatform.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        selectedPlatform.SellTower();
        BuildManager.instance.DeselectNode();
    }
}