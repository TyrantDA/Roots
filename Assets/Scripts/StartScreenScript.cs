using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    public Text highscore;
    // Start is called before the first frame update
    void Start()
    {
        float hold = PlayerPrefs.GetFloat("highscore", 0);
        highscore.text = "Highscore : " + hold;
    }

    // Update is called once per frame
    void Update()
    {
        float hold = PlayerPrefs.GetFloat("highscore", 0);
        highscore.text = "Highscore : " + hold;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void resetHighscore()
    {
        PlayerPrefs.DeleteKey("highscore");
    }

}
