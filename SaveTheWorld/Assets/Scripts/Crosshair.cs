using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Transform origin;
    private Vector2 mousePosition;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<Camera>();
        origin = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        Vector3 mouse = new Vector3((mousePosition.x), (mousePosition.y), 0);
        Vector3 viewPos = cam.ScreenToWorldPoint (mousePosition);
        Vector3 corrected = new Vector3(viewPos.x, viewPos.y, 0);
        transform.position = corrected;
    }
}
