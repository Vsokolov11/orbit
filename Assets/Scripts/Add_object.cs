using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_object : MonoBehaviour
{
    public GameObject planet;

    public void add_planet() {
        Instantiate(planet, new Vector3(0 , 0, 0), Quaternion.identity);
        Debug.Log("New Planet");
    }
    void add_star() {
        Debug.Log("New Star");
    }
}
