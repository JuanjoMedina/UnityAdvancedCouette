using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public float Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Velocity = SimulatorManager.instance.PlateVel;
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = SimulatorManager.instance.PlateVel;
        this.transform.position += new Vector3(Velocity, 0, 0);

        if (this.transform.position.x - this.transform.localScale.x / 2 > 10.2)
            this.transform.position = new Vector3(-this.transform.localScale.x / 2 - 0.2f, this.transform.position.y, this.transform.position.z);
        else if (this.transform.position.x + this.transform.localScale.x / 2 < -0.2)
            this.transform.position = new Vector3(9.8f + this.transform.localScale.x / 2, this.transform.position.y, this.transform.position.z);
    }
}
