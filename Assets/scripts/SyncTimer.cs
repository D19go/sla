using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class SyncTimer : MonoBehaviour
{
    public int min, seg;
    public TextMeshProUGUI timer;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Timer(int minutos, int segundos)
    {
        min = minutos;
        seg = segundos;
        StartCoroutine(TimerEND());
    }

    IEnumerator TimerEND()
    {
        timer.text = $"{min:D2}:{seg:D2}";
        seg--;
        yield return new WaitForSeconds(1f);

        if (seg <= 0 && min <= 0)
        {
            timer.text = $"ACABOU O TEMPO";
        }
        else if (seg <= 0)
        {
            min--;
            seg = 59;
        }   
        StartCoroutine(TimerEND());
    }
}
