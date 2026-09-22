namespace ConnectFour;

public class GameState
{
	static GameState()
	{
		CalculateWinningPlaces();
	}

	public enum WinState
	{
		No_Winner = 0,
		Player1_Wins = 1,
		Player2_Wins = 2,
		Tie = 3
	}

	public int PlayerTurn => TheBoard.Count(x => x != 0) % 2 + 1;

	public int CurrentTurn => TheBoard.Count(x => x != 0);

	public static readonly List<int[]> WinningPlaces = new();

	public int Player1Wins { get; private set; }
	public int Player2Wins { get; private set; }
	public int Ties { get; private set; }
	public int CurrentStreakPlayer { get; private set; }
	public int CurrentStreakCount { get; private set; }
	public bool ScoreRecordedForCurrentGame { get; private set; }

	public static void CalculateWinningPlaces()
	{
		// Horizontal rows
		for (byte row = 0; row < 6; row++)
		{
			byte rowCol1 = (byte)(row * 7);
			byte rowColEnd = (byte)((row + 1) * 7 - 1);
			byte checkCol = rowCol1;
			while (checkCol <= rowColEnd - 3)
			{
				WinningPlaces.Add(new int[] {
					checkCol,
					checkCol + 1,
					checkCol + 2,
					checkCol + 3
				});
				checkCol++;
			}
		}

		// Vertical columns
		for (byte col = 0; col < 7; col++)
		{
			byte colRow1 = col;
			byte checkRow = colRow1;
			while (checkRow <= 14 + col)
			{
				WinningPlaces.Add(new int[] {
					checkRow,
					checkRow + 7,
					checkRow + 14,
					checkRow + 21
				});
				checkRow += 7;
			}
		}

		// Diagonal "/"
		for (byte col = 0; col < 4; col++)
		{
			byte colRow1 = (byte)(21 + col);
			byte colRowEnd = (byte)(35 + col);
			byte checkPos = colRow1;
			while (checkPos <= colRowEnd)
			{
				WinningPlaces.Add(new int[] {
					checkPos,
					checkPos - 6,
					checkPos - 12,
					checkPos - 18
				});
				checkPos += 7;
			}
		}

		// Diagonal "\"
		for (byte col = 0; col < 4; col++)
		{
			byte colRow1 = (byte)(0 + col);
			byte colRowEnd = (byte)(14 + col);
			byte checkPos = colRow1;
			while (checkPos <= colRowEnd)
			{
				WinningPlaces.Add(new int[] {
					checkPos,
					checkPos + 8,
					checkPos + 16,
					checkPos + 24
				});
				checkPos += 7;
			}
		}
	}

	public WinState CheckForWin()
	{
		if (TheBoard.Count(x => x != 0) < 7) return WinState.No_Winner;

		foreach (var scenario in WinningPlaces)
		{
			if (TheBoard[scenario[0]] == 0) continue;

			if (TheBoard[scenario[0]] == TheBoard[scenario[1]] &&
				TheBoard[scenario[1]] == TheBoard[scenario[2]] &&
				TheBoard[scenario[2]] == TheBoard[scenario[3]])
			{
				return (WinState)TheBoard[scenario[0]];
			}
		}

		if (TheBoard.Count(x => x != 0) == 42) return WinState.Tie;

		return WinState.No_Winner;
	}

	public byte PlayPiece(int column)
	{
		if (CheckForWin() != WinState.No_Winner) throw new ArgumentException("Game is over");
		if (column < 0 || column > 6) throw new ArgumentOutOfRangeException(nameof(column), "Column must be between 0 and 6");
		if (TheBoard[column] != 0) throw new ArgumentException("Column is full");

		var landingSpot = column;
		for (var i = column; i < 42; i += 7)
		{
			if (TheBoard[landingSpot + 7] != 0) break;
			landingSpot = i;
		}

		TheBoard[landingSpot] = PlayerTurn;

		var winState = CheckForWin();
		if (winState != WinState.No_Winner && !ScoreRecordedForCurrentGame)
		{
			RecordScore(winState);
		}

		return ConvertLandingSpotToRow(landingSpot);
	}

	private void RecordScore(WinState winState)
	{
		ScoreRecordedForCurrentGame = true;
		if (winState == WinState.Player1_Wins)
		{
			Player1Wins++;
			if (CurrentStreakPlayer == 1)
			{
				CurrentStreakCount++;
			}
			else
			{
				CurrentStreakPlayer = 1;
				CurrentStreakCount = 1;
			}
		}
		else if (winState == WinState.Player2_Wins)
		{
			Player2Wins++;
			if (CurrentStreakPlayer == 2)
			{
				CurrentStreakCount++;
			}
			else
			{
				CurrentStreakPlayer = 2;
				CurrentStreakCount = 1;
			}
		}
		else if (winState == WinState.Tie)
		{
			Ties++;
			CurrentStreakPlayer = 0;
			CurrentStreakCount = 0;
		}
	}

	public List<int> TheBoard { get; private set; } = new(new int[42]);

	public void ResetBoard()
	{
		TheBoard = new List<int>(new int[42]);
		ScoreRecordedForCurrentGame = false;
	}

	public void ResetScoreboard()
	{
		Player1Wins = 0;
		Player2Wins = 0;
		Ties = 0;
		CurrentStreakPlayer = 0;
		CurrentStreakCount = 0;
	}

	private static byte ConvertLandingSpotToRow(int landingSpot)
	{
		return (byte)(Math.Floor(landingSpot / (decimal)7) + 1);
	}
}
