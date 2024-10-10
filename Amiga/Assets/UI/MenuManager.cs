using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMenuManager : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] private Staff staff;
    [SerializeField] private List<GameObject> staffs;
    [SerializeField] private List<GameObject> staffSlots;
    [SerializeField] private MenuInitializer menuInitializer;
    [SerializeField] private GameObject attachmentUITemplate;

    private List<float> staffSlotXCoords = new List<float> {240f, 160f, 200f, 120f, 80f, 0f, 40f};
    private List<float> staffSlotYCoords = new List<float> {140f, 140f, 20f, 20f, 140f, 160f, 20f};

    public List<GameObject> attachmentUIInsts = new List<GameObject> (); // {attached...attached, inventory...inventory}

    // whenever the staff menu is enabled/ disables:
    void OnEnable ()
    {
        // generate staff slots, staff, and inventory slots
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            staffSlots[i].SetActive (true);
        }
        staffs[staff.staffLevel].SetActive (true);
        for (int i = 0; i < menuInitializer.inventorySlots.Count; ++i)
        {
            menuInitializer.inventorySlots[i].SetActive (true);
        }

        // generate currently attached attachments
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            attachmentUIInsts.Add (null);
            if(staff.attachments[i] != null)
            {
                attachmentUIInsts[i] = GameObject.Instantiate (attachmentUITemplate, canvas.transform);
                attachmentUIInsts[i].transform.localPosition = StaffSlotToPosition (i);
                attachmentUIInsts[i].GetComponent<AttachmentUIScript> ().attachment = staff.attachments[i];
                attachmentUIInsts[i].SetActive (true);
            }
        }

        // generate attachments in inventory
        int numInstsSoFar = attachmentUIInsts.Count;
        for (int i = 0; i < staff.inventory.Count; ++i)
        {
            attachmentUIInsts.Add (null);
            if (staff.inventory[i] != null)
            {
                attachmentUIInsts[i + numInstsSoFar] = GameObject.Instantiate (attachmentUITemplate, canvas.transform);
                attachmentUIInsts[i + numInstsSoFar].transform.localPosition = InventorySlotToPosition (i);
                attachmentUIInsts[i + numInstsSoFar].GetComponent<AttachmentUIScript> ().attachment = staff.inventory[i];
                attachmentUIInsts[i + numInstsSoFar].SetActive (true);
            }
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
        for (int i = 0; i < attachmentUIInsts.Count; ++i)
        {
            if(attachmentUIInsts[i] != null)
            {
                GameObject.Destroy (attachmentUIInsts[i]);
                attachmentUIInsts[i] = null;
            }
        }
        attachmentUIInsts.Clear ();
    }


    // update the staff script and its attachments whenever an attachment UI element gets moved
    public Vector3 AttachmentUIChange (GameObject attachmentUI, Vector3 lastPos)
    {
        Vector3 pos = attachmentUI.transform.localPosition;
        int loc = PositionToInventorySlot (pos);
        Attachment attachment = attachmentUI.GetComponent<AttachmentUIScript> ().attachment;
        if (loc != -1)
        {
            int lastLoc = PositionToInventorySlot (lastPos);
            if (lastLoc != -1)
            {
                // inventory slot -> inventory slot
                staff.DiscardAttachment (lastLoc);
                staff.StoreAttachment (staff.inventory[loc], lastLoc);
                staff.DiscardAttachment (loc);
                staff.StoreAttachment (attachment, loc);
                if (attachmentUIInsts[loc + staff.maxAttachmentCount] != null) attachmentUIInsts[loc + staff.maxAttachmentCount].GetComponent<AttachmentUIScript> ().GoToPosition (lastPos);
                swapPosInAttachmentUIInsts (loc + staff.maxAttachmentCount, lastLoc + staff.maxAttachmentCount);
                return InventorySlotToPosition (loc);
            }
            else
            {
                // staff slot -> inventory slot
                lastLoc = PositionToStaffSlot (lastPos);
                staff.DetachAttachment (lastLoc);
                staff.AttachAttachment (staff.inventory[loc], lastLoc);
                staff.DiscardAttachment (loc);
                staff.StoreAttachment (attachment, loc);
                if (attachmentUIInsts[loc + staff.maxAttachmentCount] != null) attachmentUIInsts[loc + staff.maxAttachmentCount].GetComponent<AttachmentUIScript> ().GoToPosition (lastPos);
                swapPosInAttachmentUIInsts (loc + staff.maxAttachmentCount, lastLoc);
                return InventorySlotToPosition (loc);
            }
        }
        else
        {
            loc = PositionToStaffSlot (pos);
            if (loc != -1)
            {
                int lastLoc = PositionToInventorySlot (lastPos);
                if (lastLoc != -1)
                {
                    // inventory slot -> staff slot
                    staff.DiscardAttachment (lastLoc);
                    staff.StoreAttachment (staff.attachments[loc], lastLoc);
                    staff.DetachAttachment (loc);
                    staff.AttachAttachment (attachment, loc);
                    if (attachmentUIInsts[loc] != null) attachmentUIInsts[loc].GetComponent<AttachmentUIScript> ().GoToPosition (lastPos);
                    swapPosInAttachmentUIInsts (loc, lastLoc + staff.maxAttachmentCount);
                    return StaffSlotToPosition (loc);
                }
                else
                {
                    // staff slot -> staff slot
                    lastLoc = PositionToStaffSlot (lastPos);
                    staff.DetachAttachment (lastLoc);
                    staff.AttachAttachment (staff.attachments[loc], lastLoc);
                    staff.DetachAttachment (loc);
                    staff.AttachAttachment (attachment, loc);
                    if (attachmentUIInsts[loc] != null) attachmentUIInsts[loc].GetComponent<AttachmentUIScript> ().GoToPosition (lastPos);
                    swapPosInAttachmentUIInsts (loc, lastLoc);
                    return StaffSlotToPosition (loc);
                }
            }
            else
            {
                if (pos.x > -305 && pos.x < 305 && pos.y > -210 && pos.y < 210)
                {
                    // miss
                    return lastPos;
                }
                else
                {
                    int lastLoc = PositionToInventorySlot (lastPos);
                    if (lastLoc != -1)
                    {
                        // inventory slot -> discard
                        staff.DiscardAttachment (lastLoc);
                        attachmentUIInsts[lastLoc + staff.maxAttachmentCount] = null;
                        GameObject.Destroy (attachmentUI);
                    }
                    else
                    {
                        // staff slot -> discard
                        lastLoc = PositionToStaffSlot (lastPos);
                        staff.DetachAttachment (lastLoc);
                        attachmentUIInsts[lastLoc] = null;
                        GameObject.Destroy (attachmentUI);
                    }
                }
            }
        }
        return new Vector3 (0f, 0f, 0f);
    }


    // position <-> slot conversions:
    public int PositionToInventorySlot (Vector3 pos)
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

    public Vector3 InventorySlotToPosition (int slot)
    {
        return new Vector3 (-260f + 26f + 52f * (float) (slot % 10), -24 - 26f - 52f * (float) (slot / 10), 0f);
    }

    public int PositionToStaffSlot (Vector3 pos)
    {
        for (int i = 0; i < staff.maxAttachmentCount; ++i)
        {
            if (pos.x >= staffSlotXCoords[i] - 26 && pos.x <= staffSlotXCoords[i] + 26 && pos.y >= staffSlotYCoords[i] - 26 && pos.y <= staffSlotYCoords[i] + 26)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 StaffSlotToPosition (int slot)
    {
        return new Vector3 (staffSlotXCoords[slot], staffSlotYCoords[slot], 0f);
    }


    // swap positions in attachmentUIInsts
    private void swapPosInAttachmentUIInsts (int a, int b)
    {
        GameObject temp = attachmentUIInsts[a];
        attachmentUIInsts[a] = attachmentUIInsts[b];
        attachmentUIInsts[b] = temp;
    }
}
