using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementManager : MonoBehaviour {

	private Rigidbody rb;

	private Vector3 vel = Vector3.zero;
	private float max_vel = 5f;
	private float min_vel = 1f;

	private Vector3 target;
	private float rotation_speed = 2f;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		//Calculo del movimiento
		Vector3 incr = ((vel.magnitude < min_vel)
			? Vector3.zero
			: Vector3.ClampMagnitude(vel, max_vel)
			) * Time.fixedDeltaTime;
		//Aplicacion y reseteo
		rb.MovePosition(rb.position + incr);
		vel = Vector3.zero;

		//Calculo de la rotacion
		Quaternion rotation_incr = Quaternion.LookRotation(
			target - this.transform.position);
		//Apply
		this.transform.rotation = Quaternion.Slerp(
			this.transform.rotation, rotation_incr,
			rotation_speed * Time.fixedDeltaTime);
		//reseteo
		target = this.transform.position;
	}

	public void updateVelocity(Vector3 _vel){
		vel = _vel;
	}

	public void updateStaringTarget(Vector3 _target){
		target = _target;
	}
}
