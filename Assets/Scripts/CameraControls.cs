using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    [SerializeField] private GameObject map;

    public float speed;
    Vector3 moveVec;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    // Start is called before the first frame update
    private void Awake()
    {
        mapMinX = map.transform.position.x - map.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        mapMaxX = map.transform.position.x + map.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        mapMinY = map.transform.position.y - map.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        mapMaxY = map.transform.position.y + map.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        zoomCam();
    }

    private void PanCamera()
    {
        moveVec = new Vector3(0f, 0f, 0f);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.y += speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.y -= speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x -= speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x += speed * Time.deltaTime;
        }

        cam.transform.position = ClampCamera(cam.transform.position + moveVec);
        
        //if(cam.transform.position.y > this.transform.position.y)
        //{
        //    cam.transform.position = new Vector3 (cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
        //}

        //if(cam.transform.position.x < left.transform.position.x)
        //{
        //    cam.transform.position = new Vector3(left.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        //}

        //if (cam.transform.position.x > right.transform.position.x)
        //{
        //    cam.transform.position = new Vector3(right.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        //}
    }

    private void zoomCam()
    {
        if(cam.GetComponent<Camera>().orthographic)
        {
            float newsize = cam.GetComponent<Camera>().orthographicSize - Input.GetAxis("Mouse ScrollWheel") * speed;
            cam.GetComponent<Camera>().orthographicSize = Mathf.Clamp(newsize, 5, 16);
            cam.transform.position = ClampCamera(cam.transform.position);
        }
        else
        {
            cam.GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * speed;
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.GetComponent<Camera>().orthographicSize;
        float camWidth = cam.GetComponent<Camera>().orthographicSize * cam.GetComponent<Camera>().aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
