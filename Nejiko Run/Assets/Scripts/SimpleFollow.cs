using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

    Vector3 diff;

    public GameObject target;   // 추적 대상 파라미터
    public float followSpeed;

	// Use this for initialization
	void Start () {
        diff = target.transform.position - transform.position;  // 추적 거리 계산
	}
	
	void LateUpdate () {
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed        // 선형 보간 함수에 의한 유연한 움직임
        );      
	}
}
