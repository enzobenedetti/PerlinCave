using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider speedSlider;

    public Slider height;
    public Slider width;
    public Slider gap;
    public Grid grid;
    public GameLife gameScript;
    // Start is called before the first frame update
    void Start()
    {
        if (gameScript == null)
            gameScript = GetComponent<GameLife>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartSimulation()
    {
        gameScript.StartSimulation();
    }

    public void ChangeSpeed()
    {
        gameScript.updateSpeed = speedSlider.value;
    }

    public void ChangeHeight()
    {
        gameScript.height = (int)height.value;
        gameScript.SetSize();
    }

    public void ChangeWidth()
    {
        gameScript.width = (int) width.value;
        gameScript.SetSize();
    }

    public void ChangeGap()
    {
        grid.cellSize = new Vector3( 1+ gap.value, 1 + gap.value, 0);
    }
}
