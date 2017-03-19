﻿using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof (Character2D))]
    public class UserControl : MonoBehaviour
    {
        private Character2D character;
        private Hook hook;
        private bool jump = false;
        private Transform arrow;

        private void Awake()
        {
            character = GetComponent<Character2D>();
            arrow = transform.Find("Arrow");
            hook = arrow.GetComponent<Hook>();
        }

        private void Update()
        {
            if (!jump)
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
                hook.Shoot(arrow.localRotation);
            // character.Move(h, jump);
                jump = false;
        }
    }
}