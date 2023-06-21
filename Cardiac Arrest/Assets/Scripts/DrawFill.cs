using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFill : MonoBehaviour
{
	[SerializeField] private GameObject container;
    private Transform myTransform;
	private Vector3 scaleChange = new Vector3(2, 2, 2);
	private Vector3 posChange = new Vector3(0, 0, 0);
	
    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }
	
	private void FixedUpdate()
	{
		posChange.y = ((container.GetComponent<BloodTrigger>().fill / 10000.0f) - 1);
		scaleChange.y = (container.GetComponent<BloodTrigger>().fill / 5000.0f);
		myTransform.localScale = scaleChange;
		myTransform.localPosition = posChange;
	}
}