using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
    private Platform target;

    public void SetTarget(Platform platform)
    {
        target = platform;

        transform.position = target.GetBuildPosition();
    }
}
