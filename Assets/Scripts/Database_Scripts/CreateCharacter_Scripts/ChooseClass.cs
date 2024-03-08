using TMPro;
using UnityEngine;

public class ChooseClass : MonoBehaviour
{
    public GameObject engineerClassDesc;
    public GameObject fighterClassDesc;
    public GameObject medicClassDesc;
    public GameObject scoutClassDesc;
    public TMP_Dropdown chooseClass;

    private void Start()
    {
        fighterClassDesc.SetActive(false);
        medicClassDesc.SetActive(false);
        scoutClassDesc.SetActive(false);
    }

    public void ChooseYourClass()
    {
        int index = chooseClass.value;
        switch (index)
        {
            case 0:
                engineerClassDesc.SetActive(true);
                fighterClassDesc.SetActive(false);
                medicClassDesc.SetActive(false);
                scoutClassDesc.SetActive(false);
                break;
            case 1:
                fighterClassDesc.SetActive(true);
                engineerClassDesc.SetActive(false);
                medicClassDesc.SetActive(false);
                scoutClassDesc.SetActive(false);
                break;
            case 2:
                medicClassDesc.SetActive(true);
                engineerClassDesc.SetActive(false);
                fighterClassDesc.SetActive(false);
                scoutClassDesc.SetActive(false);
                break;
            case 3:
                scoutClassDesc.SetActive(true);
                engineerClassDesc.SetActive(false);
                fighterClassDesc.SetActive(false);
                medicClassDesc.SetActive(false);
                break;
        }
    }
}
