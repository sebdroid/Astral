using UnityEngine;

public class PlayerTrans : MonoBehaviour {

    private PlayerMotor Motor;

    [SerializeField]
    private Object sparePlayer;

    public bool canCreate;

    [SerializeField]
    private Camera cam;

    private float range = 100f;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
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
        }else if ((Input.GetButtonDown("Fire2")) && (canCreate)){
            Target(2);
        }
	}

    void Target(int mode)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
        {
            if (mode == 1)
            {
                if (hit.transform.tag == "Playable")
                {
                    Motor.SwitchChar(hit.transform);
                }
            }
            else if (mode == 2)
            {
                if (true)
                {
                    Debug.Log("Spawning clone...");
                    Instantiate(sparePlayer, new Vector3(hit.point.x, hit.point.y+1, hit.point.z), Quaternion.identity);
                }
            }
        }
    }
}
