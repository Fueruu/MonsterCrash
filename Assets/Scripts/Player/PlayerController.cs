using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using InControl;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : APlayer
{
    #region ExposedVariables
    [Header("Dash Properties")]
    [SerializeField] private float rateOfCharge = 0.1f;
    [SerializeField] private float dashOffset = 6;
    [SerializeField] private float maxCharge = 150.0f;
    [SerializeField] private float minCharge = 10.0f;


    /* ----------------------------------------------- */

    [Header("Player Rotation Control")]
    [SerializeField] private float turnSpeed;

    /* ----------------------------------------------- */

    [Header("Target Indicator")]
    public LineRenderer _LineRenderer;
    [SerializeField] private Transform _TargetTransform;

    /* ----------------------------------------------- */

    [Header("Physics")]
    [SerializeField] private float baseKnockBack = 300;
    [SerializeField] private float _MaxGroundDistance = 1f;
    [SerializeField] private float stopCheckTolerance = 0.005f;	//Added by Isai, used to handle check for stop.
    [SerializeField] private float _AngleOfDamage = 0.7f;

    /* ----------------------------------------------- */

    [Header("Debug")]
    [SerializeField] private float dashDistance = 0.0f;
    #endregion

    /* ----------------------------------------------- */

    #region HiddenVariables
    private Vector3 _OriginalTransformPos;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private bool isCharging = false;

    #endregion

    /* ----------------------------------------------- */

    #region UnityFunctions
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        _OriginalTransformPos.y = _TargetTransform.position.y;
        //print(rb.sleepThreshold);
    }

    private void Update()
    {
        if (IsDead())
        {
            anim.SetTrigger("IsDead");
            return;
        }

        //Debug.LogFormat("{0} Ground: {1}", gameObject.name, IsGrounded());

        if (GroundCheckDistance() < _MaxGroundDistance)
        {
            Dash();
            JoyStickControl();
            /// <summary>
            /// TODO: Stop falling audio when grounded
            /// Issue: Stop causes all sound to be stopped 
            /// </summary>
        }
        else
        {
            anim.SetTrigger("IsFalling");
            if (fallingAudio && !fallingAudio.IsPlaying())
            {
                fallingAudio.Play();
            }
        }
        IsMoving();

        _LineRenderer.SetPosition(0, transform.position);
        _LineRenderer.SetPosition(1, _TargetTransform.transform.position);
    }

    private void FixedUpdate()
    {
        if (GroundCheckDistance() < _MaxGroundDistance)
        {
            ChargeDash();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            var building = other.gameObject.GetComponent<Item>();
            currentHP += building.Heal();
            damageMultiplier += building.MultiplyDamage();
        }

        if (other.gameObject.GetComponent<PlayerController>())
        {
            if(_OnCollisionParticle != null)
            {
                _OnCollisionParticle.Play();
            }
            //  Apply knockback so we can breath
            Vector3 dir = (other.transform.position - rb.transform.position).normalized;

            var otherPlayerController = other.gameObject.GetComponent<PlayerController>();
            CalculateDamage(damage, damageMultiplier);

            //  Is the other player moving? 
            //  If not, do damage
            //  If they are...
            //  What angle are we colliding at?
            if (otherPlayerController.IsMoving())
            {
                Debug.Log("They are moving.");
                //  If colliding at x angle and not head on
                //  Then do damage
                if (IsFacingPlayer(otherPlayerController))
                {
                    //Debug.Log("Collided head on.");

                    //  If head on?
                    //  Compare damage values and apply the one with higher damage minus the other players damage
                    //  The player that does the damage knocks back the one that gets hurt
                    if (totalDamage > otherPlayerController.totalDamage)
                    {
                        other.gameObject.GetComponent<Rigidbody>().AddForce(dir * CalculateKnockBack(baseKnockBack, totalDamage), ForceMode.VelocityChange);
                        otherPlayerController.CalculateHealth(totalDamage - otherPlayerController.totalDamage);
                        Debug.Log("Other took damage.");
                    }
                    //  Player takes damage
                    else if (totalDamage < otherPlayerController.totalDamage)
                    {
                        CalculateHealth(otherPlayerController.totalDamage - totalDamage);
                        Debug.Log("You took damage.");
                    }
                    else
                    {
                        // Apply force to both players if their damage is the same (needs to be fixed)
                        rb.AddForce(dir * CalculateKnockBack(baseKnockBack, totalDamage), ForceMode.VelocityChange);
                    }
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().AddForce(dir * CalculateKnockBack(baseKnockBack, totalDamage), ForceMode.VelocityChange);
                    otherPlayerController.CalculateHealth(totalDamage);
                }
            }
            else
            {
                Debug.Log("They didn't move.");
                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * CalculateKnockBack(baseKnockBack, totalDamage), ForceMode.VelocityChange);
                otherPlayerController.CalculateHealth(totalDamage);
            }
        }

        if (impactAudio && !impactAudio.IsPlaying())
        {
            impactAudio.PlayOneShot();
        }
    }

    #endregion
    #region PrivateFunctions
    //  Apply joystick controls
    private void JoyStickControl()
    {
        Vector3 lookDirection = GetJoystickValues();

        if (lookDirection == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        float turn = turnSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turn);
    }

    private Vector3 GetJoystickValues()
    {
        float inputY = deviceInput.Direction.Y;
        float inputX = deviceInput.Direction.X;

        return new Vector3(inputX, 0F, inputY);
    }

    private void Dash()
    {
        if (deviceInput == null) return;

        if (deviceInput.RightTrigger.IsPressed)
        {
            //  Allow our dash to charge up
            if (chargeUpAudio && !chargeUpAudio.IsPlaying())
            {
                chargeUpAudio.PlayOneShot();
            }

            if (_ChargeUpParticle != null)
            {
                _ChargeUpParticle.Play();
            }

            isCharging = true;
        }

        if (deviceInput.RightTrigger.WasReleased)
        {
            isCharging = false;
            if (dashDistance < minCharge) return;

            anim.SetTrigger("ReleaseCharge");

            if (dashAudio != null)
            {
                dashAudio.PlayOneShot();
            }

            chargeUpAudio.Stop();
            
            if (_ChargeUpParticle != null)
            {
                _ChargeUpParticle.Stop();
            }

            if(_DashParticle != null)
            {
                _DashParticle.Play();
            }


            // Calculate our dash
            rb.AddForce(transform.forward * dashDistance * dashOffset, ForceMode.VelocityChange);
        }
    }

    private void ChargeDash()
    {
        if (isCharging)
        {
            if (dashDistance >= maxCharge) return;

            anim.SetBool("StartCharge", true);

            dashDistance += rateOfCharge;

            //  get the percent of charge and multiply it by our max damage value so we can create a scaling damage dependant on how much charge
            var chargePercent = Mathf.InverseLerp(minCharge, maxCharge, dashDistance);
            damage = maxChargeDamage * chargePercent;

            _TargetTransform.transform.Translate(Vector3.forward * rateOfCharge);
        }
        else
        {
            dashDistance = 0;
            _TargetTransform.transform.localPosition = new Vector3(0, _OriginalTransformPos.y, 0);
            anim.SetBool("StartCharge", false);
        }
    }

    private float CalculateKnockBack(float knockback, float damage)
    {
        if (totalDamage > 0)
        {
            return baseKnockBack * totalDamage;
        }
        else
        {
            return baseKnockBack;
        }
    }

    public bool IsMoving()
    {
        //if (Mathf.Approximately(rb.velocity.magnitude, Vector3.zero.magnitude))	Previous version of stop comparison. Kept as comment for future reference.
        if (Mathf.Abs(rb.velocity.magnitude - Vector3.zero.magnitude) < stopCheckTolerance)
        {
            //rb.velocity.Set(0,0,0);
            //rb.sleepThreshold = 5;
            //print("Not moving");
            damageMultiplier = 1;
            anim.SetTrigger("Stopped");
            return false;
        }
        return true;
    }

    private float GroundCheckDistance()
    {

        RaycastHit hit;
        var distanceToGround = Physics.Raycast(transform.position, -Vector3.up, out hit);
        //Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance + 0.1F); 
        return hit.distance;
    }

    private bool IsFacingPlayer(PlayerController other)
    {
        float dot = Vector3.Dot(transform.forward, (other.transform.position - transform.position).normalized);
        Debug.Log(dot);
        if (dot > _AngleOfDamage)
        {
            Debug.Log("Not facing");
            return false;
        }
        return true;
    }

    #endregion

}
