using System.Collections;
using UnityEngine;

public class DelayedEnable : MonoBehaviour
{
    public MonoBehaviour targetScript;
    public float delaySeconds = 0.1f;

    void Start()
    {
        if (targetScript != null)
        {
            targetScript.enabled = false;
            StartCoroutine(EnableLater());
        }
    }

    IEnumerator EnableLater()
    {
        yield return new WaitForSeconds(delaySeconds);
        targetScript.enabled = true;
    }
}

