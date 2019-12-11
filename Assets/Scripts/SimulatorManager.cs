using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimulatorManager : MonoBehaviour
{
    public static SimulatorManager instance;
    public GameObject cube;
    public GameObject particle;
    public float PlateVel = 0f;

    private GameObject[][][] layers;
    private double[] result;
    private int step = 0;
    private System.Timers.Timer timer = new System.Timers.Timer(200);
    private bool nextStep = false;
    private bool firstTick = true;
    // Start is called before the first frame update
    void Awake()
    {
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
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

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (!firstTick)
        {
            nextStep = true;
            firstTick = true;
            timer.Stop();
        }
        else
            firstTick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextStep)
        {
            result[result.Length - 1] = SinusoidalVel();
            result = Crank_Nicolson_Method.CrankNicolson(5000, 12.5, 1f / (layers.Length - 1), result);
            ChangeVelocities();
            nextStep = false;
            timer.Start();
        }
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

    private float SinusoidalVel()
    {
        float result = 0.05f * Mathf.Sin(2f * (float)Math.PI * 1/20f * step);
        PlateVel = result;
        step++;
        return result;

    }
    
}
