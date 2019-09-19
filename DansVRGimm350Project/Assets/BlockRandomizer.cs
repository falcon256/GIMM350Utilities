using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255));
        this.transform.localScale = Random.insideUnitSphere;
        this.transform.localPosition = new Vector3(0, Random.Range(1, 200), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
