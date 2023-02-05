using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceNumber : MonoBehaviour
{
    public Text resource;
    public float startingTotal;
    public MoveToMouse hold;
    public float total;
    public Scoring score;
    public GameObject textWarning;
    // Start is called before the first frame update
    void Start()
    {
        resource.text = " " + startingTotal + " ";
        total = startingTotal;
    }

    // Update is called once per frame
    void Update()
    {
        total += hold.pointedToTotal();

        if(total < 0)
        {
            float hold = PlayerPrefs.GetFloat("highscore", 0);

            if(score.scoreNumber > hold)
            {
                PlayerPrefs.SetFloat("highscore", score.scoreNumber);
            }

            SceneManager.LoadScene("startScene");
            textWarning.SetActive(false);
        }
        else if(total == 0)
        {
            textWarning.SetActive(true);
        }
        resource.text = "Resources : " + total + " ";
    }
}
