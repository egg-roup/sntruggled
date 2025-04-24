using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BulletUIController : MonoBehaviour
{
    public TextMeshProUGUI bulletText;

    public void UpdateAmmo(int currentClip, int totalAmmo) {
        bulletText.text = $"{currentClip}/{totalAmmo}";
    }
}
