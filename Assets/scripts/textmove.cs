using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textmove : MonoBehaviour
{
    public Animator animator;
    public Animation x;
    private void OnEnable()
    {
        animator.Play("New Animation");


    }
    private void OnDisable()
    {

    }
}
