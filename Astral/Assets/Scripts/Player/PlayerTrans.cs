using UnityEngine;

public class PlayerTrans : MonoBehaviour {

    private PlayerMotor Motor;

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
            Debug.LogError("No camera defined?");
            this.enabled = false;
        }
    }
	
	void Update()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            Target();
        }
	}

    void Target()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
        {
            if (hit.transform.tag == "Playable")
            {
                Debug.Log("We hit " + hit.collider.name);
                Motor.SwitchChar(hit.transform);
            }
        }
    }
}
