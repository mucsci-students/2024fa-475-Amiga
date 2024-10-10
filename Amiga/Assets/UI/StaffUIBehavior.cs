using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// THIS IS NO LONGER BEING USED
// TODO: DELETE

public class StaffUIBehavior : MonoBehaviour
{

    [SerializeField] private Staff staff;
    [SerializeField] private List<Sprite> sprites;
    private Image image;

    private float overallWidth = 1064f * 1.5f;
    private float overallHeight = 851f * 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable ()
    {
        image = GetComponent<Image> ();
        //if (staff.maxAttachmentCount == 0) print ("The UI should start disabled when you run the game! --LCC");
        //image.sprite = sprites[staff.maxAttachmentCount - 3];
        RectTransform rect = GetComponent<RectTransform> ();
        rect.anchorMin = new Vector2 (0.5f * (1f - image.sprite.rect.width / overallWidth), 0);
        rect.anchorMax = new Vector2 (0.5f + 0.5f * (image.sprite.rect.width / overallWidth), image.sprite.rect.height / overallHeight);
        rect.sizeDelta = new Vector2 (0f, 0f);
        //print (image.sprite.rect);
    }
}
