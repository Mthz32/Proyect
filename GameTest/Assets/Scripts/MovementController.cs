using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class MovementController : MonoBehaviour {

	public NavMeshAgent player;

	private int[] targeteable_layers = new int[] {12};
	private List<Targeteable> posible_targets = new List<Targeteable>();
	[SerializeField]
	private Targeteable target;
	[SerializeField]
	private bool targetOnRange = false;

	void Update () {
		//New destination?¿ --> right click
		if (Input.GetMouseButtonDown(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//hit anything?¿
			if (Physics.Raycast(ray, out hit)){
				if (targeteable_layers.Contains(hit.transform.gameObject.layer)){
					target = (Targeteable) hit.transform.gameObject.GetComponent(typeof(Targeteable));
					SetDestinationToTarget();
				}else{
					target = null;
					player.isStopped = false;
					player.SetDestination(hit.point);
				}
			}
		}

		//Puede ser que cause lag el tener que recalcular rutas?¿?¿?¿?¿?¿
		//Esta condicion es necesaria para mantener el objetivo si el target se mueve
		if (target != null && !targetOnRange){
			SetDestinationToTarget();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Targeteable t = (Targeteable) other.gameObject.GetComponent(typeof(Targeteable));
			posible_targets.Add(t);
			if (IsTheTarget(t)) {
				targetOnRange = true;
				player.isStopped = true;
				StartCoroutine(OnRange());
			}
		}
	}

	void OnTriggerExit(Collider	other){
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Targeteable t = (Targeteable) other.gameObject.GetComponent(typeof(Targeteable));
			posible_targets.Remove(t);
			if (IsTheTarget(t)){
				targetOnRange = false;
				SetDestinationToTarget();
			}
		}
	}

	private void SetDestinationToTarget(){
		player.isStopped = false;
		player.SetDestination(target.gameObject.transform.position);
	}

	private bool IsTheTarget(Targeteable t){
		return (GameObject.ReferenceEquals(t.gameObject, target.gameObject)) ? true : false;
	}

	private IEnumerator OnRange(){
		while ((target != null) && (targetOnRange)){
			yield return new WaitForSeconds (1f);

			//Para el caso de ataques a mele, tiene sentido suponer que debe seguir
			//en rango del jugador en el momento que termina la animacion el golpe
			//?¿?¿?
			if (targetOnRange)
			if (target.TakeDmg(100)){
				target = null;
			}
		}
	}
}
