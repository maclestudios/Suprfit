using UnityEngine;

[CreateAssetMenu(menuName ="Difficulty Type")]
public class DifficultyObject : ScriptableObject
{
    public DifficultyType difficultyType;
    public string discription;
    public int numberOfExercise;
    public float holdTime;
    public float inBetweenTime;
    public float repeatCount;

    [Header("Individual Mode")]
    public bool individualMode;
    public ExerciseObject individualExercise;
}


[System.Serializable]
public enum DifficultyType
{
    Beginner,
    Intermidiate,
    Hardcore
}
