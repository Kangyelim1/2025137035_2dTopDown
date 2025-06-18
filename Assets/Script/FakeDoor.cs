using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
    public float damageAmount = 100f;
    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인 (플레이어에게 "Player" 태그가 있어야 함)
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 데미지를 주는 함수 호출 (이 부분은 플레이어의 체력 시스템에 따라 다름)
            DamagePlayer(other.gameObject);
        }
    }

    void DamagePlayer(GameObject player)
    {
        // 플레이어에게 데미지를 주는 로직을 여기에 구현합니다.
        // 예를 들어, 플레이어 스크립트에서 체력을 감소시키는 함수를 호출할 수 있습니다.
        // 예시: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

        Debug.Log("가짜 문에 닿았습니다! 플레이어에게 데미지를 줍니다.");
        player.GetComponent<Player1>().PlayerDead();
        // (선택 사항) 가짜 문이 사라지게 하거나, 다른 시각 효과를 줄 수 있습니다.
        // Destroy(player); // 가짜 문을 파괴합니다.
    }
}
