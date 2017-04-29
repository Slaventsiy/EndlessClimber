using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof(Character2D))]
    public class Hook : MonoBehaviour
    {

        private bool isShot = false;
        public float speed = 0.5f;
        public float rotationSpeed = 50;
        private int maxAngle = 60;
        private Quaternion rotation;
        public GameObject tip;
        private Vector3 shotDirection;
        public UnityEvent arrowStop;
        private Character2D character;
        private bool isAiming = true;
        private GameObject player;
        private bool facingRight = true;
        
        void Awake()
        {
            player = GameObject.Find("Player");
            character = player.GetComponent<Character2D>();
            // GetComponent<Character2D>();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (character == null)
            {
                character = GetComponent<Character2D>();
            }
            if (isShot)
            {
                Move();
            }
            if (isAiming)
            {
                Rotate();
            }
        }

        public void Shoot(Quaternion rotation)
        {
            if (!isAiming) return;

            float arrowRotation = rotation.eulerAngles.z;
            Vector3 axis = Vector3.forward;
            if (!facingRight)
            {
                axis = Vector3.back;
            }

            shotDirection = Quaternion.AngleAxis(arrowRotation, axis) * Vector3.right;
            if (!facingRight)
            {
                shotDirection.x *= -1;
            }

            isShot = true;
            isAiming = false;
            this.rotation = rotation;
        }

        private void Move()
        {
            Vector3 distanceToMove = shotDirection * speed;
         
            if (Mathf.Abs(tip.transform.position.x + distanceToMove.x) < 7)
            {
                transform.Translate(distanceToMove, Space.World);
            }
            else
            {
                float overShootOnX = Mathf.Abs(tip.transform.position.x + distanceToMove.x) - 7.0f;
                float ratio = Mathf.Abs(overShootOnX / distanceToMove.x);

                Vector3 correctedDistanceToMove = distanceToMove * (1 - ratio);

                transform.position += correctedDistanceToMove;
                isShot = false;

                character.HookUp(shotDirection);

                Collider2D[] overlappingPlatforms = Physics2D.OverlapCircleAll(new Vector2(tip.transform.position.x, tip.transform.position.y), 0.1f, 1 << LayerMask.NameToLayer("Platforms"));

                if (overlappingPlatforms.Length == 0)
                {
                    GameMaster.ActivateGameOverScreen();
                }
                else
                {
                    GameMaster.UpdateScore();
                }
            }
        }

        private void Rotate()
        {
            float rotation = Mathf.PingPong(Time.time * rotationSpeed, maxAngle);
            if (!facingRight)
            {
                rotation += 300;
            }
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }

        public void Aim()
        {
            Vector3 arrowSpawnPoint = player.transform.FindChild("ArrowSpawnPoint").transform.position;
            transform.position = arrowSpawnPoint;
            isAiming = true;
            Flip();
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
    }
}
