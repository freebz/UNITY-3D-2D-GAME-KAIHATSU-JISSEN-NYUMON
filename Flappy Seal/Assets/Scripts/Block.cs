using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public float minHeight;
    public float maxHeight;     // 틈새 높이의 범위
    public GameObject pivot;    // Pivot 오브젝트

	// Use this for initialization
	void Start () {

        // 시작할 때 틈새의 높이를 변경
        ChangeHeight();         // 틈새 초기화
	}
	
    void ChangeHeight ()
    {
        // 임의의 높이를 생성하고 설정
        float height = Random.Range(minHeight, maxHeight);
        pivot.transform.localPosition = new Vector3(0.0f, height, 0.0f);    // 높이 변경
    }

    // ScrollObject 스크립트로부터의 메시지를 받아 높이를 변경
    void OnScrollEnd ()
    {
        ChangeHeight();     // 스크롤 완료 시의 틈새 재구성
    }
}
