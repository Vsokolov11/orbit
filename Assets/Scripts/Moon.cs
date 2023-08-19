using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private Renderer renderer;
    private GameObject target;
    private float orbitDistance;
    private float orbitSpeed;
    private Vector3 initPos;
    private bool isSelected;

    void Start() {
        isSelected = false;
        renderer = GetComponent<Renderer>();
        target = transform.parent.gameObject;
        if (target == null) {
            Debug.Log("There is no planet to go around...");
            Die();
        }
    }

    public void InitializeMoon() {
        if(target != null) {
            initPos = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
        }
    }

    void Update() {
        if (target != null ) {
            transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
            transform.RotateAround(target.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        // if(isSelected) {
        //     renderer.material.color += Color.green;
        // } else {
        //     renderer.material.color -= Color.green;
        // }
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
        orbitDistance = newOrbitDistance;
        orbitSpeed = newOrbitSpeed;
    }

    public void Die() {
        Destroy(gameObject);
    }
}
