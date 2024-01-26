using UnityEngine;
using UnityEngine.UI;

public class BlendHead : MonoBehaviour
{
    public SkinnedMeshRenderer head;

    public SkinnedMeshRenderer beard;
    public SkinnedMeshRenderer goatee;
    public SkinnedMeshRenderer mustache;

    public Slider jawLength; //jawForward -- DONE
    public Slider jawWidth; //chinShape2 -- DONE
    public Slider cheeksWide; //cheekBig -- DONE
    public Slider cheeksNarrow; //cheeksSmall -- DONE
    public Slider cheekBones; //cheeksSize -- DONE
    public Slider eyebrows; //browInnerUp -- DONE
    public Slider outerEyebrows; //browOuterUpRight & browOuterUpLeft -- DONE
    public Slider smile; //smile -- DONE
    public Slider chin; //chinShape1 -- DONE
    public Slider chinLength; //chinLong -- DONE
    public Slider eyesHeight; //eyesHeight -- DONE
    public Slider eyesOpen; //blink -- DONE
    public Slider noseLength; //noseHeight  -- DONE
    public Slider ears; //ears_big -- DONE
    public Slider mouthWidth; //mouth_width -- DONE
    public Slider mouthLength; //mouth_length -- DONE
    public Slider lipsThick; //lips_up_thick & lips_down_thick (limit to max value of 50 for each) -- DONE
    public Slider lipsThin; //lips_up_thin & lips_down_thin (limit to max value of 50 for each) -- DONE
    public Slider noseTipUp; //nose_tip_up (limit to max value of 50) -- DONE
    public Slider noseTipDown; //nose_tip_down (limit to max value of 50) -- DONE
    public Slider noseRidge; //nose_crook (max value of 50) -- DONE
    public Slider noseWide; //nose_wide (max value of 50) -- DONE
    public Slider noseNarrow; //nose_narrow (max value of 50) -- DONE

    private string jawLengthIndex;
    private string jawWidthIndex;
    private string cheeksWideIndex;
    private string cheeksNarrowIndex;
    private string cheeksBonesIndex;
    private string innerEyebrowsIndex;
    private string browOuterbrowsRightIndex;
    private string browOuterbrowsLeftIndex;
    private string smileIndex;
    private string chinWidthIndex;
    private string chinLengthIndex;
    private string eyesHeightIndex;
    private string eyesOpenIndex;
    private string noseLengthIndex;
    private string earsIndex;
    private string mouthWidthIndex;
    private string mouthLengthIndex;
    private string lipsThickTopIndex;
    private string lipsThickBottomIndex;
    private string lipsThinTopIndex;
    private string lipsThinBottomIndex;
    private string noseTipUpIndex;
    private string noseTipDownIndex;
    private string noseRidgeIndex;
    private string noseWideIndex;
    private string noseNarrowIndex;

    private string beardSmileIndex;
    private string goateeSmileIndex;
    private string mustacheSmileIndex;


    private void Start()
    {
        head = GetComponent<SkinnedMeshRenderer>();

        jawLengthIndex = head.sharedMesh.GetBlendShapeName(18);

        jawWidthIndex = head.sharedMesh.GetBlendShapeName(54);
        cheeksWideIndex = head.sharedMesh.GetBlendShapeName(63);
        cheeksNarrowIndex = head.sharedMesh.GetBlendShapeName(65);
        cheeksBonesIndex = head.sharedMesh.GetBlendShapeName(58);
        innerEyebrowsIndex = head.sharedMesh.GetBlendShapeName(49);
        browOuterbrowsRightIndex = head.sharedMesh.GetBlendShapeName(46);
        browOuterbrowsLeftIndex = head.sharedMesh.GetBlendShapeName(47);
        smileIndex = head.sharedMesh.GetBlendShapeName(50);
        chinWidthIndex = head.sharedMesh.GetBlendShapeName(53);
        chinLengthIndex = head.sharedMesh.GetBlendShapeName(64);
        eyesHeightIndex = head.sharedMesh.GetBlendShapeName(56);
        eyesOpenIndex = head.sharedMesh.GetBlendShapeName(51);
        noseLengthIndex = head.sharedMesh.GetBlendShapeName(57);
        earsIndex = head.sharedMesh.GetBlendShapeName(66);
        mouthWidthIndex = head.sharedMesh.GetBlendShapeName(67);
        mouthLengthIndex = head.sharedMesh.GetBlendShapeName(68);
        lipsThickTopIndex = head.sharedMesh.GetBlendShapeName(75);
        lipsThickBottomIndex = head.sharedMesh.GetBlendShapeName(76);
        lipsThinTopIndex = head.sharedMesh.GetBlendShapeName(73);
        lipsThinBottomIndex = head.sharedMesh.GetBlendShapeName(74);
        noseTipUpIndex = head.sharedMesh.GetBlendShapeName(77);
        noseTipDownIndex = head.sharedMesh.GetBlendShapeName(78);
        noseRidgeIndex = head.sharedMesh.GetBlendShapeName(79);
        noseWideIndex = head.sharedMesh.GetBlendShapeName(81);
        noseNarrowIndex = head.sharedMesh.GetBlendShapeName(80);

        beardSmileIndex = beard.sharedMesh.GetBlendShapeName(2);
        goateeSmileIndex = goatee.sharedMesh.GetBlendShapeName(1);
        mustacheSmileIndex = mustache.sharedMesh.GetBlendShapeName(3);

    }

