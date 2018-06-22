using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Moving : MonoBehaviour
    {
        public float acceleration = 5f;
        public float rotationSpeed = 3f;
        public float maxVelocity = 5f;

        private Rigidbody2D rigid;
        // Member variables

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            //fMovement(Input.GetAxis("Vertical"));
            //fRotation(-Input.GetAxis("Horizontal"));
            LimitVelocity();
        }

        //void fMovement(float yVal)
        //{
        //    transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * yVal);
        //}

        //void fRotation(float xVal)
        //{
        //    transform.rotation *= Quaternion.AngleAxis(rotateSpeed * xVal, Vector3.forward);
        //}
        void LimitVelocity()
        {
            Vector3 vel = rigid.velocity;
            if (vel.magnitude > maxVelocity)
            {
                vel = vel.normalized * maxVelocity;
            }

            rigid.velocity = vel;
        }
        public void Accelerate(Vector3 direction)
        {
            rigid.AddForce(direction * acceleration);            
        }
        public void RotateLeft()
        {
            rigid.rotation += rotationSpeed * Time.deltaTime;
        }
        public void RotateRight()
        {
            rigid.rotation -= rotationSpeed * Time.deltaTime;
        }
    }
}