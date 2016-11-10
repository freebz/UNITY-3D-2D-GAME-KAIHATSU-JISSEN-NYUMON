﻿using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    const int SphereCandyFrequency = 3;
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;  // 슛 파워의 회복 시간 상수

    int sampleCandyCount;
    int shotPower = MaxShotPower;
    AudioSource shotSound;

    public GameObject[] candyPrefabs;  // Candy 프리팹 파라미터
    public GameObject[] candySquarePrefabs;
    public CandyHolder candyHolder;
    public float shotSpeed;
    public float shotTorque;
    public float baseWidth;
    
    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) Shot();   // 입력 감지
	}

    GameObject SampleCandy ()
    {
        GameObject prefab = null;

        // 특정 주기로 한 번씩 둥근 사탕을 선택한다
        if (sampleCandyCount % SphereCandyFrequency == 0)
        {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];
        } else
        {
            int index = Random.Range(0, candySquarePrefabs.Length);
            prefab = candySquarePrefabs[index];
        }

        sampleCandyCount++;

        return prefab;
    }

    Vector3 GetInstantiatePosition ()
    {
        // 화면의 사이즈와 Input의 비율로부터 사탕 생성의 포지션을 계산
        float x = baseWidth *
            (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot ()
    {
        // 사탕을 생성할 수 있는 조건을 벗어나면 Shot하지 않는다
        if (candyHolder.GetCandyAmount() <= 0) return;
        if (shotPower <= 0) return;     // 슛 파워의 체크

        // 프리팹에서 Candy 오브젝트를 생성
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
            );  // 오브젝트 생성

        // 생성한 Candy 오브젝트의 부모를 CandyHolder에 설정한다
        candy.transform.parent = candyHolder.transform;     // 부모 오브젝트 설정

        // Candy 오브젝트의 Rigidbody를 취득하여 힘과 회전을 더한다
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotSpeed); // 힘과 토크 계산
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        // Candy의 던질 횟수를 소비
        candyHolder.ConsumeCandy();
        // ShotPower를 소비
        ConsumePower();     // 슛 파워의 소비 실행

        // 사운드를 재생
        shotSound.Play();
    }

    void OnGUI ()
    {
        GUI.color = Color.black;

        // ShotPower의 남은 수를 +의 수로 표시
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";

        GUI.Label(new Rect(0, 15, 100, 30), label);
    }

    void ConsumePower()
    {
        // ShotPower를 소비하면 동시에 회복 카운터를 스타터
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower ()
    {
        // 일정 시간(초 단위)을 기다린 후에 shotPower를 회복
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
