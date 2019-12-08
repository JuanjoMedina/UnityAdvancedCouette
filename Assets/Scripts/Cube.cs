using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public float Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Velocity = SimulatorManager.instance.MaxVel;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Velocity, 0, 0);

        if (this.transform.position.x - this.transform.localScale.x / 2 > 9.8)
            this.transform.position = new Vector3(-this.transform.localScale.x / 2, this.transform.position.y, this.transform.position.z);
    }
}
