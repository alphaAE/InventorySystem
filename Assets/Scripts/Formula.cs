using System.Linq;

public class Formula {
    public int ResultId { get; private set; }
    public int[][] PatternId { get; private set; }
    private int[] _patternIdUsed = new int[9];

    public Formula(int resultId, int[][] patternId) {
        ResultId = resultId;
        PatternId = patternId;
        for (int i = 0; i < _patternIdUsed.Length; i++) {
            _patternIdUsed[i] = PatternId[i / 3][i % 3];
        }
    }

    public bool Match(int[] ids) {
        return _patternIdUsed.SequenceEqual(ids);
    }
}