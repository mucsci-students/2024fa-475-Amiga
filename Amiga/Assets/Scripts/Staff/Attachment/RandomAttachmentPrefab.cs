using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttachmentPrefab : MonoBehaviour
{
    /// <summary>
    /// All possible attachments to generate.
    /// </summary>
    public Attachment[] attachmentPrefabs;

    /// <summary>
    /// Randomly return one of the attachments.
    /// </summary>
    /// <returns> the randomly chosen attachment </returns>
    public Attachment GenerateAttachement()
    {
        return attachmentPrefabs[Random.Range(0, attachmentPrefabs.Length)];
    }
}
