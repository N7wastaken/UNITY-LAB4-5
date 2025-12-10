using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CC_Movement : MonoBehaviour
{

    public float speed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    private CharacterController cc;
    private Vector3 velocity;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(x, 0f, z);
        move = Vector3.ClampMagnitude(move, 1f);

        cc.Move(move * speed * Time.deltaTime);


        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -1f;


        if (cc.isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);


        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
    public void LaunchMultiplier(float multiplier)
    {

        float baseJumpVel = Mathf.Sqrt(jumpHeight * -2.0f * gravity);


        velocity.y = baseJumpVel * multiplier;
    }

}
