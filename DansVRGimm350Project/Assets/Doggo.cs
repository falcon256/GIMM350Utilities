using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour
{
    public float lifeLived = 0;
    public float maxLifetime = 10.0f;
    public static float currentHighestScore = 0;
    public Network myNetwork = null;
    public Network oldNetwork = null;
    public int neuronWidth = 84;
    public float[] myInputs = null;// new float[neuronWidth];
    public GameObject doggoPrefab = null;
    public static int xoffset = -100;
    public static Network highScoreNetwork = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        lifeLived += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myInputs == null||myInputs.Length<neuronWidth)
            myInputs = new float[neuronWidth];
        if (myNetwork == null)
        {
            myNetwork = new Network();
            myNetwork.Init(3, neuronWidth);
        }

        ConfigurableJoint[] joints = this.GetComponentsInChildren<ConfigurableJoint>();
        Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody>();
        int index = 0;
        myInputs[index++] = transform.position.x;
        myInputs[index++] = transform.position.y;
        myInputs[index++] = transform.position.z;
        myInputs[index++] = transform.forward.x;
        myInputs[index++] = transform.forward.y;
        myInputs[index++] = transform.forward.z;
        for (int i = 0; i < rbs.Length; i++)
        {
            myInputs[index++] = rbs[i].transform.localEulerAngles.x;
            myInputs[index++] = rbs[i].transform.localEulerAngles.y;
            myInputs[index++] = rbs[i].transform.localEulerAngles.z;
            myInputs[index++] = rbs[i].transform.localPosition.x;
            myInputs[index++] = rbs[i].transform.localPosition.y;
            myInputs[index++] = rbs[i].transform.localPosition.z;          
        }

        float[] outputs = myNetwork.Tick(myInputs);
        index = 0;

        for(int i = 0; i < outputs.Length; i++)
        {
            if(float.IsInfinity(outputs[i])|| float.IsNaN(outputs[i]))
                outputs[i] = 0;
            
        }
        for(int i = 0; i < joints.Length; i++)
        {
            joints[i].targetAngularVelocity = new Vector3(outputs[index++], outputs[index++], outputs[index++]);
            
        }
        //Debug.Log(outputs[0]);
        //currentHighestScore *= 0.999f;
        //currentHighestScore -= 0.001f;


        /*
        //TODO also spawn new ones if we are the farthest one to whatever direction...
        if (lifeLived > maxLifetime)
        {
            //do the mutation
            float myscore = this.transform.position.z + this.transform.position.y;

            if (myscore > currentHighestScore)
            {
                currentHighestScore = myscore;
                lifeLived = 0;
                Network newNet = myNetwork.GetMutatedChild(0.01f, 0.01f, 0.1f);
                GameObject goob = Instantiate(doggoPrefab, new Vector3(xoffset++*5.0f, 10, 0), Quaternion.LookRotation(Vector3.forward, Vector3.up));
                Doggo newDoggo = goob.GetComponent<Doggo>();
                newDoggo.myNetwork = newNet;

                //newNet = myNetwork.GetMutatedChild(0.01f, 0.01f, 0.1f);
                //goob = Instantiate(doggoPrefab, new Vector3(xoffset++ * 5.0f, 10, 0), Quaternion.LookRotation(Vector3.forward, Vector3.up));
                //newDoggo = goob.GetComponent<Doggo>();
                //newDoggo.myNetwork = newNet;

                //newNet = myNetwork;
                //goob = Instantiate(doggoPrefab, new Vector3(xoffset++ * 5.0f, 10, 0), Quaternion.LookRotation(Vector3.forward, Vector3.up));
                //newDoggo = goob.GetComponent<Doggo>();
                //newDoggo.myNetwork = newNet;
                
                if (xoffset > 100)
                    xoffset = -100;
            }
            //Destroy(this.gameObject);
            lifeLived = 0;
        }
        */



        if(lifeLived > maxLifetime)
        {
            float myscore = this.transform.position.z;
            if (float.IsNaN(myscore) || float.IsInfinity(myscore))
                myscore = 0;
            lifeLived = 0;
            this.transform.position = new Vector3(this.transform.position.x, 5.0f, 0);

            if (myscore >= currentHighestScore)
            {
                currentHighestScore = myscore;
                Network newNet = myNetwork.GetMutatedChild(1.0f / (1.0f+myscore), 1.0f / (1.0f + myscore), 1.0f / (1.0f + myscore));
                highScoreNetwork = myNetwork;
                oldNetwork = myNetwork;
                myNetwork = newNet;
                Debug.Log("New Neural Network score: " + currentHighestScore);

            }
            else
            {
               
                if(highScoreNetwork!=null)
                {
                    myNetwork = highScoreNetwork.GetMutatedChild(1.0f / (1.0f + myscore), 1.0f / (1.0f + myscore), 1.0f / (1.0f + myscore));
                    currentHighestScore -= 0.001f;
                }
                else if(oldNetwork!=null)
                {
                    myNetwork = oldNetwork;
                }
            }


            //for (int i = 0; i < rbs.Length; i++)
            //{
            //    if (rbs[i].gameObject != this.gameObject)
            //    {
            //        rbs[i].gameObject.transform.localPosition = Vector3.zero;
            //        rbs[i].gameObject.transform.localRotation = Quaternion.identity;
            //    }
            //}




                maxLifetime = 1.0f + myscore;
            if (float.IsNaN(maxLifetime)||float.IsInfinity(maxLifetime))
                maxLifetime = 1.0f;
        }
    }
}
