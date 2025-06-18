using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject keyObject; // Inspector에서 열쇠 GameObject 할당
    public Sprite openedChestSprite; // (선택 사항) 열린 상자 스프라이트 할당

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

    // 플레이어가 상자에 접근했을 때
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("'E' 키로 상호작용 가능");
        }
    }

    void OpenChest()
    {
        chestOpened = true; // 상자를 열린 상태로 설정

        // 열린 상자 스프라이트가 할당되어 있다면 스프라이트 변경
        if (openedChestSprite != null)
        {
            chestRenderer.sprite = openedChestSprite;
        }

        // 열쇠 오브젝트가 할당되어 있다면
        if (keyObject != null)
        {
            keyObject.SetActive(true); // 열쇠 오브젝트를 활성화하여 보이게 합니다.
            Debug.Log("열쇠 획득!"); // 디버그 메시지 출력
            // 열쇠를 인벤토리에 추가하고 싶다면 여기에 추가 로직을 구현합니다.
            // keyObject.SetActive(false); // (선택 사항) 열쇠를 즉시 다시 숨기려면
            // Destroy(keyObject, 0.5f); // (선택 사항) 0.5초 후 열쇠를 파괴하려면
        }
    }
}
