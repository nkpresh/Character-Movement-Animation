using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    float Xvelocity = 0;
    float Zvelocity = 0;

    int XvelocityHash;
    int ZvelocityHash;

    Animator animator;

    public float acceleration = 2;
    public float deceleration = 2;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        ZvelocityHash = Animator.StringToHash("Velocity Z");
        XvelocityHash = Animator.StringToHash("Velocity X");

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        ChangeVeolocity(forwardPressed, leftPressed, rightPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(ZvelocityHash, Zvelocity);
        animator.SetFloat(XvelocityHash, Xvelocity);
    }

    private void ChangeVeolocity(bool forwardPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity)
    {
        if (forwardPressed && Zvelocity < currentMaxVelocity)
        {
            Zvelocity += Time.deltaTime * acceleration;
        }

        if (leftPressed && Xvelocity > -currentMaxVelocity)
        {
            Xvelocity -= Time.deltaTime * acceleration;
        }

        if (rightPressed && Xvelocity < currentMaxVelocity)
        {
            Xvelocity += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && Zvelocity > 0)
        {
            Zvelocity -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && Xvelocity < 0)
        {
            Xvelocity += Time.deltaTime * deceleration;
        }

        if (!rightPressed && Xvelocity > 0)
        {
            Xvelocity -= Time.deltaTime * deceleration;
        }
    }

    private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        if (!forwardPressed && Zvelocity < 0)
        {
            Zvelocity = 0;
        }

        if (!leftPressed && !rightPressed && Xvelocity != 0 && (Xvelocity > -0.05 || Xvelocity < 0.05))
        {
            Xvelocity = 0;
        }

        if (forwardPressed && runPressed && Zvelocity > currentMaxVelocity)
        {
            Zvelocity = currentMaxVelocity;
        }
        else if (forwardPressed && Zvelocity > currentMaxVelocity)
        {
            Zvelocity -= Time.deltaTime * deceleration;

            if (Zvelocity > currentMaxVelocity && Zvelocity < (currentMaxVelocity + 0.5))
            {
                Zvelocity = currentMaxVelocity;
            }
        }
        else if (forwardPressed && Zvelocity < currentMaxVelocity && Zvelocity > (currentMaxVelocity - 0.05))
        {
            Zvelocity = currentMaxVelocity;
        }

        if (leftPressed && runPressed && Xvelocity < -currentMaxVelocity)
        {
            Xvelocity = -currentMaxVelocity;
        }
        else if (leftPressed && Xvelocity < -currentMaxVelocity)
        {
            Xvelocity += Time.deltaTime * deceleration;
            if (Xvelocity < -currentMaxVelocity && Xvelocity > (-currentMaxVelocity - 0.05f))
            {
                Xvelocity = -currentMaxVelocity;
            }
        }
        else if (leftPressed && Xvelocity > -currentMaxVelocity && Xvelocity < (-currentMaxVelocity + 0.05))
        {
            Xvelocity = -currentMaxVelocity;
        }

        if (rightPressed && runPressed && Xvelocity > currentMaxVelocity)
        {
            Xvelocity = currentMaxVelocity;
        }
        else if (rightPressed && Xvelocity > currentMaxVelocity)
        {
            Xvelocity -= Time.deltaTime * deceleration;
            if (Xvelocity > currentMaxVelocity && Xvelocity < (currentMaxVelocity + 0.05f))
            {
                Xvelocity = currentMaxVelocity;
            }
        }
        else if (rightPressed && Xvelocity < currentMaxVelocity && Xvelocity > (currentMaxVelocity - 0.05))
        {
            Xvelocity = currentMaxVelocity;
        }

    }
}

