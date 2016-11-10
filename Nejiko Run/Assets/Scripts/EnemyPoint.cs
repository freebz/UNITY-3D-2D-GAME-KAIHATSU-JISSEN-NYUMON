using UnityEngine;
using System.Collections;

public class EnemyPoint : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        // 프리팹을 동일 위치에 생성
        GameObject go = (GameObject)Instantiate(
            prefab,
            Vector3.zero,
            Quaternion.identity
        );                          // 적 오브젝트 생성

        // 함께 삭제되도록 생성한 적 오브젝트를 자식으로 설정
        go.transform.SetParent(transform, false);   // 부모-자식 관계 설정
	}
	
    // 스테이지 편집 중이므로 씬에 기즈모를 표시
    void OnDrawGizmos ()
    {
        // 기즈모의 아랫부분이 지면과 같은 높이가 되도록 오프셋을 설정
        Vector3 offset = new Vector3(0, 0.5f, 0);

        // 공을 표시
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.5f);

        // 프리팹명의 아이콘을 표시
        if (prefab != null)
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);    // 씬 기즈모 표시
    }
}
