using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes one thing always look at another
/// @Author 256
/// </summary>

public class LookAtTarget : MonoBehaviour {
    public GameObject target = null;

	void Update () {
        this.transform.LookAt(target.transform.position);
	}
}
