using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagement : MonoBehaviour
{

    private GameManagement game;

    int score;

    [SerializeField]
    private Object spare;

    [SerializeField]
    private Object auto;

    [SerializeField]
    private Vector3 fall = new Vector3(15, 29.25f, -9);

    [SerializeField]
    private Vector3 fallauto = new Vector3(15, 29.25f, -4);

    private bool DCP1;

    private void Start()
    {
        game = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }

    private void housekeep(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        for (var i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if ((obj.CompareTag("Auto")) && (gameObject.CompareTag("DCP")))
        {
            Destroy(obj.gameObject);
            Debug.Log("Spawning clone...");
            Instantiate(spare, new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, obj.gameObject.transform.position.z), Quaternion.identity);
        }
        else if (gameObject.CompareTag("DCP2"))
        {
            housekeep("Playable");
            GameObject.FindGameObjectWithTag("Playing").transform.position = fall;
            Instantiate(auto, fallauto, Quaternion.identity);
        }
        else if (gameObject.CompareTag("DCP3"))
        {
            housekeep("Playable");
            Destroy(obj.gameObject);
            GameObject.FindGameObjectWithTag("Playing").transform.position = fall;
            Instantiate(auto, fallauto, Quaternion.identity);
            game.clones++;
        }
        else if (gameObject.CompareTag("DCP4"))
        {
            game.EndGame(false, "GAME\nCOMPLETE");
        }
    }

    void OnTriggerStay(Collider obj)
    {
        if ((obj.CompareTag("Playing")) && (!DCP1))
        {
            Destroy(GameObject.FindGameObjectWithTag("DCP1"));
            DCP1 = true;
            GameObject.FindGameObjectWithTag("Playing").GetComponentInChildren<PlayerTrans>().canCreate = true;
        }
    }
}
