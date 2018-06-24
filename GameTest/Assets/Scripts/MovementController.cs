using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementManager))]
public class MovementController : MonoBehaviour {

	private float speed = 3f;
	private MovementManager mm;

	void Start () {
		mm = GetComponent<MovementManager>();
	}

	void Update () {
		//Movimiento basado en el input
		float input_x = Input.GetAxis("Horizontal");
		float input_z = Input.GetAxis("Vertical");
		Vector3 x_mov = this.transform.right * input_x;
		Vector3 z_mov = this.transform.forward * input_z;
		Vector3 output = (x_mov + z_mov).normalized * speed;
		mm.updateVelocity(output);

		//Rotacion basada en la posicion del raton
		Plane aux_plane = new Plane(Vector3.up, this.transform.position);
  	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist = 0f;
		if (aux_plane.Raycast(ray, out dist)){
			Vector3 target_point = ray.GetPoint(dist);
			mm.updateStaringTarget(target_point);
		}
	}
}
