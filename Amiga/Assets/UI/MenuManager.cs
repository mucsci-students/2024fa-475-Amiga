using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMenuManager : MonoBehaviour
{

    [SerializeField] private Staff staff;
    [SerializeField] private List<GameObject> staffs;
    [SerializeField] private List<GameObject> staffSlots;
    [SerializeField] private MenuInitializer menuInitializer;

    private List<int> staffSlotXCoords = new List<int> {240, 160, 200, 120, 80, 0, 40};
    private List<int> staffSlotYCoords = new List<int> {140, 140, 20, 20, 140, 160, 20};

    void OnEnable ()
    {
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            staffSlots[i].SetActive (true);
        }
        staffs[staff.staffLevel].SetActive (true);
        for (int i = 0; i < menuInitializer.inventorySlots.Count; ++i)
        {
            menuInitializer.inventorySlots[i].SetActive (true);
        }
    }

    void OnDisable ()
    {
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            staffSlots[i].SetActive (false);
        }
        staffs[staff.staffLevel].SetActive (false);
        for (int i = 0; i < menuInitializer.inventorySlots.Count; ++i)
        {
            menuInitializer.inventorySlots[i].SetActive (false);
        }
    }

    public Vector3 AttachmentUIChange (GameObject attachmentUI, Vector3 lastPos)
    {
        Vector3 pos = attachmentUI.transform.localPosition;
        int loc = PositionToInventorySlot (pos);
        if (loc != -1)
        {
            // was inventory slot
            print ("from: " + lastPos + "\nto: " + loc);
        }
        else
        {
            // was attached to staff
            loc = PositionToStaffSlot (pos);
            print ("from: " + lastPos + "\nto: " + loc);
        }
        return new Vector3 (0f, 0f, 0f);
    }

    private int PositionToInventorySlot (Vector3 pos)
    {
        if (pos.x >= -225f - 10f - 25f && pos.x <= 225f + 10f + 25f && pos.y >= -152f - 3f - 25f && pos.y <= -52f + 3f + 25f)
        {
            int col = (int) ((pos.x + 260f) / 52f);
            if (col == 10) col = 9;
            int row = (int) ((-pos.y - 24f) / 52f);
            if (row == 3) row = 2;
            return col % 10 + row * 10;
        }
        else
        {
            return -1;
        }
    }

    private int PositionToStaffSlot (Vector3 pos)
    {
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            if (pos.x >= staffSlotXCoords[i] - 26 && pos.x <= staffSlotXCoords[i] + 26 && pos.y >= staffSlotYCoords[i] - 26 && pos.y <= staffSlotYCoords[i])
            {
                return i;
            }
        }
        return -1;
    }
}
