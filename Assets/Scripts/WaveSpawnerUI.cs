using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawnerUI : MonoBehaviour
{
    public Spawner spawner;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    private void Update()
    {
        if (spawner == null) return;

        waveText.text = $"{spawner.CurrentWave}";

        float timeLeft = spawner.TimeUntilNextWave;
        if (timeLeft > 0)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timerText.text = $"{minutes:00}:{seconds:00} until next wave";
        }
        else
        {
            timerText.text = "they're coming.";
        }
    }
}
