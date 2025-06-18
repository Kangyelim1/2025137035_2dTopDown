using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    float moveSpeed = 2f;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    int score;
    

    public TextMeshProUGUI scoreText;

    // 죽었을때 변수
    public GameObject gravePrefab; // 유니티 에디터에서 무덤 프리팹을 여기에 할당하세요.
    public Transform respawnPoint; // (선택 사항) 플레이어가 부활할 특정 지점
    public float respawnDelay = 3f; // 부활까지 기다릴 시간 (초)

    private Vector3 initialPosition; // 게임 시작 시 플레이어의 초기 위치를 저장할 변수
    private GameObject currentGrave; // 생성된 무덤 오브젝트 참조

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > .01f)
        {
            if (input.x > 0)
                sR.sprite = spriteRight;
            else if (input.x < 0)
                sR.sprite = spriteLeft;
        }
        else
        {
            if (input.y > 0)
                sR.sprite = spriteUp;
            else
                sR.sprite = spriteDown;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Item"))
        {
            score += collision.GetComponent<ItemObject>().GetPoint();
            Destroy(collision.gameObject);
            scoreText.text = score.ToString();

            
        }
    }

    void Start()
    {
        initialPosition = transform.position; // 플레이어의 시작 위치 저장

        // respawnPoint가 설정되지 않았다면 initialPosition을 기본 부활 위치로 사용
        if (respawnPoint == null)
        {
            GameObject defaultPoint = new GameObject("DefaultRespawnPoint");
            defaultPoint.transform.position = initialPosition;
            respawnPoint = defaultPoint.transform;
            defaultPoint.transform.SetParent(this.transform.parent); // Hierarchy 정리용
        }
    }



    public void PlayerDead()
    {
        Debug.Log("플레이어가 사망했습니다!");

        Vector3 deathPosition = transform.position; // 죽은 위치 저장

        // 1. 플레이어 비활성화 (움직임, 시각, 충돌)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // 콜라이더 비활성화 (다른 오브젝트와 충돌하지 않음)
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null) playerCollider.enabled = false;

        // 스프라이트 렌더러 비활성화 (플레이어가 보이지 않게 함)
        SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = false;

        this.enabled = false; // 이 스크립트 자체를 비활성화하여 Update() 등 정지

        // 2. 죽은 위치에 무덤 생성
        if (gravePrefab != null)
        {
            Vector3 spawnPos = new Vector3(deathPosition.x, deathPosition.y, 0f); // 2D를 위해 Z는 0으로 고정
            currentGrave = Instantiate(gravePrefab, spawnPos, Quaternion.identity);
        }

        // 3. 부활 코루틴 시작
        StartCoroutine(RespawnPlayerCoroutine());

        // --- 부활을 처리하는 코루틴 함수 ---
        IEnumerator RespawnPlayerCoroutine()
        {
            yield return new WaitForSeconds(respawnDelay); // 지정된 시간만큼 기다림

            // 1. 플레이어 부활 위치로 이동
            transform.position = respawnPoint.position;

            // 2. 플레이어 다시 활성화 (보이게 하고 충돌하게 함)
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null) playerCollider.enabled = true;

            SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = true;

            this.enabled = true; // 이 스크립트 다시 활성화

            // 3. 생성된 무덤 제거
            if (currentGrave != null)
            {
                Destroy(currentGrave);
                currentGrave = null; // 참조 초기화
            }

            Debug.Log("플레이어가 부활했습니다!");
        }
    }
}