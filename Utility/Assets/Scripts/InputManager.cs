using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputField = null;
    public Text outputField = null;
    public GameObject LetterSpherePrefab = null;
    public List<GameObject> oldSpheres = null;
    void Start()
    {
        inputField.text = "";
        outputField.text = "";
    }


    public void doUpdate()
    {
        string derp = inputField.text;
        //outputField.text = Futilities.reverse(derp);
        //outputField.text = Futilities.CheckIfNumOutputString(derp);
        outputField.text = ""+Futilities.getAverageTokenLength(derp);
        makeSomeSpheres(LetterSpherePrefab, inputField.text);
    }


    //shove this somewhere else once it works.
    public void makeSomeSpheres(GameObject prefab,string input)
    {
        if(oldSpheres!=null)
        {
            foreach(GameObject g in oldSpheres)
            {
                Destroy(g);
            }
            oldSpheres.Clear();
        }
        else
        {
            oldSpheres = new List<GameObject>();
        }




        Dictionary<char, int> charDict = new Dictionary<char, int>() ;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (charDict.ContainsKey(c))
            {
                int val = 0;
                charDict.TryGetValue(c, out val);
                charDict[c] = val + 1;
            }
            else
            {
                charDict.Add(c, 1);
            }
        }
        int pos = 0;
        for (int i = input.Length; i > 0; i--)
        { 
            foreach( KeyValuePair<char,int> c in charDict)
            {
                if (c.Value == i)
                {
                    GameObject newGO = Instantiate(prefab);
                    newGO.transform.position = new Vector3(1.1f * pos++, 0, 0);
                    newGO.GetComponentInChildren<TextMesh>().text = "" + c.Key+"\n"+c.Value;
                    Debug.Log("" + c.Key + "\n" + c.Value);
                    oldSpheres.Add(newGO);
                }
            }
           

        }
    }


}
