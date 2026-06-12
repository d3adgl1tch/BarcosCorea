using UnityEngine;
using DG.Tweening;

public class UIAnim : MonoBehaviour
{
    enum animType
    {
        translate,
        rotate,
        scale,
    }
    [SerializeField] private animType currentAnim = animType.translate;
    [SerializeField] private Vector3 translateOffset = Vector3.zero;
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;
    [SerializeField] private float duration = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {    Sequence animSequence = DOTween.Sequence();
        switch (currentAnim)
        {
            case animType.translate:
                animSequence.Append(
                        GetComponent<RectTransform>().DOMove(new Vector3(GetComponent<RectTransform>().position.x + translateOffset.x,
                                                                                    GetComponent<RectTransform>().position.y + translateOffset.y,
                                                                                    GetComponent<RectTransform>().position.z + translateOffset.z),duration,true))
                .AppendInterval(.25f)
                .Append(GetComponent<RectTransform>().DOMove(new Vector3(GetComponent<RectTransform>().position.x - translateOffset.x,
                                                                            GetComponent<RectTransform>().position.y - translateOffset.y,
                                                                            GetComponent<RectTransform>().position.z - translateOffset.z), duration,true))
                .AppendInterval(.25f);
                break;
            case animType.rotate:
                animSequence.Append(
                        GetComponent<RectTransform>().DORotate(rotationOffset, duration))
                    .AppendInterval(.25f)
                    .Append(GetComponent<RectTransform>().DORotate(Vector3.zero, duration));
                       
                break;
            case animType.scale:
                break;

        }
           animSequence.SetLoops(-1);
    }

}
