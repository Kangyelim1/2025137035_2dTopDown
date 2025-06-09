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
            Debug.Log("Finish ���� ����! ���� ������ �̵� �õ�.");

            LevelObject levelObject = collision.gameObject.GetComponent<LevelObject>();

            // LevelObject ������Ʈ�� �ִ��� Ȯ���մϴ�.
            if (levelObject != null)
            {
                levelObject.MoveTonextLevel();
                Debug.Log("MoveToNextLevel() ȣ�� �Ϸ�. ���� ������ �Ѿ ����.");
            }
            else
            {
                Debug.LogWarning("���: 'Finish' �±׸� ���� ������Ʈ�� LevelObject ������Ʈ�� �����ϴ�. ���� ���������� �Ѿ �� �����ϴ�.");
            }
        }
        
    }

}

