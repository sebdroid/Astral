using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 1f;

    private PlayerMotor Motor;

    void Start()
    {
        Motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //calculate movement velocity as 3d vector
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        // Final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        //Apply movement via motor
        Motor.Move(velocity);

        //Calculate rotation as vector
        //Only moves rotation horizontally so it affects camera + player model
        //Rotation horizontally in Unity is the Y rotation????
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Apply rotation

        Motor.Rotate(rotation);

        //This is the vertical rotation but only for camera so it doesn't seem retarded

        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 camrotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation

        Motor.RotateCamera(camrotation);

    }

}