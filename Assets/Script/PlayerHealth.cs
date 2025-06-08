using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    private bool isAlive = true;

    // �ٸ� ������Ʈ�� �浹���� �� ȣ��Ǵ� �Լ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �浹�� ������Ʈ�� �±װ� "Enemy"���
        // (����: "Enemy" �±״� ���� ����Ƽ���� �������� �մϴ�. �Ʒ� ����)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ĳ���Ͱ� ���� ����ִٸ�
            if (isAlive)
            {
                Die(); // �״� �Լ� ȣ��
            }
        }
    }

    // ĳ���Ͱ� �׾��� �� ����� �Լ�
    void Die()
    {
        isAlive = false; // �� �̻� ������� �ʴٰ� ǥ��
        Debug.Log("ĳ���Ͱ� �׾����ϴ�!"); // �ֿܼ� �޽��� ���

        // ���⿡ ĳ���͸� ���ӿ��� �����ϰų� (��: Destroy(gameObject);)
        // ���� ���� ȭ���� ���ų�
        // ĳ���͸� ��Ȱ��ȭ�ϴ� ���� �ڵ带 �߰��մϴ�.
        gameObject.SetActive(false); // ����: ĳ���� ������Ʈ�� ��Ȱ��ȭ
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
