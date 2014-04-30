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
	
	/// <summary>
	/// Saves the ID of the player winning current game. If more than one game has been played in the current session, stats showing how many times each player has won will be saved.
	/// </summary>
	/// <param name="player">ID of the winning player</param>
	public static void OnGameWon(int player) {
		lastPlayerWinning = player;
		if (winningStats.ContainsKey(player))
			winningStats[player]++;
		else
			winningStats[player] = 1;
	}
	
}
