using UnityEngine;

public class ControllerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
    
    public void OpenController()
    {
        animator.SetTrigger(OpenDoor);
    }
}