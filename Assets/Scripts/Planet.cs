using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    GameObject target;
    public float orbitDistance;
    public float orbitSpeed;
    public Vector3 initPos;

    void Start() {

        target  = GameObject.FindWithTag("Star");
        if(target != null) {
            InitializePlanet();
        }
        else {
            Debug.Log("No star in this solar system...");
            Destroy(gameObject);
        }
    }

    void InitializePlanet() {
        initPos = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
    }

    void Update() {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
        transform.RotateAround(target.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

}
