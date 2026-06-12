using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

//public class StickerAnim : MonoBehaviour
//{
//    enum animType
//    {
//        translate,
//        rotate,
//        scale,
//    }
//    [SerializeField] private animType currentAnim = animType.translate;
//    [SerializeField] private Vector3 translateOffset = Vector3.zero;
//    [SerializeField] private float duration = 0.25f;
//    [SerializeField] private List<Image> images;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()

//    {
//        foreach (Image image in images)
//        {
//            Sequence animSequence = DOTween.Sequence();
//            switch (currentAnim)
//            {
//                case animType.translate:
//                    animSequence.Append(
//                            image.DOMove(new Vector3(GetComponent<RectTransform>().position.x + translateOffset.x,
//                                                                                        GetComponent<RectTransform>().position.y + translateOffset.y,
//                                                                                        GetComponent<RectTransform>().position.z + translateOffset.z), duration, true))
//                    .AppendInterval(.25f)
//                    .Append(GetComponent<RectTransform>().DOMove(new Vector3(GetComponent<RectTransform>().position.x - translateOffset.x,
//                                                                                GetComponent<RectTransform>().position.y - translateOffset.y,
//                                                                                GetComponent<RectTransform>().position.z - translateOffset.z), duration, true))
//                    .AppendInterval(.25f);
//                    break;
//                case animType.rotate:
//                    animSequence.Append(
//                        GetComponent<RectTransform>().DORotate(GetComponent<RectTransform>().eulerAngles, .25f));
//                    break;
//                case animType.scale:
//                    break;

//            }
//            animSequence.SetLoops(-1);
//        }
//    }

//}
