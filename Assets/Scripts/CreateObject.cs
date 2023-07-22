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

    public Planet PlanetPrefab;
    public Star StarPrefab;

    public void SelectType(int index) {
        Debug.Log(index);
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

    public void AddObject() {
        switch (objectType) {
            case Objects.star:
                Instantiate(StarPrefab, Vector3.zero, Quaternion.identity);
                Debug.Log("Creating a star...");
            break;
            case Objects.planet:
                Instantiate(PlanetPrefab, PlanetPrefab.transform.position, Quaternion.identity);
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
