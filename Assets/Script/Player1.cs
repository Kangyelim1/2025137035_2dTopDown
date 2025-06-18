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

    // �׾����� ����
    public GameObject gravePrefab; // ����Ƽ �����Ϳ��� ���� �������� ���⿡ �Ҵ��ϼ���.
    public Transform respawnPoint; // (���� ����) �÷��̾ ��Ȱ�� Ư�� ����
    public float respawnDelay = 3f; // ��Ȱ���� ��ٸ� �ð� (��)

    private Vector3 initialPosition; // ���� ���� �� �÷��̾��� �ʱ� ��ġ�� ������ ����
    private GameObject currentGrave; // ������ ���� ������Ʈ ����

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
        initialPosition = transform.position; // �÷��̾��� ���� ��ġ ����

        // respawnPoint�� �������� �ʾҴٸ� initialPosition�� �⺻ ��Ȱ ��ġ�� ���
        if (respawnPoint == null)
        {
            GameObject defaultPoint = new GameObject("DefaultRespawnPoint");
            defaultPoint.transform.position = initialPosition;
            respawnPoint = defaultPoint.transform;
            defaultPoint.transform.SetParent(this.transform.parent); // Hierarchy ������
        }
    }



    public void PlayerDead()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�!");

        Vector3 deathPosition = transform.position; // ���� ��ġ ����

        // 1. �÷��̾� ��Ȱ��ȭ (������, �ð�, �浹)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // �ݶ��̴� ��Ȱ��ȭ (�ٸ� ������Ʈ�� �浹���� ����)
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null) playerCollider.enabled = false;

        // ��������Ʈ ������ ��Ȱ��ȭ (�÷��̾ ������ �ʰ� ��)
        SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = false;

        this.enabled = false; // �� ��ũ��Ʈ ��ü�� ��Ȱ��ȭ�Ͽ� Update() �� ����

        // 2. ���� ��ġ�� ���� ����
        if (gravePrefab != null)
        {
            Vector3 spawnPos = new Vector3(deathPosition.x, deathPosition.y, 0f); // 2D�� ���� Z�� 0���� ����
            currentGrave = Instantiate(gravePrefab, spawnPos, Quaternion.identity);
        }

        // 3. ��Ȱ �ڷ�ƾ ����
        StartCoroutine(RespawnPlayerCoroutine());

        // --- ��Ȱ�� ó���ϴ� �ڷ�ƾ �Լ� ---
        IEnumerator RespawnPlayerCoroutine()
        {
            yield return new WaitForSeconds(respawnDelay); // ������ �ð���ŭ ��ٸ�

            // 1. �÷��̾� ��Ȱ ��ġ�� �̵�
            transform.position = respawnPoint.position;

            // 2. �÷��̾� �ٽ� Ȱ��ȭ (���̰� �ϰ� �浹�ϰ� ��)
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null) playerCollider.enabled = true;

            SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null) playerSpriteRenderer.enabled = true;

            this.enabled = true; // �� ��ũ��Ʈ �ٽ� Ȱ��ȭ

            // 3. ������ ���� ����
            if (currentGrave != null)
            {
                Destroy(currentGrave);
                currentGrave = null; // ���� �ʱ�ȭ
            }

            Debug.Log("�÷��̾ ��Ȱ�߽��ϴ�!");
        }
    }
}