using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour
{
    public float lifeLived = 0;
    public float maxLifetime = 10.0f;
    public static float currentHighestScore = 0;
    public Network myNetwork = null;
    public int neuronWidth = 100;
    public float[] myInputs = null;// new float[neuronWidth];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myInputs == null)
            myInputs = new float[neuronWidth];
        if (myNetwork == null)
        {
            myNetwork = new Network();
            myNetwork.Init(5, 100);
        }

        ConfigurableJoint[] joints = this.GetComponentsInChildren<ConfigurableJoint>();
        Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody>();
        int index = 0;
        for(int i = 0; i < rbs.Length; i++)
        {
            myInputs[index++] = rbs[i].transform.localEulerAngles.x;
            myInputs[index++] = rbs[i].transform.localEulerAngles.y;
            myInputs[index++] = rbs[i].transform.localEulerAngles.z;
            myInputs[index++] = rbs[i].transform.localPosition.x;
            myInputs[index++] = rbs[i].transform.localPosition.y;
            myInputs[index++] = rbs[i].transform.localPosition.z;
            //     myInputs[index++] = joints[i].
        }



        currentHighestScore *= 0.999f;
        //TODO also spawn new ones if we are the farthest one to whatever direction...
        if (lifeLived > maxLifetime)
            Destroy(this.gameObject);
    }
}
