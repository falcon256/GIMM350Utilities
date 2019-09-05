using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Removes a game object if this object isn't the local player.
/// Also makes sure it stays dead.
/// @Author 256
/// </summary>

public class ToastGameObjectIfNotLocal : NetworkBehaviour {

    public GameObject killMe = null;
    
    // Kill it on start if it isn't null
    void Start () {
		if (!isLocalPlayer)
        {
            if (killMe != null)
                Destroy(killMe);
        }
	}
	
	// if killMe isn't instantiated before Start is called, we might need to kill it later.
	void Update () {
        if (!isLocalPlayer)
        {
            if (killMe != null)
                Destroy(killMe);
        }
    }
}
