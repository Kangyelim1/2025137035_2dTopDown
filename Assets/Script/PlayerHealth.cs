using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    private bool isAlive = true;

    // 다른 오브젝트와 충돌했을 때 호출되는 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 만약 충돌한 오브젝트의 태그가 "Enemy"라면
        // (주의: "Enemy" 태그는 직접 유니티에서 만들어줘야 합니다. 아래 설명)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 캐릭터가 아직 살아있다면
            if (isAlive)
            {
                Die(); // 죽는 함수 호출
            }
        }
    }

    // 캐릭터가 죽었을 때 실행될 함수
    void Die()
    {
        isAlive = false; // 더 이상 살아있지 않다고 표시
        Debug.Log("캐릭터가 죽었습니다!"); // 콘솔에 메시지 출력

        // 여기에 캐릭터를 게임에서 제거하거나 (예: Destroy(gameObject);)
        // 게임 오버 화면을 띄우거나
        // 캐릭터를 비활성화하는 등의 코드를 추가합니다.
        gameObject.SetActive(false); // 예시: 캐릭터 오브젝트를 비활성화
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveTonextLevel();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
