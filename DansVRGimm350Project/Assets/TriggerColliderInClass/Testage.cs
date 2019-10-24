using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Random.onUnitSphere * 2.0f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("One rigid bodies, detects collision");
    }
}
