using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	void Start () {
		TextMesh winner = GameObject.Find("WinningPlayerText").GetComponent<TextMesh>();
		winner.text = "Player " + GameStats.lastPlayerWinning + " has won!";

		TextMesh p1Score = GameObject.Find("Player1Score").GetComponent<TextMesh>();
		Debug.Log(p1Score.text);
		p1Score.text += (int)Random.Range(0.0f, 99.9f);

		TextMesh p2Score = GameObject.Find("Player2Score").GetComponent<TextMesh>();
		p2Score.text += (int)Random.Range(0.0f, 99.9f);

		this.enabled = false;
	}
}
