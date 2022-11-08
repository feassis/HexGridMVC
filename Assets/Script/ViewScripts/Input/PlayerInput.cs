using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace MVC.View.InputHandler
{
    public class PlayerInput : MonoBehaviour
    {
        public Action<Vector3> PointerClicked;

        void Update()
        {
            DetectPlayerClick();
        }

        private void DetectPlayerClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                PointerClicked?.Invoke(mousePos);
            }
        }

        public void RegisterToPointerClicked(Action<Vector3> action)
        {
            PointerClicked += action;
        }

        public void DeregisterToPointerClicked(Action<Vector3> action)
        {
            PointerClicked -= action;
        }
    }
}

