using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof(Character2D))]
    public class Hook : MonoBehaviour
    {

        private bool isShot = false;
        private float speed = 0.1f;
        public float rotationSpeed = 50;
        private int maxAngle = 60;
        private Quaternion rotation;
        public GameObject tip;
        private Vector3 shotDirection;
        public UnityEvent arrowStop;
        private Character2D character;
        private bool isAiming = true;
        private GameObject playa;
        private bool facingRight = true;
        
        void Awake()
        {
            playa = GameObject.Find("Player");
            character = playa.GetComponent<Character2D>();
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

                if (Mathf.Abs(tip.transform.position.x) >= 7)
                {
                    isShot = false;
                    character.HookUp(shotDirection);
                }
            }
            if (isAiming)
            {
                Rotate();
            }
        }

        public void Shoot(Quaternion rotation)
        {
            shotDirection = Quaternion.AngleAxis(rotation.eulerAngles.z, Vector3.forward) * Vector3.right;
            isShot = true;
            isAiming = false;
            this.rotation = rotation;
        }

        private void Move()
        {
            transform.Translate(shotDirection * speed, Space.World);
        }

        private void Rotate()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.PingPong(Time.time * rotationSpeed, maxAngle)));
        }

        public void Aim()
        {
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
