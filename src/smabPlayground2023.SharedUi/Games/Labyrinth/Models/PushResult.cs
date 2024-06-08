namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public record PushResult(string Word, int Score, PushResultReason Reason)
{
	public static implicit operator (string word, int score, PushResultReason reason)(PushResult value)
		=> (value.Word, value.Score, value.Reason);

	public static implicit operator PushResult((string word, int score, PushResultReason reason) value)
		=> new(value.word, value.score, value.reason);
}

public enum PushResultReason
{
	Success = 0,
	InvalidColumnOrRow,
	Blocked,
}
