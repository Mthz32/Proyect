using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

// [RequireComponent(typeof(...))]
public class MovementController : MonoBehaviour {

	public NavMeshAgent player;

	private int[] targeteable_layers = new int[] {12};
	private Targeteable target;

	void Update () {
		//Set destination where the player clicks (right click)
		if (Input.GetMouseButtonDown(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (targeteable_layers.Contains(hit.transform.gameObject.layer)){
					target = (Targeteable) hit.transform.gameObject.GetComponent(typeof(Targeteable));

					//MAY NEED TO RESET IF THE TARGET CAN MOVE
					SetDestinationToTarget();
				}else{
					target = null;
					player.SetDestination(hit.point);
				}
			}
		}
	}

	void SetDestinationToTarget(){
		player.SetDestination(target.gameObject.transform.position);
	}
}
