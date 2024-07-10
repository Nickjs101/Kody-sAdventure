using UnityEngine;
[System.Serializable]

public class OGQuestion
{
    [TextArea(3, 10)]
    public string Question;
    public string Answer;
    [TextArea(3, 10)]
    public string Input;
    public string[] Options;
    
}
