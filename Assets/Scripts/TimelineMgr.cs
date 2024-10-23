using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineMgr : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public RuntimeAnimatorController rabbitIdleAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnTimelineFinished", (float)timelineDirector.duration);
    }

    private void OnTimelineFinished()
    {
        Animator animator = GameObject.Find("Rabbit1").GetComponent<Animator>();
        if (animator != null && rabbitIdleAnimator != null)
        {
            animator.runtimeAnimatorController = rabbitIdleAnimator;   
        }
    }
}
