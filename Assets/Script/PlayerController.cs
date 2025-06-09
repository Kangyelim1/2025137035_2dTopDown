using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private bool isGiant = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Finish 지점 도착! 다음 레벨로 이동 시도.");

            LevelObject levelObject = collision.gameObject.GetComponent<LevelObject>();

            // LevelObject 컴포넌트가 있는지 확인합니다.
            if (levelObject != null)
            {
                levelObject.MoveTonextLevel();
                Debug.Log("MoveToNextLevel() 호출 완료. 다음 씬으로 넘어갈 예정.");
            }
            else
            {
                Debug.LogWarning("경고: 'Finish' 태그를 가진 오브젝트에 LevelObject 컴포넌트가 없습니다. 다음 스테이지로 넘어갈 수 없습니다.");
            }
        }
        
    }

    private Rigidbody2D rb;
    private Vector2 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        previousPosition = rb.position;
        // 여기에 플레이어 이동 로직 구현 (예: 키 입력에 따른 velocity 변경)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 레이어가 "BlackTile"인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("BlackTile"))
        {
            Debug.Log("검은색 타일에 부딪혔습니다!");
            // 이동을 멈추는 방법
            rb.velocity = Vector2.zero;
            // 또는 이전 위치로 되돌리는 방법 (FixedUpdate에서 previousPosition을 업데이트해야 함)
            rb.MovePosition(previousPosition);
        }
    }

}

