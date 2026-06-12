using DG.Tweening;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] float duration = 15f;
    [SerializeField] float downPause = .5f;
    [SerializeField] float upPause = .5f;

    private Sequence creditSequence;
    private void OnEnable()
    {
        ScrollDown();
    }
    private void OnDisable()
    {
        creditSequence?.Kill();
    }
    void ScrollDown()
    {
        RectTransform viewport = content.parent.GetComponent<RectTransform>();

        float startY = 0f;
        float endY = content.rect.height - viewport.rect.height;

        content.anchoredPosition = new Vector2(0, startY);

        creditSequence = DOTween.Sequence();

        creditSequence
            .AppendInterval(upPause) 
            .Append(content.DOAnchorPosY(endY, duration).SetEase(Ease.Linear))
            .AppendInterval(downPause) 
            .Append(content.DOAnchorPosY(startY, duration).SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }
}
