using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float Velocity;
    // Start is called before the first frame update
    void Awake()
    {
        Velocity = SimulatorManager.instance.PlateVel;
        int length = SimulatorManager.instance.getLayers().Length;
        if (System.Convert.ToInt32((length - 1) * this.transform.position.y) != length - 1)
            Velocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int length = SimulatorManager.instance.getLayers().Length;
        if (System.Convert.ToInt32((length - 1) * this.transform.position.y) == length - 1)
            Velocity = SimulatorManager.instance.PlateVel;
        this.transform.position = new Vector3(transform.position.x + Velocity, transform.position.y, transform.position.z);
        OutOfSim();
    }

    void OutOfSim()
    {
        if (this.transform.position.x > 10)

            this.transform.position= new Vector3(0 + this.transform.position.x - 10, this.transform.position.y, this.transform.position.z);

        else if (this.transform.position.x < 0)
            this.transform.position = new Vector3(10 + this.transform.position.x, this.transform.position.y, this.transform.position.z);


    }
    void setVelocity(float Vel)
    {
        this.Velocity = Vel;
    }
}
