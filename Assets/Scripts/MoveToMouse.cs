using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    public static List<MoveToMouse> moveableObjects = new List<MoveToMouse>();
    public float speed = 5f;
    public float moveableDistance = 10f;
    public GameObject rootSpawn;
    public GameObject rootNode;

    private Vector3 target;
    public bool selected;
    private bool canMove;
    private Vector3 initialPosition;
    public GameObject root;
    public float AddedPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveableObjects.Add(this);
        target = transform.position;
        initialPosition = transform.position;
        
        root = Instantiate(rootSpawn, transform.position, Quaternion.identity);
        AddedPoints = -moveableDistance;
        Strech(root, initialPosition, transform.position, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selected && moveableDistance > 0)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            float hold = Vector3.Distance(transform.position, target);

            canMove = true;
                
        }
        else if (moveableDistance <= 0)
        {
            canMove = false;
            
            if (Input.GetKeyDown(KeyCode.Space) && selected)
            {
                GameObject hold = Instantiate(rootNode, transform.position, Quaternion.identity);
                hold.GetComponent<SpriteRenderer>().sortingOrder = (this.GetComponent<SpriteRenderer>().sortingOrder + 1);
                hold.GetComponent<MoveToMouse>().moveableDistance = 10f;
                selected = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }  

        if (canMove)
        { 
            if(transform.position != target)
            {
                moveableDistance = moveableDistance - (speed * Time.deltaTime);
                Strech(root, initialPosition, transform.position, false);
            }
        }
        else
        {
            target = transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (selected && moveableDistance > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }

        else if (selected && moveableDistance <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

    }

    private void OnMouseDown()
    {
        selected = true;
        

        foreach (MoveToMouse obj in moveableObjects)
        {
            if(obj != this)
            {
                obj.selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }

    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        _sprite.transform.localScale = scale;
    }

    public float pointedToTotal()
    {
        float total = 0;

        for(int x = 0; x < moveableObjects.Count; x++)
        {
            total += moveableObjects[x].AddedPoints;
            moveableObjects[x].takePoints();

        }

        return total;
    }

    public void addPoint(float ap)
    {
        AddedPoints += ap;
    }

    public void takePoints()
    {
        AddedPoints = 0;
    }

    public float depth()
    {
        float deepest = 0;

        for (int x = 0; x < moveableObjects.Count; x++)
        {
            if(moveableObjects[x].transform.position.y < deepest)
            {
                deepest = moveableObjects[x].transform.position.y;
            }
        }
        return deepest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            canMove = false;
            target = transform.position;
        }
    }
}
