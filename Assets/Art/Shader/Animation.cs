using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3DAnimator : MonoBehaviour
{
    public enum AnimationAxis { Rows, Columns }

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private string rowProperty = "_CurrentRow", colProperty = "_CurrentColumn";

    [SerializeField] private AnimationAxis axis;
    [SerializeField] private float animationSpeed = 5f;
    [SerializeField] private int animationIndex = 0;
    [SerializeField] private int frameCount = 56;
    [SerializeField] private int rowCount = 4; 

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        string clipKey, frameKey;
        if (axis == AnimationAxis.Rows)
        {
            clipKey = rowProperty;
            frameKey = colProperty;
        }
        else
        {
            clipKey = colProperty;
            frameKey = rowProperty;
        }

        int frame = (int)(timer * animationSpeed) % frameCount;
        int animationIndex = (int)(timer * animationSpeed / frameCount) % rowCount;

        meshRenderer.material.SetFloat(clipKey, animationIndex);
        meshRenderer.material.SetFloat(frameKey, frame);
    }
}