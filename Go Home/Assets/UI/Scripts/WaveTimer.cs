using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private WaveNumber number;
    [SerializeField] private int[] waveTimes;
    internal static float waveTimer;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        number = GameObject.Find("WaveNumber").GetComponent<WaveNumber>();
        //Set the wave timer to the first time in the array
        waveTimer = waveTimes[0];
    }

    void Update()
    {
        //Decrease the wave timer by the difference in time it has been since the last frame
        waveTimer -= Time.deltaTime;
        //Display the text
        timerText.text = ((int)waveTimer).ToString();
        //If the timer is lower than 0 increase the wave
        if (waveTimer < 0)
        {
            StartCoroutine(IncreaseWave());
        }
    }
    //Increase the wave to the next wave, if this was the last wave, go to level select
    IEnumerator IncreaseWave()
    {
        if(number.waveNumber < waveTimes.Length)
        {
            number.IncreaseWave(waveTimes[number.waveNumber]);
        }
        else if(number.waveNumber == waveTimes.Length)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        yield return null;  
    }
    //Set the timer from anywhere
    internal static void SetWaveTimer(float timer)
    {
        waveTimer = timer;
    }
}
