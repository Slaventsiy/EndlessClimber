using UnityEngine;

namespace UnitySampleAssets._2D
{
    public class Character2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	

        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = true; // Whether or not the player is grounded.

        private Animator anim; // Reference to the player's animator component.


        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            anim = GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
         //   grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            // Set the vertical animation
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        }


        public void Move(float move, bool jump)
        {
            Quaternion rotation = transform.Find("Arrow").transform.rotation;
            //only control the player if grounded or airControl is turned on
            if (grounded)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                //  GetComponent<Rigidbody2D>().velocity = new Vector2(move*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<Rigidbody2D>().velocity = new Vector2(rotation.x, rotation.y);
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }

            Debug.Log(grounded);
            // If the player should jump...
            if (grounded && jump/* && anim.GetBool("Ground")*/)
            {
                Debug.Log("JUMPIIIING");
                // Add a vertical force to the player.
                grounded = false;
                anim.SetBool("Ground", false);

                Vector3 dir = Quaternion.AngleAxis(rotation.eulerAngles.z, Vector3.forward) * Vector3.right;
                GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y) * 100;
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void OnCollisionEnter2D(Collision2D colInfo)
        {
            if (colInfo.collider.tag == "Platform")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                grounded = true;
            }
        }
    }
}
