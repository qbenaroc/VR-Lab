using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float _characterControllerInitialHeight = 0.2f;

    private float _fallingSpeed;
    private XRRig _rig;
    private Vector2 _inputAxis;
    private CharacterController _character;

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<CharacterController>();
        _rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
    }

	private void FixedUpdate()
    {
        CapsuleFollowHead();
        Quaternion headYaw = Quaternion.Euler(0, _rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(_inputAxis.x, 0, _inputAxis.y);

        _character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity
        bool isGrounded = CheckIfGrounded();

        if (isGrounded)
            _fallingSpeed = 0;
        else
            _fallingSpeed += gravity * Time.fixedDeltaTime;

        _character.Move(Vector3.up * _fallingSpeed * Time.fixedDeltaTime);
	}

    void CapsuleFollowHead()
	{
        _character.height = _rig.cameraInRigSpaceHeight + _characterControllerInitialHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(_rig.cameraGameObject.transform.position);
        _character.center = new Vector3(capsuleCenter.x, _character.height / 2 + _character.skinWidth, capsuleCenter.z);
	}

    bool CheckIfGrounded()
	{
        Vector3 rayStart = transform.TransformPoint(_character.center);
        float rayLength = _character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return (hasHit);
	}
}