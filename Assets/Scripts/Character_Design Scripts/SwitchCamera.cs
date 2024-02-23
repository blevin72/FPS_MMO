using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    public GameObject leftScrollView;
    public GameObject rightScrollView;
    public Slider modelRotate;
    public Button blendFaceOptions;
    public Button backButton;
    public Button confirmButton;

    public GameObject facePanel;
    public Slider modelFaceRotate;

    public Camera characterCamera;
    public Camera faceCamera;

    public void Start()
    {
        facePanel.SetActive(false);
        modelFaceRotate.gameObject.SetActive(false);
        faceCamera.enabled = false;
        backButton.gameObject.SetActive(false);
    }

    public void ZoomToFace()
    {
        leftScrollView.SetActive(false);
        rightScrollView.SetActive(false);
        modelRotate.gameObject.SetActive(false);
        characterCamera.enabled = false;
        blendFaceOptions.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);

        facePanel.SetActive(true);
        modelFaceRotate.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        faceCamera.enabled = true;
    }

    public void ZoomOut()
    {
        facePanel.SetActive(false);
        modelFaceRotate.gameObject.SetActive(false);
        faceCamera.enabled = false;
        backButton.gameObject.SetActive(false);

        leftScrollView.SetActive(true);
        rightScrollView.SetActive(true);
        modelRotate.gameObject.SetActive(true);
        characterCamera.enabled = true;
        blendFaceOptions.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }

}
