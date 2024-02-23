using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharacterBlendshapes : MonoBehaviour
{
    public BlendHead blendHead;

    private string characterBlendshapesURL = "http://localhost:8888/sqlconnect/characterBlendshapes.php?action=update";

    internal IEnumerator SaveBlendShapes()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        //Add to the form
        form.AddField("characterID", DB_Manager.characterID);
        form.AddField("jaw_length", blendHead.jawLength.value.ToString());
        form.AddField("jaw_width", blendHead.jawWidth.value.ToString());
        form.AddField("cheek_widen", blendHead.cheeksWide.value.ToString());
        form.AddField("cheek_narrow", blendHead.cheeksNarrow.value.ToString());
        form.AddField("cheek_bones", blendHead.cheekBones.value.ToString());
        form.AddField("inner_eyebrows", blendHead.eyebrows.value.ToString());
        form.AddField("outer_eyebrows", blendHead.outerEyebrows.value.ToString());
        form.AddField("smile", blendHead.smile.value.ToString());
        form.AddField("chin_width", blendHead.chin.value.ToString());
        form.AddField("chin_length", blendHead.chinLength.value.ToString());
        form.AddField("eye_height", blendHead.eyesHeight.value.ToString());
        form.AddField("eye_open", blendHead.eyesOpen.value.ToString());
        form.AddField("nose_length", blendHead.noseLength.value.ToString());
        form.AddField("ears_size", blendHead.ears.value.ToString());
        form.AddField("mouth_width", blendHead.mouthWidth.value.ToString());
        form.AddField("mouth_length", blendHead.mouthLength.value.ToString());
        form.AddField("lips_thick", blendHead.lipsThick.value.ToString());
        form.AddField("lips_thin", blendHead.lipsThin.value.ToString());
        form.AddField("nose_tips_up", blendHead.noseTipUp.value.ToString());
        form.AddField("nose_tips_down", blendHead.noseTipDown.value.ToString());
        form.AddField("nose_ridge", blendHead.noseRidge.value.ToString());
        form.AddField("nose_width", blendHead.noseWide.value.ToString());
        form.AddField("nose_narrow", blendHead.noseNarrow.value.ToString());

        UnityWebRequest www = UnityWebRequest.Post(characterBlendshapesURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Character blendshapes failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            if (responseText.StartsWith("0"))
            {
                Debug.Log("Character Blendshapes saved successfully");
            }
            else
            {
                Debug.Log("Character Blendshapes save failed. Error: " + responseText);
            }
        }
    }
}
