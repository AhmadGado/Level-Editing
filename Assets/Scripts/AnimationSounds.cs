using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] AudioSource footSound;
    public void EnableCharacterSoundS()
    {
        footSound.Play();
    }
    public void DisableCharacterSoundS()
    {
        footSound.Stop();
    }
}
