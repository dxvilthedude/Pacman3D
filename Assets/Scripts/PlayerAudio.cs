using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] collectSounds;
    public AudioClip FootstepSound;
    public AudioSource audioS;

    public void Footstep()
    { audioS.Play(); }
    public void CollectBoostSFX()
    { AudioSource.PlayClipAtPoint(collectSounds[1],transform.position, 1f); }

}
