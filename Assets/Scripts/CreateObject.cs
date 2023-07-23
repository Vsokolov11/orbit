using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public enum Objects {
        star,
        planet,
        moon
    }

    public static Objects objectType;

    GameObject selected;
    Camera Camera;

    static float orbitDistanceValue;
    static float orbitSpeedValue;

    public Planet PlanetPrefab;
    public Star StarPrefab;

    public void Start() {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo)) {
                selected = hitInfo.transform.gameObject;
            }
            else {
                selected = null;
            }

            if (selected != null) {
                if(selected.GetComponent<Planet>() != null) {
                    Debug.Log("Selected Object is a planet...");
                    Planet selectedPlanet = selected.GetComponent<Planet>();
                    selectedPlanet.Select(true);
                    selectedPlanet.ChangeValues(orbitDistanceValue, orbitSpeedValue);
                }
                if(selected.GetComponent<Star>() != null) {
                    Debug.Log("Selected Object is a star...");
                }
            }
        }
    }

    public void SelectType(int index) {
        switch (index) {
            case 0:
                Debug.Log("You chose a star...");
                objectType = Objects.star;
            break;
            case 1:
                Debug.Log("You chose a planet...");
                objectType = Objects.planet;
            break;
            case 2:
                Debug.Log("You chose a moon...");
                objectType = Objects.moon;
            break;
        }
    }

    public void SetOrbitDistance(float value) {
        orbitDistanceValue = value;
    }

    public void SetOrbitSpeed(float value) {
        orbitSpeedValue = value;
    }

    public void AddObject() {
        switch (objectType) {
            case Objects.star:
                Instantiate(StarPrefab, Vector3.zero, Quaternion.identity);
                Debug.Log("Creating a star...");
            break;
            case Objects.planet:
                Planet instantiatedPlanet = Instantiate(PlanetPrefab, new Vector3(1, 0, 1), Quaternion.identity);
                instantiatedPlanet.ChangeValues(orbitDistanceValue, orbitSpeedValue);
                Debug.Log("Creating a planet...");
            break;
            case Objects.moon:
                Debug.Log("Creatig a moon...");
            break;
            default:
                Debug.Log("No such type of stellar object...");
            break;
        }
    }
}
