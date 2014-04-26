using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStats {

	/// <summary>
	/// Keeps track of the last player who won a game.
	/// </summary>
	public static int lastPlayerWinning { get; private set;	}
	/// <summary>
	/// Contains the scoreboard for the current session, i.e. how many times each player has won since startup.
	/// </summary>
	private static Dictionary<int, int> winningStats = new Dictionary<int, int>();
	
	//void Awake () {
	//	this.enabled = false;
	//	DontDestroyOnLoad(this.gameObject);
	//}

	public static void OnGameWon(int player) {
		lastPlayerWinning = player;
		if (winningStats.ContainsKey(player))
			winningStats[player]++;
		else
			winningStats[player] = 1;
	}
	
}
