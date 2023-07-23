using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dragSpeed = 0.8f;
    private Vector3 dragOrigin;
    float sensitivity = 30f;
    float minFov = 15f;
    float maxFov = 90f;
    Camera cam;

    void Awake() {
        cam = Camera.main;
    }

    void Update()
    {
        //ZOOM
        float fov = cam.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.fieldOfView = fov;

        if (Input.GetMouseButtonDown(2)) {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) {
            return;
        }

        //Invert values to make it look like the mouse is pulling the screen
        Vector3 camera_position = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin) * -1;
        Vector3 camera_movement = new Vector3(camera_position.x * dragSpeed * -1, 0, camera_position.y * dragSpeed * -1);

        transform.Translate(camera_movement, Space.World);


    }
}
