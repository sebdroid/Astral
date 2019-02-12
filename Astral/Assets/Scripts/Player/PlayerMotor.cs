using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    private PlayerTrans Trans; //Reference to the player switching script

    [SerializeField] //Allows the Unity Editor to view the contents of the variable for debugging purposes
    private Camera cam; //Refence to the camera of the player

    private Vector3 velocity = Vector3.zero; //Initialises the Vector3 to store the velocity of the player and sets it to zero
    private Vector3 rotation = Vector3.zero; //Initialises the Vector3 to store the rotation of the player and sets it to zero

    private float camrotation = 0f; //Initialises the float variable to store the camera rotation of the player from input and sets it to zero
    private float curCamRotation = 0f; // Initialises the float variable to store the current camera rotation of the player and sets it to zero

    [SerializeField] //Allows the Unity Editor to view the contents of the variable for debugging purposes
    private float camLimit = 85f; //Initialises a float variable to store the maximum camera rotation permitted

    [SerializeField] //Allows the Unity Editor to view the contents of the variable for debugging purposes
    private Rigidbody Rb; //Refernce to the rigidbody of the parent

    private void CalibrateControls() //Declares a function to allow the Rigidbody to be reset
    {
        Rb = GetComponentInParent<Rigidbody>(); //Resets the Rigidbody to match that of the new parent object
    }

    void Start() //When the script starts
    {
        CalibrateControls(); //Reset Rigidbody
        cam = gameObject.GetComponentInChildren<Camera>(); //Sets the camera to the child camera of the object
    }

    public void SwitchChar(Transform playernew) //Declares the function to allow the player to switch clones.
    {
        transform.parent.tag = "Playable"; //Sets the tag of the current game object to "Playable" to allow the player to switch back
        transform.SetParent(playernew, false); //Changes the parent of the controller to switch to the clone
        playernew.tag = "Playing"; //Changes the tag of the new parent game object to "Playing" to indicate usage
        CalibrateControls(); //Resets Rigidbody
    }

    public void Move(Vector3 velocityinp) //Declares the function to accept player input and move the player
    {
        velocity = velocityinp; //Sets the velocity to the input velocity
    }

    public void Rotate(Vector3 rotationinp) //Declares the function to accept player input and rotate the player
    {
        rotation = rotationinp; //Sets the rotation to the input rotation
    }

    public void RotateCamera(float camrotationinp) //Declares the function to accept player input and rotate the camera
    {
        camrotation = camrotationinp; //Sets the camera rotation to the input rotation
    }

    void FixedUpdate() //Run every physics loop
    {
        if (transform.parent.CompareTag("Auto")) //If the tag of the game object is the same as the follower object
        {
            transform.parent.rotation = GameObject.Find("PController").transform.parent.rotation; //Set the rotation of the follower object to the rotation of the player
        }
        ApplyMovement(); //Applies the player movement
        ApplyRotation(); //Applies the player rotation
    }

    void ApplyMovement() //Declares the function to move the player
    {
        if (velocity != Vector3.zero) //If velocity is not zero
        {
            Rb.MovePosition(Rb.position + velocity * Time.fixedDeltaTime); //Move the player to the current position plus movement
        }
    }

    void ApplyRotation() //Declares the function to rotate the player
    {
        Rb.MoveRotation(Rb.rotation * Quaternion.Euler(rotation)); //Rotates the player to the new rotation

        if (cam != null) //If camera is attached
        {
            curCamRotation -= camrotation; //Adds the new camera rotation to the current camera rotation
            curCamRotation = Mathf.Clamp(curCamRotation, -camLimit, camLimit); //Clamps the camera rotation so it is within the constraints

            cam.transform.localEulerAngles = new Vector3(curCamRotation, 0f, 0f); //Rotates the camera to the new rotation
        }
    }


}