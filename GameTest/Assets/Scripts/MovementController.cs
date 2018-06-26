using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof())]
public class MovementController : MonoBehaviour {

	public NavMeshAgent player;

	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Plane aux_plane = new Plane(Vector3.up, this.transform.position);
	  	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist = 0f;
			if (aux_plane.Raycast(ray, out dist)){
				Vector3 target_point = ray.GetPoint(dist);
				player.SetDestination(target_point);
			}
		}
	}
}
