using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimulatorManager : MonoBehaviour
{
    public static SimulatorManager instance;
    public GameObject cube;
    public GameObject particle;

    private GameObject[][][] layers;
    private double[] result;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        string[] var = Environment.GetCommandLineArgs();
        int x = Convert.ToInt32(Mathf.Floor(cube.transform.localScale.x / particle.transform.localScale.x));
        int y = Convert.ToInt32(Mathf.Floor(1f / particle.transform.localScale.y))+1;
        int z = Convert.ToInt32(Mathf.Floor(cube.transform.localScale.z / particle.transform.localScale.z));
        layers = new GameObject[y][][];

        for (int j = 0; j < y; j++)
        {
            GameObject[][] layer = new GameObject[x][];
            for (int i = 0; i < x; i++)
            {
                GameObject[] row = new GameObject[z];
                for (int k = 0; k < z; k++)
                {
                    row[k]=Instantiate(particle, new Vector3(particle.transform.localScale.x * i, particle.transform.localScale.y * j, particle.transform.localScale.z * k), Quaternion.identity);
                }
                layer[i] = row;
            }
            layers[j] = layer;
        }
        InitialCond();

    }

    // Update is called once per frame
    void Update()
    {
        result=Crank_Nicolson_Method.CrankNicolson(5000,12.5,1f/(layers.Length-1),result);
        ChangeVelocities();


    }

    public GameObject[][][] getLayers()
    {
        return layers;
    }
    private void InitialCond()
    {
        result = new double[layers.Length];
        for (int i = 0; i < layers.Length; i++)
            result[i] = layers[i][0][0].GetComponent<Particle>().Velocity;

    }
    private void ChangeVelocities()
    {
        for (int j = 0; j < layers.Length; j++)
        {
            for (int i = 0; i < layers[j].Length; i++)
            {
                for (int k = 0; k < layers[j][i].Length; k++)
                {
                    layers[j][i][k].GetComponent<Particle>().Velocity = (float)result[j];
                }
            }
        }
    }
    
}
