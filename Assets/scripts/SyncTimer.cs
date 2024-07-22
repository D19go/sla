using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class SyncTimer : NetworkBehaviour
{
    public readonly SyncVar<int> min = new();
    public readonly SyncVar<int> seg = new();
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    public void Timer()
    {
        StartCoroutine(TimerEND());
        IEnumerator TimerEND()
        {
            GetComponent<GameManager>().AtualizaTimer(min.Value, seg.Value, false);
            seg.Value--;
            yield return new WaitForSeconds(1f);

            if (seg.Value <= 0 && min.Value <= 0)
            {
                GetComponent<GameManager>().AtualizaTimer(min.Value, seg.Value, true);
            }
            else if (seg.Value <= 0)
            {
                min.Value--;
                seg.Value = 59;
            }
            StartCoroutine(TimerEND());
        }
    }

    
}
