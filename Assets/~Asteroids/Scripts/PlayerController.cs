using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{
    public class PlayerController : MonoBehaviour
    {
        public Moving movement;

        // Update is called once per frame
        void Update()
        {
            float inputV = Input.GetAxis("Vertical");
            float inputH = Input.GetAxis("Horizontal");
            if (inputV > 0)
            {
                movement.Accelerate(transform.up);
            }
            if (inputH < 0)
            {
                movement.RotateLeft();
            }
            if (inputH > 0)
            {
                movement.RotateRight();
            }
        }
    }
}