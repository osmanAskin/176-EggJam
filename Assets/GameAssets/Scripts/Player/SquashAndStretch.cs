using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    public Transform Sprite;
    public float Stretch = 0.1f;
    [SerializeField] private Transform squashParent;

    private Rigidbody2D _rigidbody;
    private Vector3 _originalScale;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalScale = Sprite.transform.localScale;

        if (!squashParent)
            squashParent = new GameObject(string.Format("_squash_{0}", name)).transform;
    }

    private void Update()
    {
        Sprite.parent = transform;
        Sprite.localScale = _originalScale;
        Sprite.localRotation = Quaternion.identity;

        squashParent.localScale = Vector3.one;
        squashParent.position = transform.position;

        Vector3 velocity = _rigidbody.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            squashParent.rotation = Quaternion.FromToRotation(Vector3.right, velocity);
        }

        var scaleX = 1.0f + (velocity.magnitude * Stretch);
        var scaleY = 1.0f / scaleX;

        var offsetY = (scaleY - _originalScale.y) * 0.5f;
        Sprite.localPosition = new Vector3(0, offsetY, 0);

        Sprite.parent = squashParent;
        squashParent.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }
}