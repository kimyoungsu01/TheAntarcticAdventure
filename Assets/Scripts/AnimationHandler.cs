using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsDie = Animator.StringToHash("IsDie"); // IsDie 애니메이션을 위한 해시 추가

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsJump, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsSlide, true);
    }

    public void IncincibilityEnd()
    {
        animator.SetBool(IsSlide, false);
    }
    public void SetDieState(int dieValue)
    {
        animator?.SetInteger(IsDie, dieValue);
    }
}