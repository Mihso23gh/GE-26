using UnityEngine;
using System.Collections;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Just a quick 2d circle movement sample

namespace TTTSamples
{
	public class CircleMoverSample : MonoBehaviour 
	{
		public LayerMask layerMaskGround;
        public float moveSpeed=150f;
		public float jumpForce=20f;
		private float torque = 0;
		private Rigidbody2D rb;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			torque = GetHorizontalInput() * moveSpeed * Time.deltaTime;

			if (GetJumpPressed())
            {
                // check if grounded
				if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerMaskGround))
				{
                    rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
				}
            }
        }

		void FixedUpdate () 
		{
			rb.AddTorque(-torque);
		}

        float GetHorizontalInput()
        {
#if ENABLE_INPUT_SYSTEM
            var keyboard = Keyboard.current;
            var gamepad = Gamepad.current;

            if (keyboard != null)
            {
                float value = 0f;
                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) value -= 1f;
                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) value += 1f;
                if (!Mathf.Approximately(value, 0f))
                    return value;
            }

            if (gamepad != null)
                return gamepad.leftStick.ReadValue().x;
#endif
            return Input.GetAxis("Horizontal");
        }

        bool GetJumpPressed()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
                return true;

            if (Gamepad.current != null && Gamepad.current.buttonSouth.isPressed)
                return true;
#endif
            return Input.GetButton("Jump");
        }
	}
}