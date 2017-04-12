using UnityEngine;

namespace UnitySampleAssets._2D
{
    public class Character2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        public float speed = 0.1f;
        public LayerMask whatIsGround; // A mask determining what is ground to the character

        public GameObject impactEffect;

        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = true; // Whether or not the player is grounded.
        private bool isHooked = false; // Is character pulling himself.
        private Animator anim; // Reference to the player's animator component.
        private Vector3 pullDirection;
        private Hook hook;

        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            anim = GetComponent<Animator>();
            GameObject arrow = GameObject.Find("Arrow");
            hook = arrow.GetComponent<Hook>();
        }

        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            //   grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            // Set the vertical animation
            anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

            if (isHooked)
            {
                Move();
            }
        }

        public void Move()
        {
            anim.SetBool("Ground", false);

            Vector3 distanceToMove = pullDirection * speed;
            Vector3 transformedDistanceToMove = transform.TransformDirection(distanceToMove);

            if (Mathf.Abs(transform.position.x + distanceToMove.x) <= 7)
            {
                transform.Translate(distanceToMove, Space.World);
            }
            else
            {
                transform.position.Set(7, transform.position.y, transform.position.z);
                isHooked = false;
                OnWallReach();
                hook.Aim();
            }
        }

        public void HookUp(Vector3 direction)
        {
            pullDirection = direction;
            
            isHooked = true;
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
        }

        private void OnWallReach()
        {
            grounded = true;
            Flip();
            CreateImpactEffect();
            GameMaster.UpdateScore();
        }
    }
}
