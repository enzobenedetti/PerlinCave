using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLife : MonoBehaviour
{
    public GameLife game;
    public UIScript UIScript;
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        transform.position = new Vector3((game.width / 2f) * (1f + UIScript.gap.value), (game.height / 2f) * (1f + UIScript.gap.value), -10f);
        _camera.orthographicSize = game.height <= game.width ? game.width * (((1f + UIScript.gap.value)*2f)/3f) : game.height * (((1f + UIScript.gap.value)*2f)/3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((game.width / 2f) * (1f + UIScript.gap.value), (game.height / 2f) * (1f + UIScript.gap.value), -10f);
        _camera.orthographicSize = game.height <= game.width ? game.width * (((1f + UIScript.gap.value)*2f)/3f) : game.height * (((1f + UIScript.gap.value)*2f)/3f);
    }
}
