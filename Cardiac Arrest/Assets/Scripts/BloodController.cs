using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    private int beatFUCounter;
    public int heartRate;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] ParticleSystem blood;
    private void FixedUpdate()
    {
        beatFUCounter -= 1;
        if (beatFUCounter <= 0)
        {
            blood.Play();
            beatFUCounter = (3000 / heartRate);
        }
    }
}
