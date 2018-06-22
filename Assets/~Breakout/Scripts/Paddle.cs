using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public Ball[] currentBall;
        // Use this for initialization
        public Vector2[] directions = new Vector2[]
        {
            new Vector2(-0.5f, 0.5f),
            new Vector2(0.5f, 0.5f)
        };
        public static int score = 0;
        private Rigidbody2D rb;
        // Update is called once per frame
        void Start()
        {
            currentBall = GetComponentsInChildren<Ball>();
            rb = GetComponent<Rigidbody2D>();
        }
        void Fire()
        {
            foreach (var item in currentBall)
            {
                item.transform.SetParent(null);
                Vector3 randomDir = directions[Random.Range(0, directions.Length)];
                item.Fire(randomDir);
            }
        }
        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && transform.GetChild(0) != null)
            {
                Fire();
            }
        }
        void Movement()
        {
            float inputH = Input.GetAxis("Horizontal");
            Vector3 force = transform.right * inputH;
            force *= movementSpeed;
            rb.velocity = force;
        }
        private void Update()
        {
            CheckInput();
            Movement();
        }
        void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 300, 300), "Score: " + score);
        }
    }
}