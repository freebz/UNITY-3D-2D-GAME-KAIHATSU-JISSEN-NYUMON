using UnityEngine;
using System.Collections;
using UnityEngine.UI;                   // UI 네임스페이스의 임포트
using UnityEngine.SceneManagement;      // 5.3

public class GameControll : MonoBehaviour {

    public NejikoController nejiko;
    public Text scoreLabel;             // ScoreLabel 참조
    public LifePanel lifePanel;
    
	// Update is called once per frame
	void Update () {

        // 스코어 레이블을 업데이트
        int score = CalcScore();
        scoreLabel.text = "Score : " + score + "m";     // 텍스트 업데이트

        // 라이프 패널을 업데이트
        lifePanel.UpdateLife(nejiko.Life());            // LifePanel 업데이트

        // 네지코의 라이프가 0이 되면 게임 종료
        if (nejiko.Life() <= 0)
        {
            // 이 이후의 업데이트는 멈춘다.
            enabled = false;

            // 하이 스코어를 업데이트
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            // 2초 후에 ReturnToTitle을 호출
            Invoke("ReturnToTitle", 2.0f);
        }
	}

    int CalcScore ()
    {
        // 네지코의 주행 거리를 스코어로 한다
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle ()
    {
        // 타이틀 씬으로 전환
        //Application.LoadLevel("Title");   // 5.2
        SceneManager.LoadScene("title");    // 5.3
    }
}
