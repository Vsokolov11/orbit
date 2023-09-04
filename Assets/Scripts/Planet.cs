using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Renderer renderer;
    private GameObject target;
    private float orbitDistance;
    private float orbitSpeed;
    private Vector3 initPos;
    private bool isSelected;

    private GameObject selectionPointer;

    void Start() {
        isSelected = false;
        renderer = GetComponent<Renderer>();
        target  = GameObject.FindWithTag("Star");
        if(target != null) {
            InitializePlanet();
        }
        else {
            Debug.Log("No star in this solar system...");
            Destroy(gameObject);
        }
        selectionPointer = this.gameObject.transform.GetChild(0).gameObject;
        selectionPointer.SetActive(false);
    }

    void InitializePlanet() {
        initPos = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
    }

    void Update() {
        transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
        transform.RotateAround(target.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);

        if(isSelected) {
            renderer.material.color = Color.green;
            selectionPointer.SetActive(true);
        }
        else {
            renderer.material.color = Color.blue;
            selectionPointer.SetActive(false);
        }
    }

    private void OnMouseEnter() {
        renderer.material.color += Color.red;
    }

    private void OnMouseExit() {
        renderer.material.color -= Color.red;
    }

    public void Select(bool state) {
        isSelected = state;
    }

    public void ChangeValues(float newOrbitDistance, float newOrbitSpeed) {
        //Debug.Log("new orbit distance" + newOrbitDistance);
        orbitDistance = newOrbitDistance;
        orbitSpeed = newOrbitSpeed;
    }

    public void CrashIntoStar() {
        Debug.Log("I was too close to the sun...");
        Destroy(gameObject);
    }

    public float ReturnSpeed() {
        return orbitSpeed;
    }

    public float ReturnDistance() {
        return orbitDistance;
    }

}
