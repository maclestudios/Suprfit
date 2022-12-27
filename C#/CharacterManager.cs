using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CharacterManager : MonoBehaviour
{
    [SerializeField] string voiceLanguage;
    [SerializeField] ExerciseObject[] exerciseObjects;
    [SerializeField] GameObject maleCharacter;
    [SerializeField] GameObject femaleCharacter;


    Animator animator;
    DifficultyObject difficultyObject;
    bool startExercise;
    int exerciseIndex;

    int currentExerciseStep;
    int currentRepeatCount = 1;
    int exerciseSteps;
    int prevExIndex = -1;
    int numberOfExerciseDone = 0;
    int numberOfEerciseInQueue;
    float holdTimer;
    float handWaveTimer = 20;

    public static CharacterManager characterManager;

    private void Awake()
    {
        characterManager = this;
      
    }
    private void OnDestroy()
    {
        characterManager = null;
    }


    public void SetupCharacter(bool male)
    {
        if (male)
        {
            animator = maleCharacter.GetComponent<Animator>();
            maleCharacter.SetActive(true);
        }
        else
        {
            animator = femaleCharacter.GetComponent<Animator>();
            femaleCharacter.SetActive(true);
        }
    }

    private void Update()
    {
        if (!startExercise)
        {
            if (handWaveTimer <= 0)
            {
                handWaveTimer = 20;
                animator?.Play("wave", -1, 0f);
            }
            else
            {
                handWaveTimer -= Time.deltaTime;
            }
        }
    }

    public void ResetMe()
    {
        animator.Play("Idle");
        startExercise = false;
    }

    public void SetupCharacterExerciseMode(DifficultyObject difficultyObject)
    {
        this.difficultyObject = difficultyObject;
        currentRepeatCount = 1;
        currentExerciseStep = 0;
        numberOfExerciseDone = 0;
        holdTimer = difficultyObject.holdTime;
        numberOfEerciseInQueue = difficultyObject.numberOfExercise;
        GetRandomExercise();
        ExcersiseDashboard.dashboard.exLeftText.text = $"Exercise Round Done : {numberOfExerciseDone}/{numberOfEerciseInQueue}";
        PlayExercise();
    }

    private void GetRandomExercise()
    {
        if (difficultyObject.individualMode)
        {
            for (int i = 0; i < exerciseObjects.Length; i++)
            {
                if (exerciseObjects[i].exerciseName.Equals(difficultyObject.individualExercise.exerciseName))
                    exerciseIndex = i;
            }
        }
        else
        {
            exerciseIndex = Random.Range(0, exerciseObjects.Length);
            if (exerciseIndex == prevExIndex)
            {
                exerciseIndex++;
                if (exerciseIndex == exerciseObjects.Length)
                    exerciseIndex = 0;
            }
            prevExIndex = exerciseIndex;
        }
    }

    private void PlayExercise()
    {
        var exercise = exerciseObjects[exerciseIndex];
        exerciseSteps = exercise.exerciseSteps.Length;

        //UI Update
        ExcersiseDashboard.dashboard.infoPanelText.text = exercise.exerciseSteps[currentExerciseStep];
        ExcersiseDashboard.dashboard.exNameText.text = exercise.exerciseName;
        ExcersiseDashboard.dashboard.exRepeatTimeText.text = $"{currentRepeatCount}";

        //anim
        animator.Play(exerciseObjects[exerciseIndex].exerciseStepAnimNames[currentExerciseStep]);
        StartCoroutine(AnimWait());
        startExercise = true;
    }

    IEnumerator AnimWait()
    {
        yield return new WaitForSeconds(5);
        OnStepUpdate();
        StopCoroutine(AnimWait());
    }

    public void OnStepUpdate()
    {
        currentExerciseStep++;

        if(currentExerciseStep >= exerciseSteps)
            StartCoroutine(HoldDelay());
        else
        {
            PlayNextStep();
        }
    }

    IEnumerator HoldDelay()
    {
        ExcersiseDashboard.dashboard.infoPanelText.text = $"Now hold your pose for {exerciseObjects[exerciseIndex].holdTime} seconds";
        StartCoroutine(Countdown(exerciseObjects[exerciseIndex].holdTime,(v)=> {
            ExcersiseDashboard.dashboard.exHoldTimeText.text = $"{(int)v}";
        },null));
        yield return new WaitForSeconds(exerciseObjects[exerciseIndex].holdTime);
        PlayNextExercise();
    }

    IEnumerator Countdown(float seconds, System.Action<float> tickCallback = null, System.Action resultCallBack = null)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            tickCallback?.Invoke(counter);
        }
        resultCallBack?.Invoke();
    }

    private void PlayNextStep()
    {
        PlayExercise();
    }

    private void PlayNextExercise()
    {
        if (currentRepeatCount >= difficultyObject.repeatCount)
        {
            numberOfExerciseDone++;
            ExcersiseDashboard.dashboard.exLeftText.text = $"Exercise Round Done : {numberOfExerciseDone}/{numberOfEerciseInQueue}";
            if (numberOfExerciseDone >= numberOfEerciseInQueue)
            {
                //Done
                StopCoroutine(Countdown(0, null, null));
                ScreenManager.screenManager.ShowScreen(MenuScreens.Dashboard);
                return;
            }
            GetRandomExercise();
            currentRepeatCount = 1;
        }
        else
        {
            currentRepeatCount++;
            ExcersiseDashboard.dashboard.exRepeatTimeText.text = $"{currentRepeatCount}/{difficultyObject.repeatCount}";
        }
        currentExerciseStep = 0;
        PlayExercise();
    }
}
