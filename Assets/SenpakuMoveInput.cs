using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenpakuMoveInput : MonoBehaviour
{
    [SerializeField]
    float speed = 0.002f;

    Vector3 moveDir
    {
        get
        {
            return this.transform.forward;
        }
    }
    [SerializeField]
    Vector3 accel = new Vector3();

    [SerializeField]
    float rotSpeed = 0.2f;
    float rotXSpeed = 0.0f;

    public Vector3 friction_waves;
    public Vector3 friction_air;

    [SerializeField]
    Vector3 friction_total;
    [SerializeField]
    Vector3 accelv2 = new Vector3();
    void Start()
    {
    }
    void Update()
    {

        Vector3 vec = new Vector3();
        if (GateInput.Instance.IsUpdatePadInputUp())
        {
            vec += moveDir * speed;
        }
        if (GateInput.Instance.IsUpdatePadInputDown())
        {
            vec += -moveDir * speed;
        }

        if (GateInput.Instance.IsUpdatePadInputLeft())
        {
            rotXSpeed -= rotSpeed;
        }
        else if (GateInput.Instance.IsUpdatePadInputRight())
        {
            rotXSpeed += rotSpeed;
        }else
        {
            //rotXSpeed = 0.0f;
        }
        accel += vec;
        if (accel.magnitude > 0.1f) accel -= vec;

        accelv2 = accel * 0.2f;
        //accelv2.x = accel.x * accel.x *0.1f;
        //accelv2.y = accel.y * accel.y * 0.1f;
        //accelv2.z = accel.z * accel.z * 0.1f;
        //accelv2 = accelv2 * 0.2f;
        //Debug.Log("accelv2:" + accelv2.ToString());

        friction_waves = accelv2 * 0.5f * 0.997f * 0.001f;
        friction_air = accelv2 * 0.5f * 1.293f * 0.6f;
        friction_total = friction_waves + friction_air;
        //if (friction_waves.magnitude > -0.01f) friction_waves = Vector3.zero;
        //if (friction_air.magnitude > -0.01f) friction_air = Vector3.zero;
        //if (friction_total.magnitude > -0.01f) friction_total = Vector3.zero;


        accel -= friction_total;

        Debug.Log("wave:"+ friction_waves.ToString() + " air:" + friction_air.ToString() + " total:" + friction_total.ToString());

        this.gameObject.transform.localRotation = Quaternion.Euler(.0f, rotXSpeed, .0f);
        this.gameObject.transform.localPosition += accel;
    }
}