using UnityEngine;

[CreateAssetMenu(fileName ="AnimationSequence")]
public class AnimationSequenceObject : ScriptableObject
{
    public string exName;
    public string englistName;
    [Space]
    public RuntimeAnimatorController runtimeAnimatorController;
    [Space]
    public string[] exSteps;
    [Space]
    public string[] exStepAnimNames;
}
