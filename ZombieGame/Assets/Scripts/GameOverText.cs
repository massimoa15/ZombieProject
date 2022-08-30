using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Game Over\nEnemies Killed: " + GlobalData.GetEnemiesKilled();
    }
}
