using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    //VR RealWorld Input
    public XRNode inputSource = XRNode.LeftHand; //checks which device to get input from
    private Vector2 inputAxis;
    public Camera cameraHead;

    //In-Game Character
    private CharacterController character;
    public float speed = 1f;

    //Layer Masks + Gravity
    public float fallingSpeed;
    public float gravAccel = -1f;
    public LayerMask groundLayer;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        //adjusting for an input bug with the controller
        if ((inputAxis.x < 0.15 && inputAxis.y < 0.15) && (inputAxis.x > 0 && inputAxis.y > 0))
        {
            inputAxis.x = 0;
            inputAxis.y = 0;
        }
    }

    private void FixedUpdate()
    {
        //Get Headset Direction values
        Quaternion headYaw = Quaternion.Euler(x: 0, cameraHead.transform.eulerAngles.y, z: 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * speed * Time.deltaTime);

        //Check layers
        bool isGrounded = CheckIfGrounded(groundLayer);

        //Apply Gravity
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravAccel * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    bool CheckIfGrounded(LayerMask layerName)
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, layerName);
        return hasHit;
    }
}
