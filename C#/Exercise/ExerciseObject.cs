using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Exercise Object")]
public class ExerciseObject : ScriptableObject
{
    public string exerciseName;
    public float holdTime;
    public string[] exerciseSteps;
    public string[] exerciseStepAnimNames;
}
