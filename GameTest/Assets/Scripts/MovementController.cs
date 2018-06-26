using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof())]
public class MovementController : MonoBehaviour {

	public NavMeshAgent player;

	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				player.SetDestination(hit.point);
			}
		}
	}
}
