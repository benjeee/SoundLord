using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float speed = 12f;
	[SerializeField]
	private float sens = 2.5f;

	private PlayerMotor motor;

    [SerializeField]
    private float dashCooldown;
    private float timeSinceLastDash;

	void Start()
	{
		Cursor.visible = false;
		motor = GetComponent<PlayerMotor> ();
	}

    void HandleMovement()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        if (Input.GetButton("Jump"))
        {
            velocity.y = 50;
        } else if (Input.GetKey(KeyCode.LeftControl))
        {
            velocity.y = -50;
        }
        motor.Move(velocity);   

        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 playerRotation = new Vector3(0f, yRot, 0f) * sens;
        motor.Rotate(playerRotation);

        float camRotation = xRot * sens;
        motor.CamRotate(camRotation);
    }

    void Update()
	{
        HandleMovement();
    }
}
