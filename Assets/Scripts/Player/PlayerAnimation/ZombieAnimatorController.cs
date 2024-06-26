using UnityEngine;

// This script plays animation for the zombie
[RequireComponent(typeof(Animator))]
public class ZombieAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play("zombie_idle");
    }

    public void PlayRun()
    {
        _animator.Play("zombie_walk_forward");
    }

    public void PlayAttack()
    {
        _animator.Play("zombie_attack");
    }
}