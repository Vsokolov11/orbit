using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public enum Objects {
        star,
        planet,
        moon
    }

    public Slider speedSlider;
    public Slider distanceSlider;

    public static Objects objectType;
    public static Objects selectedType;

    GameObject selected;
    Camera Camera;
    GameObject RunningCanvas;
    GameObject PausedCanvas;

    static float orbitDistanceValue;
    static float orbitSpeedValue;

    public Planet PlanetPrefab;
    public Planet selectedPlanet;
    public Star StarPrefab;
    public Moon MoonPrefab;
    public Moon selectedMoon;

    private bool slidersSet;
    private bool isPaused;
    

    public void Start() {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        selectedPlanet = null;
        slidersSet = false;
        isPaused = false;
        speedSlider = GameObject.FindWithTag("SpeedSlider").GetComponent<Slider>();
        distanceSlider = GameObject.FindWithTag("DistanceSlider").GetComponent<Slider>();
        if(speedSlider == null) {
            Debug.Log("Speed slider couldn't be found");
        }
        else {
            speedSlider.value = 30;
        }
        if(distanceSlider == null) {
            Debug.Log("Distance slider couldn't be found");
        }
        else {
            distanceSlider.value = 20;
        }

        RunningCanvas = GameObject.FindWithTag("RunningCanvas");
        if(RunningCanvas == null) {
            Debug.Log("Running canvas couldn't be found");
        }

        PausedCanvas = GameObject.FindWithTag("PausedCanvas");
        if(PausedCanvas != null) {
            PausedCanvas.GetComponent<Canvas> ().enabled = false;
        } 
        else {
            Debug.Log("Paused canvas couldn't be found");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            PauseGame();
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            ResumeGame();
            isPaused = false;
        } 

        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo)) {
                selected = hitInfo.transform.gameObject;
                slidersSet = false;
            }
        }

        if (selected != null) {
            if(selectedPlanet != null && selectedPlanet != selected.GetComponent<Planet>()) {
                selectedPlanet.Select(false);
                selectedPlanet = null;
                slidersSet = false;
            }

            if(selectedMoon != null && selectedMoon != selected.GetComponent<Moon>()) {
                selectedMoon.Select(false);
                selectedMoon = null;
                slidersSet = false;
            }

            if(selected.GetComponent<Planet>() != null) {
                selectedType = Objects.planet;
                selectedPlanet = selected.GetComponent<Planet>();
                selectedPlanet.Select(true);
                
                if(!slidersSet) {    
                    distanceSlider.value = selectedPlanet.ReturnDistance();
                    speedSlider.value = selectedPlanet.ReturnSpeed();
                    slidersSet = true;
                }

                if((selectedPlanet.ReturnDistance() != distanceSlider.value || selectedPlanet.ReturnSpeed() != speedSlider.value) &&
                    slidersSet == true) {
                    if(orbitDistanceValue <= StarPrefab.dangerZone) {
                        selectedPlanet.CrashIntoStar();
                        selected = null;
                    }
                    selectedPlanet.ChangeValues(orbitDistanceValue, orbitSpeedValue);
                }
            }

            if(selected.GetComponent<Moon>() != null) {
                selectedType = Objects.moon;
                selectedMoon = selected.GetComponent<Moon>();
                selectedMoon.Select(true);

                if(!slidersSet) {    
                    distanceSlider.value = selectedMoon.ReturnDistance();
                    speedSlider.value = selectedMoon.ReturnSpeed();
                    slidersSet = true;
                }

                if((selectedMoon.ReturnDistance() != distanceSlider.value || selectedMoon.ReturnSpeed() != speedSlider.value) &&
                    slidersSet == true) {
                    if(orbitDistanceValue <= StarPrefab.dangerZone) {
                        selectedMoon.Die();
                        selected = null;
                    }
                    selectedMoon.ChangeValues(orbitDistanceValue / 3, orbitSpeedValue);
                }
            }

            if(selected.GetComponent<Star>() != null) {

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
                StarPrefab.dangerZone = 2;
                Debug.Log("Creating a star...");
            break;
            case Objects.planet:
                Planet instantiatedPlanet = Instantiate(PlanetPrefab, new Vector3(1, 0, 1), Quaternion.identity);
                instantiatedPlanet.ChangeValues(orbitDistanceValue, orbitSpeedValue);
                Debug.Log("Creating a planet...");
                if (orbitDistanceValue <= StarPrefab.dangerZone)
                {
                    Debug.Log("Planet was too close to the sun...");
                    instantiatedPlanet.CrashIntoStar();
                }                
            break;
            case Objects.moon:
                Moon instantiatedMoon = null;
                if (selected != null && selectedType == Objects.planet) {
                    instantiatedMoon = Instantiate(MoonPrefab, new Vector3(1, 0 ,1), Quaternion.identity, selected.transform);
                    instantiatedMoon.InitializeMoon();
                    instantiatedMoon.ChangeValues(orbitDistanceValue / 3, orbitSpeedValue);
                    Debug.Log("Creatig a moon...");
                } else {
                    Debug.Log("There is no planet for the moon to go around...");
                    instantiatedMoon.Die();
                }
            break;
            default:
                Debug.Log("No such type of stellar object...");
            break;
        }
    }

    public void PauseGame ()
    {
        Debug.Log("Pausing");
        RunningCanvas.GetComponent<Canvas> ().enabled = false;
        PausedCanvas.GetComponent<Canvas> ().enabled = true;
    }

    public void ResumeGame ()
    {
        Debug.Log("Resum");
        RunningCanvas.GetComponent<Canvas> ().enabled = true;
        PausedCanvas.GetComponent<Canvas> ().enabled = false;
    }
}
