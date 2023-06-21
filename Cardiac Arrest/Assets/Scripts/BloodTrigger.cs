using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodTrigger : MonoBehaviour
{	
	[SerializeField] public int fill = 0;
    [SerializeField] private Collider2D player;
	private bool trigger = false;
	
	[SerializeField] private UnityEvent flag;
	[SerializeField] private int fillRate = 100;
	
	private void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
	}
	
	private void OnTriggerStay2D (Collider2D enter)
    {
		if (enter == player && fill < 10000)
        {
			fill += fillRate;
			if (fill > 10000) { fill = 10000; }
		}
    }
	
	private void FixedUpdate ()
	{
		if (fill == 100 && trigger == false)
		{
			flag.Invoke();
			trigger = true;
		}
	}
}