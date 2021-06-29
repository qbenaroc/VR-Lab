using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 _initialAttachLocalPos;
    private Quaternion _initialAttachLocalRot;

    // Start is called before the first frame update
    void Start()
    {
        //Create Attach Point
        if (!attachTransform)
		{
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
		}

        _initialAttachLocalPos = attachTransform.localPosition;
        _initialAttachLocalRot = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
	{
        if (interactor is XRDirectInteractor)
		{
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
		}
        else
		{
            attachTransform.position = _initialAttachLocalPos;
            attachTransform.rotation = _initialAttachLocalRot;
		}

        base.OnSelectEntered(interactor);
	}
}
