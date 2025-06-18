using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private bool isGiant = false;


    public bool hasKey = false; // 플레이어가 열쇠를 가지고 있는지 여부\

    public float moveSpeed = 5f;

    float score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (collision.gameObject.CompareTag("Finish"))
        {
            //HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
            StageResultSaver.SaveStage(SceneManager.GetActiveScene().buildIndex, (int)score);
            collision.GetComponent<LevelObject>().MoveTonextLevel();
        }

    }



    private Rigidbody2D rb;
    private Vector2 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        hasKey = false;

    }

    void FixedUpdate()
    {
        previousPosition = rb.position;
        // 여기에 플레이어 이동 로직 구현 (예: 키 입력에 따른 velocity 변경)
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * moveSpeed;
    }
}

