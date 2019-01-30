using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    private PlayerTrans Trans;

    //Allows camera to be optional
    [SerializeField]
    private Camera cam;

    //Sets initial values to 0
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private float camrotation = 0f;
    private float curCamRotation = 0f;

    [SerializeField]
    private float camLimit = 85f;

    //Also for debugging
    [SerializeField]
    private Rigidbody Rb;

    //Allows rigidbody and camera to be reset to new player
    private void CalibrateControls()
    {
        Rb = GetComponentInParent<Rigidbody>();
        //currentplayer = GameObject.Find("PController").transform.parent.gameObject;
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
        transform.parent.tag = "Playable";
        transform.SetParent(playernew, false);
        playernew.tag = "Playing";
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

    public void RotateCamera(float camrotationinp)
    {
        camrotation = camrotationinp;
    }

    //Run every physics iteration

    void FixedUpdate()
    {
        //Try moving out of fixed update into apply rotation
        if (transform.parent.tag == "Auto")
        {
           transform.parent.rotation = GameObject.Find("PController").transform.parent.rotation;
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
            // Set our rotation and clamp it
            curCamRotation -= camrotation;
            curCamRotation = Mathf.Clamp(curCamRotation, -camLimit, camLimit);

            //Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(curCamRotation, 0f, 0f);
        }
    }


}