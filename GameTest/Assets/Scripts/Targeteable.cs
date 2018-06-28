using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeteable : MonoBehaviour {

	private float hp = 1000;
	private float def = 0.4f;

	// Use this for initialization
	public bool TakeDmg(float dmg){
		hp -= ((1 - def) * dmg);
		if (hp <= 0){
			Destroy(this.gameObject);
			return true;
		}else{
			return false;
		}
	}

}
