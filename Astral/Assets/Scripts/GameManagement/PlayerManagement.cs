using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{

    private GameManagement game;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5)
        {
            if (gameObject.CompareTag("Playing"))
            {
                game.EndGame(true, "GAME OVER");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        game = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }

}
