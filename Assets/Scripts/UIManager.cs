using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject panelCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PCanvas()
    {
        mainCanvas.SetActive(false);
    }

    public void MCanvas()
    {
        mainCanvas.SetActive(true);
    }
}
