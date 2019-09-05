using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Demo of network spawning.
/// Despawning can be easily done using a syncvar flag on the object, or just have it destroy it's self on all clients on it's own.
/// @Author 256
/// </summary>

public class NetworkObjectSpawnExample : NetworkBehaviour
{

    public Transform munitionSpawnLocation = null;
    public GameObject baseGrenadePrefab = null;



    [ClientRpc] public void Rpc_throwGrenade(float cook)
    {
        //The following must be done in order.
        //first you instantiate the object on the server.
        GameObject gren = (GameObject)Instantiate(baseGrenadePrefab, munitionSpawnLocation.position, munitionSpawnLocation.rotation);
        //then you set it's parameters.
        //gren.GetComponent<Rigidbody>().velocity = grenade.transform.forward * (1.0f + cook) * 15.0f;
        //gren.GetComponent<Grenade>().lifeTime += cook;//we cooked the grenade a bit.
        //then, if we are on a server and it is active, call spawn, which transfers the data we just set on the prefab instance.
        if (NetworkServer.active)
            {
                NetworkServer.Spawn(gren);
            }
        //modification of the prefab at this point will cause it to not be copied to the other clients.
    
   }

    [Command]public void Cmd_throwGrenade(float cook){
            Rpc_throwGrenade(cook);
    }



}
