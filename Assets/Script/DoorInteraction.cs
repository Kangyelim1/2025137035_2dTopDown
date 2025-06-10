using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public PlayerController playerController; // Inspector에서 플레이어 오브젝트의 PlayerController 스크립트를 할당할 변수
    public bool isOpen = false; // 문이 열려있는지 여부
    public Animator doorAnimator; // 문 애니메이션을 위한 Animator 컴포넌트

    private bool playerInRange = false; // 플레이어가 문 범위 안에 있는지 확인하는 변수

    // Start is called before the first frame update
    void Start()
    {
        // Animator 컴포넌트가 할당되지 않았다면, 현재 오브젝트에서 찾아 할당
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }

    // 플레이어가 트리거 콜라이더에 진입했을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("플레이어가 문 범위에 진입했습니다.");
            // E 키를 눌러 상호작용하세요! 같은 UI 메시지를 표시할 수 있습니다.
        }
    }

    // 플레이어가 트리거 콜라이더에서 벗어났을 때
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("플레이어가 문 범위에서 벗어났습니다.");
            // UI 메시지를 숨길 수 있습니다.
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 플레이어가 문 범위 안에 있고, E 키를 눌렀으며, 문이 닫혀있다면
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            if (playerController != null && playerController.hasKey)
            {
                // 플레이어가 열쇠를 가지고 있다면 문을 엽니다.
                OpenDoor();
                playerController.hasKey = false; // 열쇠를 사용했으므로 다시 false로 설정
                Debug.Log("열쇠를 사용하여 문을 열었습니다!");
            }
            else
            {
                Debug.Log("문이 잠겨 있습니다. 열쇠가 필요합니다!");
                // 문이 잠겼다는 UI 메시지나 소리를 재생할 수 있습니다.
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true; // 문 상태를 열림으로 변경

        if (doorAnimator != null)
        {
            // 문 열림 애니메이션 트리거
            // Animator에 "Open"이라는 Trigger 파라미터를 만들어야 합니다.
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // 애니메이션이 없다면 문 오브젝트를 비활성화하거나 이동시켜서 열린 것처럼 보이게 합니다.
            // 예를 들어, Destroy(gameObject); 또는 transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            // 2D 게임의 경우, 스프라이트를 변경하거나 SetActive(false)로 사라지게 할 수 있습니다.
            gameObject.SetActive(false); // 문을 단순히 사라지게 하려면 (애니메이션이 없는 경우)
        }
    }
}
