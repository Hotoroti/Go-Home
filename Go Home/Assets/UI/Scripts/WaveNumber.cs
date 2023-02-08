using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour
{
    internal int waveNumber;
    private string waveText;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        waveNumber = 1;
        waveText = "Wave: ";
        //Set the text to the first number
        text.text = waveText + waveNumber;
    }
    //Increase the wavenumber and set the timer
    internal void IncreaseWave(float waveTime)
    {
        waveNumber += 1;
        text.text = waveText + waveNumber;
        WaveTimer.SetWaveTimer(waveTime);
    }
}
