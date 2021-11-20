using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    static AudioSource SFX;
    public AudioClip shot, hurt, explosion, powerup;

    private void Start()
    {
        SFX = GetComponent<AudioSource>();
    }

    public void shotSFX()
    {
        SFX.clip = shot;
        SFX.Play();
    }

    public void hurtSFX()
    {
        SFX.clip = hurt;
        SFX.Play();
    }

    public void powerSFX()
    {
        SFX.clip = powerup;
        SFX.Play();
    }

    public void explosionSFX()
    {
        SFX.clip = explosion;
        SFX.Play();
    }
}
