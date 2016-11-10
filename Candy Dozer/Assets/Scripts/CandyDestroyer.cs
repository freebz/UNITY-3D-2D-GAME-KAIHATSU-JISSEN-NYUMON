using UnityEngine;
using System.Collections;

public class CandyDestroyer : MonoBehaviour {

    public CandyHolder candyHolder;
    public int reward;
    public GameObject effectPrefab;     // 효과 프리팹 파라미터
    public Vector3 effectRotation;      // 효과 로테이션 파라미터

	void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            // 지정한 수만큼만 Candy의 투척 기회 수를 늘린다
            candyHolder.AddCandy(reward);

            // 오브젝트 삭제
            Destroy(other.gameObject);

            if (effectPrefab != null)   // 효과 프리팹의 설정 체크
            {
                // Candy의 포지션에 효과를 생성
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation)
                    );
            }
        }
    }
}
