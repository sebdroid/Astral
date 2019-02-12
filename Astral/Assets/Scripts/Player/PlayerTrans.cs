using UnityEngine;

public class PlayerTrans : MonoBehaviour
{

    private PlayerMotor Motor; //Reference to movement script

    [SerializeField]
    private Object sparePlayer; //Reference to clone prefab

    public bool canCreate; //Keeps track of whether the player can create clones in the current stage

    [SerializeField]
    private Camera cam; //Reference to the child camera of the player

    private float range = 100f; //Stores the maximum range of the switching raycast

    [SerializeField]
    private LayerMask mask; //Determines what objects the raycast can pass through

    void Start()
    {
        transform.hasChanged = false;
        Motor = GetComponent<PlayerMotor>();
        cam = gameObject.GetComponentInChildren<Camera>();
        if (cam == null)
        {
            Debug.LogError("No camera defined");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Target(1);
        }
        else if ((Input.GetButtonDown("Fire2")) && (canCreate))
        {
            Target(2);
        }
        if (transform.hasChanged)
        {
            GameObject.Find("GameManagement").GetComponent<GameManagement>().timer = true;
        }
    }

    void Target(int mode)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
        {
            if (mode == 1)
            {
                if (hit.transform.CompareTag("Playable"))
                {
                    Motor.SwitchChar(hit.transform);
                }
            }
            else if (mode == 2)
            {
                Instantiate(sparePlayer, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                GameObject.Find("GameManagement").GetComponent<GameManagement>().clones++;
            }
        }
    }
}