    public void ChangeJawLength()
    {
        float jawLengthValue = jawLength.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(jawLengthIndex), jawLengthValue);
    }


    public void ChangeJawWidth()
    {
        if(beard.enabled == false)
        {
            float jawWidthValue = jawWidth.value;
            head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(jawWidthIndex), jawWidthValue);
        }
        else
        {   /*if beard or goatee is equipped the jaw width value must be changed to zero or the skin mesh will
             overlap with the beard/goatee meshes*/
            float jawWidthValue = 0;
            head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(jawWidthIndex), jawWidthValue);
        }      
    }

    public void WidenCheekSize()
    {
        float cheeksWidthValue = cheeksWide.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(cheeksWideIndex), cheeksWidthValue);
    }

    public void NarrowCheekSize()
    {
        float cheeksNarrowValue = cheeksNarrow.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(cheeksNarrowIndex), cheeksNarrowValue);
    }

    public void ChangeCheekBones()
    {
        float cheeksBonesValue = cheekBones.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(cheeksBonesIndex), cheeksBonesValue);
    }

    public void ChangeInnerEyebrows()
    {
        float innerEyebrowsValue = eyebrows.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(innerEyebrowsIndex), innerEyebrowsValue);
    }

    public void ChangeOuterEyebrows()
    {
        float outerEyebrowsValueRight = outerEyebrows.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(browOuterbrowsRightIndex), outerEyebrowsValueRight);

        float outerEyebrowsValueLeft = outerEyebrows.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(browOuterbrowsLeftIndex), outerEyebrowsValueLeft);
    }

    public void ChangeSmile()
    {
        float smileValue = smile.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(smileIndex), smileValue);

        float smileBeardValue = smile.value/2;
        beard.SetBlendShapeWeight(beard.sharedMesh.GetBlendShapeIndex(beardSmileIndex), smileBeardValue);

        float smileGoateeValue = smile.value / 2;
        goatee.SetBlendShapeWeight(goatee.sharedMesh.GetBlendShapeIndex(goateeSmileIndex), smileGoateeValue);

        float smileMustacheValue = smile.value / 2;
        mustache.SetBlendShapeWeight(mustache.sharedMesh.GetBlendShapeIndex(mustacheSmileIndex), smileMustacheValue);
    }

    public void ChangeChinWidth()
    {
        float chinWidthValue = chin.value;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(chinWidthIndex), chinWidthValue);
    }

    public void ChangeChinLength()
    {
        if(beard.enabled || goatee.enabled)
        {
            /*if beard or goatee is equipped the chin length value must be changed to zero or the skin mesh will
             overlap with the beard/goatee meshes*/
            float chinLengthValue = 0;
            head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(chinLengthIndex), chinLengthValue);
            
        }
        else
        {
            float chinLengthValue = chinLength.value / 2;
            head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(chinLengthIndex), chinLengthValue);
        }
    }

    public void ChangeEyesHeight()
    {
        float eyesHeightValue = eyesHeight.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(eyesHeightIndex), eyesHeightValue);
    }

    public void ChangeEyesOpen()
    {
        float eyesOpenValue = eyesOpen.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(eyesOpenIndex), eyesOpenValue);
    }

    public void ChangeNoseLength()
    {
        float noseLengthValue = noseLength.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseLengthIndex), noseLengthValue);
    }

    public void ChangeEarsSize()
    {
        float earsSizeValue = ears.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(earsIndex), earsSizeValue);
    }

    public void ChangeMouthWidth()
    {
        float mouthWidthValue = mouthWidth.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(mouthWidthIndex), mouthWidthValue);
    }

    public void ChangeMouthLength()
    {
        float mouthLengthValue = mouthLength.value/3;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(mouthLengthIndex), mouthLengthValue);
    }

    public void ChangeLipsThick()
    {
        float lipsThickTopValue = lipsThick.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(lipsThickTopIndex), lipsThickTopValue);

        float lipsThickBottomValue = lipsThick.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(lipsThickBottomIndex), lipsThickBottomValue);
    }

    public void ChangeLipsThin()
    {
        float lipsThinTopValue = lipsThin.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(lipsThinTopIndex), lipsThinTopValue);

        float lipsThinBottomValue = lipsThin.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(lipsThinBottomIndex), lipsThinBottomValue);
    }

    public void ChangeNoseTipUp()
    {
        float noseTipUpValue = noseTipUp.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseTipUpIndex), noseTipUpValue);
    }

    public void ChangeNoseTipDown()
    {
        float noseTipDownValue = noseTipDown.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseTipDownIndex), noseTipDownValue);
    }

    public void ChangeNoseRidge()
    {
        float noseRidgeValue = noseRidge.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseRidgeIndex), noseRidgeValue);
    }

    public void ChangeNoseWidth()
    {
        float noseWideValue = noseWide.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseWideIndex), noseWideValue);
    }

    public void ChangeNoseNarrow()
    {
        float noseNarrowValue = noseNarrow.value/2;
        head.SetBlendShapeWeight(head.sharedMesh.GetBlendShapeIndex(noseNarrowIndex), noseNarrowValue);
    }

}