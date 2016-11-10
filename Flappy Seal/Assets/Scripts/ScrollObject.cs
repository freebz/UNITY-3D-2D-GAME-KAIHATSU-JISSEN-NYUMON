using UnityEngine;
using System.Collections;

public class ScrollObject : MonoBehaviour {

    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;
    
	// Update is called once per frame
	void Update () {

        // 매 프레임 x 포지션을 조금씩 이동시킨다
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);     // 스크롤 처리

        // 스크롤이 목표 지점까지 도달했는지 체크
        if (transform.position.x <= endPosition) ScrollEnd();   // 스크롤 종료 판정
	}

    void ScrollEnd ()
    {
        // 스크롤한 거리만큼을 되돌린다
        transform.Translate(-1 * (endPosition - startPosition), 0, 0);  // 순환 처리

        // 동일한 게임 오브젝트에 연결되어 있는 컴포넌트에 메시지를 보낸다
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);     // ScrollEnd의 메시징
    }
}
