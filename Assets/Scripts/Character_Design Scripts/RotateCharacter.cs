using UnityEngine;
using UnityEngine.UI;

public class RotateCharacter : MonoBehaviour
{
    public Transform characterModel;
    public Slider rotationSlider;
    public Slider rotationSlider_2;

    private void Start()
    {
        characterModel.rotation = Quaternion.Euler(0f, 200f, 0f);
    }

    public void RotateModel()
    {
        float rotation = rotationSlider.value * -360f;
        characterModel.rotation = Quaternion.Euler(0f, 200f + rotation, 0f);
    }

    public void RotateModelFace()
    {
        float rotation = rotationSlider_2.value * -360f;
        characterModel.rotation = Quaternion.Euler(0f, 200f + rotation, 0f);
    }
}
