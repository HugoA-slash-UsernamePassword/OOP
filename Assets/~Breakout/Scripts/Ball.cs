using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        public float speed = 5f;
        private Vector3 velocity;
        private Collider2D cold;
        private int count = 0;
        // Use this for initialization
        public void Fire(Vector3 direction)
        {
            velocity = direction * speed;
            cold = gameObject.GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            velocity = reflect.normalized * velocity.magnitude;
            if(collision.collider.tag == "block")
            {
                Paddle.score++;
                Destroy(collision.gameObject);
            }
        }
        private void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}