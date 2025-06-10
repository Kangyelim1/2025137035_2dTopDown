using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public PlayerController playerController; // Inspector���� �÷��̾� ������Ʈ�� PlayerController ��ũ��Ʈ�� �Ҵ��� ����
    public bool isOpen = false; // ���� �����ִ��� ����
    public Animator doorAnimator; // �� �ִϸ��̼��� ���� Animator ������Ʈ

    private bool playerInRange = false; // �÷��̾ �� ���� �ȿ� �ִ��� Ȯ���ϴ� ����

    // Start is called before the first frame update
    void Start()
    {
        // Animator ������Ʈ�� �Ҵ���� �ʾҴٸ�, ���� ������Ʈ���� ã�� �Ҵ�
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }

    // �÷��̾ Ʈ���� �ݶ��̴��� �������� ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("�÷��̾ �� ������ �����߽��ϴ�.");
            // E Ű�� ���� ��ȣ�ۿ��ϼ���! ���� UI �޽����� ǥ���� �� �ֽ��ϴ�.
        }
    }

    // �÷��̾ Ʈ���� �ݶ��̴����� ����� ��
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("�÷��̾ �� �������� ������ϴ�.");
            // UI �޽����� ���� �� �ֽ��ϴ�.
        }
    }


    // Update is called once per frame
    void Update()
    {
        // �÷��̾ �� ���� �ȿ� �ְ�, E Ű�� ��������, ���� �����ִٸ�
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            if (playerController != null && playerController.hasKey)
            {
                // �÷��̾ ���踦 ������ �ִٸ� ���� ���ϴ�.
                OpenDoor();
                playerController.hasKey = false; // ���踦 ��������Ƿ� �ٽ� false�� ����
                Debug.Log("���踦 ����Ͽ� ���� �������ϴ�!");
            }
            else
            {
                Debug.Log("���� ��� �ֽ��ϴ�. ���谡 �ʿ��մϴ�!");
                // ���� ���ٴ� UI �޽����� �Ҹ��� ����� �� �ֽ��ϴ�.
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true; // �� ���¸� �������� ����

        if (doorAnimator != null)
        {
            // �� ���� �ִϸ��̼� Ʈ����
            // Animator�� "Open"�̶�� Trigger �Ķ���͸� ������ �մϴ�.
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // �ִϸ��̼��� ���ٸ� �� ������Ʈ�� ��Ȱ��ȭ�ϰų� �̵����Ѽ� ���� ��ó�� ���̰� �մϴ�.
            // ���� ���, Destroy(gameObject); �Ǵ� transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            // 2D ������ ���, ��������Ʈ�� �����ϰų� SetActive(false)�� ������� �� �� �ֽ��ϴ�.
            gameObject.SetActive(false); // ���� �ܼ��� ������� �Ϸ��� (�ִϸ��̼��� ���� ���)
        }
    }
}
