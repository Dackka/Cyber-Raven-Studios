using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touching : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("Touch");
    }
}
