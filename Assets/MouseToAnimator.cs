using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MouseToAnimator : MonoBehaviour
{
    private Animator animator;

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

        // update the animator X and Y
        animator.SetFloat("X", normalizedPosition.x);
        animator.SetFloat("Y", normalizedPosition.y);
    }
}
