using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundeffectPlay : MonoBehaviour
{
    public AudioSource aSource;
    public List<AudioClip> clipList;
    public void PlaySoundEffect(int index)
    {
        aSource.PlayOneShot(clipList[index]);
    }
    public void PlaySoundEffectRandom(int indexMin, int indexMax)
    {
        aSource.PlayOneShot(clipList[Random.Range(indexMin,indexMax+1)]);
    }
}
