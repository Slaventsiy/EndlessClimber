using UnityEngine;

namespace UnitySampleAssets._2D
{
    public class Character2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        public float speed = 70f;
        public LayerMask whatIsGround; // A mask determining what is ground to the character

        public GameObject impactEffect;

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

            // If the player should jump...
            if (grounded && jump/* && anim.GetBool("Ground")*/)
            {
                // Add a vertical force to the player.
                grounded = false;
                anim.SetBool("Ground", false);

                Vector3 dir = Quaternion.AngleAxis(rotation.eulerAngles.z, Vector3.forward) * Vector3.right;
                if (!facingRight)
                {
                    dir.x *= -1;
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y) * speed;
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void CreateImpactEffect()
        {
            GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, Quaternion.Euler(0, 90 * transform.localScale.x, 0));

            Debug.Log(effect.transform.rotation.y);
        }

        private void OnCollisionEnter2D(Collision2D colInfo)
        {
            if (colInfo.collider.tag == "Platform" || colInfo.collider.tag == "PlatformOriginal")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                grounded = true;
                Flip();
                CreateImpactEffect();
                GameMaster.UpdateScore();             
            }
        }        
    }
}
