using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network 
{
    public float[,,] weights;
    public float[,,] biases;
    public float[,] values;

    public int layerCount = 0;
    public int layerWidth = 0;

    public void Init(int layerCount, int layerWidth)
    {

        this.layerCount = layerCount;
        this.layerWidth = layerWidth;

        weights = new float[layerCount, layerWidth, layerWidth];
        biases = new float[layerCount, layerWidth, layerWidth];
        values = new float[layerCount, layerWidth];
    }

    public float[] Tick(float[] inputs)
    {
        float[] outputs = new float[layerWidth];

        for (int y = 0; y < layerCount; y++)
            for (int x = 0; x < layerWidth; x++)
                values[y, x] = 0;

        for (int i = 0; i < layerWidth; i++)
            values[0, i] = inputs[i];

        for(int layer = 0; layer < layerCount-1; layer++)//don't calc the last layer.
        {
            for(int neuron = 0; neuron < layerWidth; neuron++)
            {
                for (int neuronOut = 0; neuronOut < layerWidth; neuronOut++)
                {
                    //the web says tanh(x) = 1-2/(1+exp(2*x)) = (exp(2*x)-1)/(exp(2*x)+1)
                    //hyperbolic tangent activation function
                    values[layer+1,neuronOut]+=biases[layer,neuron,neuronOut]+(weights[layer,neuron,neuronOut]*((Mathf.Exp(2.0f * values[layer,neuron]) - 1) / (Mathf.Exp(2.0f * values[layer, neuron]) + 1)));
                }
            }
        }

        for (int i = 0; i < layerWidth; i++)
            outputs[i] = values[layerCount - 1, i];
        return outputs;
    }

    public Network GetMutatedChild(float biasMod, float weightMod, float modChance)
    {

        Network newNet = new Network();
        newNet.Init(layerCount, layerWidth);

        for (int y = 0; y < layerCount; y++)
            for (int x = 0; x < layerWidth; x++)
                for (int z = 0; z < layerWidth; z++)
                {
                    if(Random.value<modChance)
                        newNet.weights[y, z, x] += Random.Range(-weightMod, weightMod);
                    if (Random.value < modChance)
                        newNet.biases[y, z, x] += Random.Range(-biasMod, biasMod);
                }


        return newNet;
    }


}
