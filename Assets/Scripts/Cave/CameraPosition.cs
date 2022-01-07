using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GenerateCave cave;
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        transform.position = new Vector3((cave.width / 2f) * 0.64f, (cave.height / 2f) * 0.64f, -10f);
        _camera.orthographicSize = cave.height <= cave.width ? cave.width * (1f/3f) : cave.height * (1f/3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((cave.width / 2f) * 0.64f, (cave.height / 2f) * 0.64f, -10f);
        _camera.orthographicSize = cave.height <= cave.width ? cave.width * (1f/3f) : cave.height * (1f/3f);
    }
}
