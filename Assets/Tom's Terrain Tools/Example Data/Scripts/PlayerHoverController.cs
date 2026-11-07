using UnityEngine;
using System.Collections;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TTDemoScripts
{
    public class PlayerHoverController : MonoBehaviour
    {
        public float forwardSpeed = 10;
        public float strafeSpeed = 10;
        public float runMultiplier = 2;
        public KeyCode runKey = KeyCode.LeftShift;
        public LayerMask groundLayer;

        RaycastHit hit;
        float hoverHeight = 0;

        void Update()
        {
            var moveInput = GetMoveInput();
            var run = GetRunPressed() ? runMultiplier : 1f;

            float y = moveInput.y * forwardSpeed * run * Time.deltaTime;
            float x = moveInput.x * strafeSpeed * Time.deltaTime;

            // hover
            if (Physics.Raycast(transform.position + Vector3.up * 9999, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                hoverHeight = hit.point.y + 1.8f;
            }


            transform.Translate(new Vector3(x, hoverHeight - transform.position.y + 1.8f, y));
        }

        Vector2 GetMoveInput()
        {
#if ENABLE_INPUT_SYSTEM
            var keyboard = Keyboard.current;
            var gamepad = Gamepad.current;

            if (keyboard != null)
            {
                float x = 0f;
                float y = 0f;

                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) x -= 1f;
                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) x += 1f;
                if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) y -= 1f;
                if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) y += 1f;

                var keyboardMove = new Vector2(x, y);
                if (keyboardMove.sqrMagnitude > 0f)
                    return keyboardMove;
            }

            if (gamepad != null)
                return gamepad.leftStick.ReadValue();
#endif
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        bool GetRunPressed()
        {
#if ENABLE_INPUT_SYSTEM
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                switch (runKey)
                {
                    case KeyCode.LeftShift:
                        return keyboard.leftShiftKey.isPressed;
                    case KeyCode.RightShift:
                        return keyboard.rightShiftKey.isPressed;
                    case KeyCode.LeftControl:
                        return keyboard.leftCtrlKey.isPressed;
                    case KeyCode.RightControl:
                        return keyboard.rightCtrlKey.isPressed;
                    case KeyCode.LeftAlt:
                        return keyboard.leftAltKey.isPressed;
                    case KeyCode.RightAlt:
                        return keyboard.rightAltKey.isPressed;
                    default:
                        return keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed;
                }
            }
#endif
            return Input.GetKey(runKey);
        }
    }

}
