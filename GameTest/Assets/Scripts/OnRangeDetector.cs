using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnRangeDetector : MonoBehaviour {

	private int[] targeteable_layers = new int[] {12};

	private List<Targeteable> posible_targets = new List<Targeteable>();

	void OnTriggerEnter(Collider other) {
		if (targeteable_layers.Contains(other.gameObject.layer)){
			posible_targets.Add((Targeteable) other.gameObject.GetComponent(typeof(Targeteable)));
		}
  }

	void OnTriggerExit(Collider	other){
		if (targeteable_layers.Contains(other.gameObject.layer)){
			posible_targets.Remove((Targeteable) other.gameObject.GetComponent(typeof(Targeteable)));
		}
	}

}
