using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// traprollの動作
/// </summary>
public class traproll : MonoBehaviour
{
    private Vector3 roll;
    public float rollspeed_x = 0.0f;
    public float rollspeed_y = 0.0f;
    public float rollspeed_z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        roll.x = rollspeed_x;
        roll.y = rollspeed_y;
        roll.z = rollspeed_z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(roll);
    }
}
