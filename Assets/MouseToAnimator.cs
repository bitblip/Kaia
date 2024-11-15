using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MouseToAnimator : MonoBehaviour
{
    private Animator animator;

    private Vector3 _lookVector = Vector3.down;

    [SerializeField] private float _rotationRadianSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Conver the screen position to (-1, 1) in the X and Y
        Vector3 screenPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 normalizedPosition = new Vector2(screenPosition.x * 2 - 1, screenPosition.y * 2 - 1);

        // Flip axis
        var zero = new Vector3(-0.11f, 0.26f, 0);
        var normal = Vector3.SignedAngle(normalizedPosition, zero, Vector3.forward);
        var look = Vector3.SignedAngle(_lookVector, zero, Vector3.forward);

        // Do not cross Up, we need to animation around the other way
        if (normal > 0 && look <= 0)
        {
            // Rotate counter clockwise
            _lookVector = Quaternion.Euler(0, 0, _rotationRadianSpeed * Mathf.Rad2Deg * Time.deltaTime ) * _lookVector;
        }
        else if (normal < 0 && look >= 0)
        {
            // Rotate clockwise
            _lookVector = Quaternion.Euler(0, 0, -_rotationRadianSpeed * Mathf.Rad2Deg * Time.deltaTime) * _lookVector;
        }
        else
        {
            // Just update the look vector
            _lookVector = Vector3.RotateTowards(_lookVector, normalizedPosition, _rotationRadianSpeed * Time.deltaTime, 0.0f);
        }

        Debug.Log(normalizedPosition);
        Debug.DrawRay(transform.position, _lookVector, Color.red);

        // update the animator X and Y
        animator.SetFloat("X", _lookVector.x);
        animator.SetFloat("Y", _lookVector.y);
    }
}
