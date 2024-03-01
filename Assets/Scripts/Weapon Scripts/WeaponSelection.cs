using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private int selectedWeapon = -1;
    [SerializeField] private int numberOfSlots;
    private bool keyIsPressed = false;

    private void Start()
    {

    }

    private void Update()
    {
        WeaponSelectLogic();
        SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in Camera.main.transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                weapon.GetComponent<Gun_Controller>().Deactivation();
            }

            i++;
        }
    }

    void WeaponSelectLogic()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            keyIsPressed = true;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            keyIsPressed = false;
        }

        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < numberOfSlots; i++)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i)) && i < Camera.main.transform.childCount && keyIsPressed == false)
            {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

}
