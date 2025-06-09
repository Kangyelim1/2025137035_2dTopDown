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

}

