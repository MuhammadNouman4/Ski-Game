using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObstacle : Obstacle
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
