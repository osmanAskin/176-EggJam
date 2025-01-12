using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation _animation;
    
    public void Shake()
    {
        _animation.tween.Rewind();
        _animation.tween.Play();
    }
}

