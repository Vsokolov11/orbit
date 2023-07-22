using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    GameObject target;
    float rotSpeed;
    float orbitDistance;
    Vector3 initPos;

    void Start() {

        target  = GameObject.FindWithTag("Star");
        if(target != null) {
            CreatePlanet();
        }
        else {
            Debug.Log("No star in this solar system...");
            Destroy(gameObject);
        }
    }

    void CreatePlanet() {
        orbitDistance = 5;
        rotSpeed = 5;
        initPos = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
    }

    void Update() {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
        transform.RotateAround(target.transform.position, Vector3.up, rotSpeed * Time.deltaTime);
    }

}
