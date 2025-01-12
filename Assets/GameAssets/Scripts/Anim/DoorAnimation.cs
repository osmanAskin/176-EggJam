using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int DoorTrigger = Animator.StringToHash("DoorTrigger");

    public void OpenDoor()
    {
        animator.SetTrigger(DoorTrigger);
    }
}
