using UnityEngine;
using System.Collections;

public class SealController : MonoBehaviour {

    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;           // Sprite 오브젝트 참조

    public bool IsDead ()
    {
        return isDead;
    }

    void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Awake 함수에 의한 컴포넌트의 취득
        animator = sprite.GetComponent<Animator>();     // Animator 컴포넌트 취득
    }
    
	// Update is called once per frame
	void Update () {
	    
        // 최고 고도에 도달하지 않은 경우에만 탭의 입력을 받는다
        if(Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)    // 입력 판정
        {
            Flap();
        }

        // 각도를 반영
        ApplyAngle();

        // angle이 수평 이상이라면 애니메이터의 flag플래그를 true로 한다
        animator.SetBool("flap", angle >= 0.0f);    // 애니메이션 스테이트 제어
	}

    public void Flap ()
    {
        // 죽으면 날아 올라가지 않는다
        if (isDead) return;             // isDead 플레그에 의한 Flap의 억제

        // 중력을 받지 않을 때는 조작하지 않는다
        if (rb2d.isKinematic) return;

        // Velocity를 직접 바꿔 써서 위쪽 방향으로 가속
        rb2d.velocity = new Vector2(0.0f, flapVelocity);    // 속도 조작
    }

    void ApplyAngle ()
    {
        // 현재 속도, 상대 속도로부터 진행되고 있는 각도를 구한다
        float targetAngle;

        // 사망하면 항상 아래를 향한다
        if (isDead)
        {
            targetAngle = -90.0f;   // isDead 플래그에 의한 회전 제어
        }
        else
        {
            targetAngle =
                    Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;    // 벡터로부터 각도 계산
        }
        
        // 회전 애니메이션을 스무딩
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);     // 스무딩한 각도의 반영

        // Rotation의 반영
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (isDead) return;

        // 충돌 효과
        Camera.main.SendMessage("Clash");

        // 뭔가에 부딪치면 사망 플래그를 true로 한다
        isDead = true;          // 충돌에 의한 사망 판정
    }

    public void SetSteerActive (bool active)
    {
        // Rigidbody의 On, Off를 전환한다
        rb2d.isKinematic = !active;
    }
}
