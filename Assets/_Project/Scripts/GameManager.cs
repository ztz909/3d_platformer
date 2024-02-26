using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int currentSpheres;
    public Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSpheres(int sphereToAdd)
    {
        currentSpheres += sphereToAdd;
        uiText.text = "Spheres: " + currentSpheres;
    }
}
