using UnityEngine;
using System.Collections;

public class ClearTrigger : MonoBehaviour {

    GameObject gameController;

	// Use this for initialization
	void Start () {

        // 게임 시작 시에 GameController를 Find해둔다
        gameController = GameObject.FindWithTag("GameController");
    }
	
    // 트리거에서 Exit하면 일단 클리어한 것으로 간주한다
    void OnTriggerExit2D (Collider2D other)
    {
        gameController.SendMessage("IncreaseScore");
    }
}
