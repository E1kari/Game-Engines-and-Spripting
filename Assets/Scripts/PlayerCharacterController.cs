using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacterController : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI UIAmmoCount;

    float sideMovement;
    float forwardMovement;
    float horizontalMouse;
    float verticalMouse;
    float verticalAngle = 0f;
    public float MovementSpeed = 5.0f;
    public float MouseSensitivity = 5f;
    public GameObject PlayerCamera;
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    public GameObject Bullet;
    public float BulletSpeed;
    public float FireRate = 5f;
    bool isReloading = false;
    public int StartingAmmoCount;
    int ammoCount;
    public float ReloadingTime = 2f;
    bool coolDownActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ammoCount = StartingAmmoCount;
        UIAmmoCount.text = ammoCount.ToString();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GetInputs();
        if (horizontalMouse != 0)
        {
            HorizontalCameraMovement();
        }
        if (verticalMouse != 0)
        {
            VerticalCameraMovement();
        }
        Movement();


        if (ammoCount > 0 && Input.GetButtonDown("Fire1") && !coolDownActive && !isReloading)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }
        if (ammoCount != StartingAmmoCount && !isReloading && Input.GetButtonDown("Reload"))
        {
            StartCoroutine(Reload());
        }
    }
    private void GetInputs()
    {
        sideMovement = Input.GetAxis("Horizontal");
        forwardMovement = Input.GetAxis("Vertical");
        horizontalMouse = Input.GetAxisRaw("Mouse X");
        verticalMouse = Input.GetAxisRaw("Mouse Y");
    }
    private void HorizontalCameraMovement()
    {
        transform.Rotate(new Vector3(0, horizontalMouse, 0) * MouseSensitivity);
    }
    private void VerticalCameraMovement()
    {
        verticalAngle += -verticalMouse * MouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, -90, 90);
        PlayerCamera.transform.localEulerAngles = new Vector3(verticalAngle, 0, 0);
    }
    private void Movement()
    {
        moveDirection = new Vector3(sideMovement, -9.81f * 3 * Time.deltaTime, forwardMovement); //@todo: out source into variables
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * MovementSpeed * Time.deltaTime);
    }
    private void Shoot()
    {
        GameObject spawnedBullet = Instantiate(Bullet, PlayerCamera.transform.position, PlayerCamera.transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnedBullet.transform.forward * BulletSpeed;
        ammoCount--;
        UIAmmoCount.text = ammoCount.ToString();
    }
    IEnumerator Reload()
    {
        isReloading = true;
        UIAmmoCount.text = "Reloading...";
        yield return new WaitForSeconds(ReloadingTime);
        ammoCount = StartingAmmoCount;
        isReloading = false;
        UIAmmoCount.text = ammoCount.ToString();
    }
    IEnumerator Cooldown()
    {
        coolDownActive = true;
        yield return new WaitForSeconds(1 / FireRate);
        coolDownActive = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward);
    }
}
