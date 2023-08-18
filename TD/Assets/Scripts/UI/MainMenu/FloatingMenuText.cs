using UnityEngine;
using DG.Tweening;

public class FloatingMenuText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().DOMoveY(0.1f, 6.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
