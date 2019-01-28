using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagement : MonoBehaviour
{

    [SerializeField]
    private Object spare;

    private bool DCP1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Auto"))
        {
            Destroy(obj.gameObject);
            Debug.Log("Spawning clone...");
            Instantiate(spare, new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, obj.gameObject.transform.position.z), Quaternion.identity);
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
