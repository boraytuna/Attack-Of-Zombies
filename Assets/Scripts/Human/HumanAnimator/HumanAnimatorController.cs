using UnityEngine;

public class HumanAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play("idle");
    }

    public void PlayRun()
    {
        _animator.Play("run");
    }

    public void PlayWave()
    {
        _animator.Play("wave");
    }
}
