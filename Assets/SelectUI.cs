using UnityEngine;

public class SelectUI : MonoBehaviour
{
    public GameObject ui;

    private Platform target;

    public void SetTarget(Platform platform)
    {
        target = platform;

        transform.position = target.GetBuildPosition();
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
