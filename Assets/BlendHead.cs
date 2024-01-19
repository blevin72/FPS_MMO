using UnityEngine;
using UnityEngine.UI;

public class BlendHead : MonoBehaviour
{
    public SkinnedMeshRenderer head;
    public Slider jawLength; //jawForward
    public Slider jawWidth; //chinShape2
    public Slider cheeks; //cheekPuff & cheeksSmall (cheekPUff to make bigger, cheek Small to make smaller)
    public Slider dimpleRight; //mouthDimpleRight
    public Slider dimpleLeft; //mouthDimpleLeft
    public Slider brows; //browInnerUp
    public Slider smile; //smile
    public Slider chin; //chinShape1
    public Slider eyesHeight; //eyesHeight
    public Slider noseLength; //noseHeight
    public Slider chinLength; //chinLong
    public Slider ears; //ears_big
    public Slider mouthWidth; //mouth_width
    public Slider lipsWidth; //lips_up_thick & lips_down_thick (limit to max value of 50 for each)
    public Slider noseTip; //nose_tip_up & nose_tip_down (limit to max value of 50 for each)
    public Slider noseRidge; //nose_crook (max value of 50)
    public Slider noseWidth; //nose_narrow & nose_wide (max value of 50 for each)

    private string jawLengthIndex;

    private void Start()
    {
        head = GetComponent<SkinnedMeshRenderer>();

        jawLengthIndex = head.sharedMesh.GetBlendShapeName(18);
    }

    public void ChangeJawLength()
    {
        float jawLengthValue = jawLength.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(jawLengthIndex), jawLengthValue);
    }
}