using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (Character2D))]
    public class UserControl : MonoBehaviour
    {
        private Character2D character;
        private bool jump;

        private void Awake()
        {
            character = GetComponent<Character2D>();
        }

        private void Update()
        {
            if(!jump)
            // Read the jump input in Update so button presses aren't missed.
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            character.Move(h, jump);
            jump = false;
        }
    }
}