using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject keyObject; // Inspector���� ���� GameObject �Ҵ�
    public Sprite openedChestSprite; // (���� ����) ���� ���� ��������Ʈ �Ҵ�

    private bool playerInRange = false;
    private bool chestOpened = false;
    private SpriteRenderer chestRenderer;

    // Start is called before the first frame update
    void Start()
    {
        chestRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !chestOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    // �÷��̾ ���ڿ� �������� ��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("'E' Ű�� ��ȣ�ۿ� ����");
        }
    }

    void OpenChest()
    {
        chestOpened = true; // ���ڸ� ���� ���·� ����

        // ���� ���� ��������Ʈ�� �Ҵ�Ǿ� �ִٸ� ��������Ʈ ����
        if (openedChestSprite != null)
        {
            chestRenderer.sprite = openedChestSprite;
        }

        // ���� ������Ʈ�� �Ҵ�Ǿ� �ִٸ�
        if (keyObject != null)
        {
            keyObject.SetActive(true); // ���� ������Ʈ�� Ȱ��ȭ�Ͽ� ���̰� �մϴ�.
            Debug.Log("���� ȹ��!"); // ����� �޽��� ���
            // ���踦 �κ��丮�� �߰��ϰ� �ʹٸ� ���⿡ �߰� ������ �����մϴ�.
            // keyObject.SetActive(false); // (���� ����) ���踦 ��� �ٽ� �������
            // Destroy(keyObject, 0.5f); // (���� ����) 0.5�� �� ���踦 �ı��Ϸ���
        }
    }
}
