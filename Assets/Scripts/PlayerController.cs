using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public int HP=6;


    public SteamVR_Action_Boolean jumpInput;
    public SteamVR_Action_Vector2 touchInput;

    public Transform cameraTransform;
    private CapsuleCollider capsuleCollider;

    public TMP_Text hpText;

    bool isJumping;

    bool GameOver;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"HP: {HP}";

        if (HP<=0)
        {
            GameOver = true;
        }
    }

    private void FixedUpdate()
    {
        if (!GameOver)
        {
            if (jumpInput.state && !isJumping)
            {
                isJumping = true;
            }
            if (isJumping)
            {
                transform.position += Time.deltaTime * Vector3.up * 4.0f;
            }

            Vector3 movementdir = Player.instance.hmdTransform.TransformDirection(new Vector3(touchInput.axis.x, 0, touchInput.axis.y));



            transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementdir * 3.0f, Vector3.up);

            float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
            capsuleCollider.height = distanceFromFloor;

            //capsuleCollider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up;
        }
        else
        {
            gameOverScreen.SetActive(true);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
