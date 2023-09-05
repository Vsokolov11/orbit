using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private GameObject target;
    private float orbitDistance;
    private float orbitSpeed;
    private Vector3 initPos;
    private bool isSelected;
    private GameObject selectionPointer;

    void Start() {
        isSelected = false;
        target = transform.parent.gameObject;
        if (target == null) {
            Debug.Log("There is no planet to go around...");
            Die();
        }
        selectionPointer = this.gameObject.transform.GetChild(0).gameObject;
        selectionPointer.SetActive(false);
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
        if(isSelected) {
            selectionPointer.SetActive(true);
        } else {
            selectionPointer.SetActive(false);
        }
    }

    // private void OnMouseEnter() {
    //     renderer.material.color += Color.red;
    // }

    // private void OnMouseExit() {
    //     renderer.material.color -= Color.red;
    // }

    public void Select(bool state) {
        isSelected = state;
    }

    public void ChangeValues(float newOrbitDistance, float newOrbitSpeed) {
        orbitDistance = newOrbitDistance;
        orbitSpeed = newOrbitSpeed;
    }

    public float ReturnSpeed() {
        return orbitSpeed;
    }

    public float ReturnDistance() {
        return orbitDistance * 3;
    }

    public void CrashIntoPlanet() {
        Die();
        Debug.Log("I was too close to the planet");
    }

    public void Die() {
        Destroy(gameObject);
    }
}
