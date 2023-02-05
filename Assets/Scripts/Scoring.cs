using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text score;
    public float scoreNumber = 0;
    public MoveToMouse hold;

    // Start is called before the first frame update
    void Start()
    {
        score.text = " " + scoreNumber + " ";
    }

    // Update is called once per frame
    void Update()
    {
        scoreNumber = -hold.depth();
        score.text = "Score : " + scoreNumber + " ";
    }
}
