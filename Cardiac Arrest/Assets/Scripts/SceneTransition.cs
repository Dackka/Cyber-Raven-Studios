using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneTransition : MonoBehaviour
{
	
	//Variable to determine scene to go to.
	[Header("Private Vars")]
	[SerializeField] private string target;
    [SerializeField] private Collider2D player;
	[SerializeField] private bool active = true;
	
	private void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
	}
	
	private void OnTriggerStay2D (Collider2D enter)
    {
		if (enter == player && active)
        {
			SceneManager.LoadScene(target);
		}
    }
}