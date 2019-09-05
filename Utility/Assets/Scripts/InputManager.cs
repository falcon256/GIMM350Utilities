using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputField = null;
    public Text outputField = null;
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
        outputField.text = Futilities.CountfNumOutputString(derp);
    }
}
