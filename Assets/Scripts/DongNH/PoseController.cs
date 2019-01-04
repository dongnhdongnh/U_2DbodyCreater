using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{
    public Animator anim;
    public PoseMaker pose;
    public Rigidbody2D mainBody;
    public Vector2 force_Jump;
    public float forfe_torque;
    public Rigidbody2D[] myPose;
    HingeJoint2D _join;
    public PoseState characterCurrentState;
    // Use this for initialization
    void Start()
    {
        ChangeState(PoseState.STAND);
        //_join.
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            mainBody.AddForce(force_Jump);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeState(0);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // ChangeState(1);

        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (characterCurrentState)
            {
                case PoseState.STAND:
                    ChangeState(PoseState.READY);
                    break;
                case PoseState.READY:
                    ChangeState(PoseState.JUMP);
                    break;
                case PoseState.JUMP:
                    mainBody.AddTorque(forfe_torque);
                    break;
                default:
                    break;
            }

        }
    }

    public void ChangeState(PoseState state)
    {
        this.characterCurrentState = state;
        switch (state)
        {
            case PoseState.STAND:
                ChangeAnim(0);
                break;
            case PoseState.READY:
                ChangeAnim(1);
                break;
            case PoseState.JUMP:
                ChangeAnim(2);
                pose.MaKeStatic(false);
                mainBody.AddForce(force_Jump);
             
                break;
            default:
                break;
        }

    }

    void ChangeAnim(int ID)
    {
        StartCoroutine(DoChangeState(ID));
    }
    IEnumerator DoChangeState(int ID)
    {
        pose.MaKeStatic(true);
        anim.SetInteger("states", ID);
        yield return new WaitForSeconds(1);
        // pose.MaKeStatic(false);
    }
}
