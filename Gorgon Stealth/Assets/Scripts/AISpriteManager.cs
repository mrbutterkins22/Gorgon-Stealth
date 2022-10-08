using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpriteManager : MonoBehaviour
{
    private Transform cam;
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] bool mainSprite;

    private void Awake()
    {
        cam = Camera.main.transform;
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.forward = cam.forward;
        if(mainSprite)
        {
            int angle = Mathf.RoundToInt(transform.localEulerAngles.y / 90);
            anim.SetInteger("angle", angle);
            
            

            if (rb.velocity.x != 0 || rb.velocity.z != 0)
            {
                anim.SetBool("moving", true);
            }
            else anim.SetBool("moving", false);
        }
    }
}
