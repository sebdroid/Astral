using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    private PlayerTrans Trans;

    //Allows camera to be optional
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject currentplayer;

    //Sets initial values to 0
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camrotation = Vector3.zero;

    //Also for debugging
    [SerializeField]
    private Rigidbody Rb;

    //Allows rigidbody and camera to be reset to new player
    private void CalibrateControls()
    {
        Rb = GetComponentInParent<Rigidbody>();
        //currentplayer = GameObject.Find("PController").transform.parent.gameObject;
        Debug.Log(currentplayer);
        if (transform.parent.tag == "Playable")
        {
            cam = gameObject.GetComponentInChildren<Camera>();
        }
    }

    //on start
    void Start()
    {
        CalibrateControls();
    }

    //allows player to switch locally
    public void SwitchChar(Transform playernew)
    {
        transform.SetParent(playernew, false);
        CalibrateControls();
    }

    public void Move(Vector3 velocityinp)
    {
        velocity = velocityinp;
    }

    public void Rotate(Vector3 rotationinp)
    {
        rotation = rotationinp;
    }

    public void RotateCamera(Vector3 camrotationinp)
    {
        camrotation = camrotationinp;
    }

    //Run every physics iteration

    void FixedUpdate()
    {
        if (transform.parent.tag == "Auto")
        {
            transform.parent.transform.rotation = GameObject.Find("PController").transform.parent.gameObject.transform.rotation;
        }
        ApplyMovement();
        ApplyRotation();
    }

    void ApplyMovement()
    {
        if (velocity != Vector3.zero)
        {
            Rb.MovePosition(Rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void ApplyRotation()
    {
        Rb.MoveRotation(Rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            cam.transform.Rotate(-camrotation);
        }
    }


}