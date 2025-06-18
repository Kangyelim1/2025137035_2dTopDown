using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
    public float damageAmount = 100f;
    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ�� (�÷��̾�� "Player" �±װ� �־�� ��)
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� �������� �ִ� �Լ� ȣ�� (�� �κ��� �÷��̾��� ü�� �ý��ۿ� ���� �ٸ�)
            DamagePlayer(other.gameObject);
        }
    }

    void DamagePlayer(GameObject player)
    {
        // �÷��̾�� �������� �ִ� ������ ���⿡ �����մϴ�.
        // ���� ���, �÷��̾� ��ũ��Ʈ���� ü���� ���ҽ�Ű�� �Լ��� ȣ���� �� �ֽ��ϴ�.
        // ����: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

        Debug.Log("��¥ ���� ��ҽ��ϴ�! �÷��̾�� �������� �ݴϴ�.");
        player.GetComponent<Player1>().PlayerDead();
        // (���� ����) ��¥ ���� ������� �ϰų�, �ٸ� �ð� ȿ���� �� �� �ֽ��ϴ�.
        // Destroy(player); // ��¥ ���� �ı��մϴ�.
    }
}
