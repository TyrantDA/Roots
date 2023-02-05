using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject fill;
    private bool emptying;
    public float speed = 1f;
    public float pointValue;

    GameObject hold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(emptying)
        {
            if (fill.transform.localScale.y > 0)
            {
                fill.transform.localScale -= new Vector3(0f, speed * Time.deltaTime, 0f);
                fill.transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
                if(pointValue > 0)
                {
                    hold.GetComponent<MoveToMouse>().addPoint(speed);
                    pointValue -= speed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RootNode"))
        {
            emptying = true;
            hold = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RootNode"))
        {
            emptying = false;
        }
    }
}
