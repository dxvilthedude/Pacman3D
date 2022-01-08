using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource audioS;
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        GameEvents.current.onCoinCollect += CoinCollectSFX;
    }

    private void CoinCollectSFX()
    {
        audioS.Play();
    }
}
