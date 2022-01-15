using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorders : MonoBehaviour
{
    public GameObject leftBorder, rightBorder, topBorder, bottomBorder;

    private float horizBorderDist, vertBorderDist;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        horizBorderDist = transform.position.x + cam.orthographicSize * cam.aspect;
        vertBorderDist = transform.position.y + cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        leftBorder.transform.position = pos - new Vector3(horizBorderDist,0,0);
        rightBorder.transform.position = pos + new Vector3(horizBorderDist, 0, 0);
        topBorder.transform.position = pos + new Vector3(0, vertBorderDist, 0);
        bottomBorder.transform.position = pos - new Vector3(0, vertBorderDist, 0);
    }
}
