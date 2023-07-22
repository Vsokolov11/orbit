using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject target;
    public float rotSpeed;
    public float orbitDistance;

    void Start() {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
    }

    void Update() {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
        transform.RotateAround(target.transform.position, Vector3.up, rotSpeed * Time.deltaTime);
    }

}
