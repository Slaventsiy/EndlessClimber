using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof (Character2D))]
    public class UserControl : MonoBehaviour
    {
        private Character2D character;
        private Hook hook;
        private bool jump = false;
        private MovingLava lava;
        private GameObject arrow;

        private void Awake()
        {
            character = GetComponent<Character2D>();
            arrow = GameObject.Find("Arrow");
            hook = arrow.GetComponent<Hook>();
            lava = GameObject.Find("Lava").GetComponent<MovingLava>();
        }

        private void Update()
        {
            if (!jump && !GameMaster.gm.GameOverUI.activeInHierarchy)
            {
                // Read the jump input in Update so button presses aren't missed.
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            if (jump)
            {
                hook.Shoot(arrow.transform.localRotation);
                lava.StartLava();
                jump = false;
            }

        }
    }
}