using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Prefab = null;
    void Start()
    {
        for(int i = 0; i < 30; i++)
        {
            GameObject goob = Instantiate(Prefab, new Vector3(i++ * 5.0f, 5, 0), Quaternion.LookRotation(Vector3.forward, Vector3.up));
        }
    }

}
