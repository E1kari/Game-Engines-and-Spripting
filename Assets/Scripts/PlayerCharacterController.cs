using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacterController : MonoBehaviour
{
    public int health_;
    public TextMeshProUGUI uiHealthCount_;

    //-----camera-----//
    public GameObject playerCamera_;
    public float mouseSensitivity_ = 5f;
    private float verticalAngle = 0f;

    //-----movement-----//
    //MovementState movementState_;
    CharacterController characterController_;
    public float movementSpeed_ = 5.0f;

    //-----shooting-----//
    //SecondaryState secondaryState_;
    public GameObject bullet_;
    public float bulletSpeed_ = 300;
    public int startingAmmoCount_;
    private int ammoCount_;
    public TextMeshProUGUI uiAmmoCount_;
    public float reloadingTime_ = 2f;
    private bool isReloading = false;

    public enum MovementState
    {
        standing,
        walking
    }

    public enum SecondaryState
    {
        none,
        reloading,
        shooting,
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ammoCount_ = startingAmmoCount_;
        uiAmmoCount_.text = ammoCount_.ToString();
        characterController_ = GetComponent<CharacterController>();

        //movementState_ = MovementState.standing;
        //secondaryState_ = SecondaryState.none;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CameraMovement();

        if (ammoCount_ > 0 && Input.GetButtonDown("Fire1") && !isReloading)
        {
            Shoot();
        }
        if (ammoCount_ != startingAmmoCount_ && !isReloading && Input.GetButtonDown("Reload"))
        {
            StartCoroutine(Reload());
        }
    }

    private void CameraMovement()
    {
        float horizontalMouseInput = Input.GetAxisRaw("Mouse X");
        float verticalMouseInput = Input.GetAxisRaw("Mouse Y");

        if (horizontalMouseInput != 0)
        {
            transform.Rotate(new Vector3(0, horizontalMouseInput, 0) * mouseSensitivity_);
        }
        if (verticalMouseInput != 0)
        {
            verticalAngle += -verticalMouseInput * mouseSensitivity_;
            verticalAngle = Mathf.Clamp(verticalAngle, -90, 90);
            playerCamera_.transform.localEulerAngles = new Vector3(verticalAngle, 0, 0);
        }
    }

    private void Movement()
    {
        float sideMovement = Input.GetAxis("Horizontal");
        float forwardMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = Vector3.zero;

        moveDirection = new Vector3(sideMovement, -9.81f * 3 * Time.deltaTime, forwardMovement); //@todo: out source into variables
        moveDirection = transform.TransformDirection(moveDirection);
        characterController_.Move(moveDirection * movementSpeed_ * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet_, playerCamera_.transform.position, playerCamera_.transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnedBullet.transform.forward * bulletSpeed_;
        ammoCount_--;
        uiAmmoCount_.text = ammoCount_.ToString();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        uiAmmoCount_.text = "Reloading...";
        yield return new WaitForSeconds(reloadingTime_);
        ammoCount_ = startingAmmoCount_;
        isReloading = false;
        uiAmmoCount_.text = ammoCount_.ToString();
    }

    public void getHit()
    {
        health_--;
        uiHealthCount_.text = health_.ToString();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward);
    }
}
