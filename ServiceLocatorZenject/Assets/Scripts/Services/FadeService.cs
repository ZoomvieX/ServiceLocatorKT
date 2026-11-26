using UnityEngine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeService : IFadeService
{
    public Tween FadeIn(Image target, float duration)
    {
        if (target == null) return null;

        target.DOKill();
        var color = target.color;
        color.a = 0f;
        target.color = color;
        target.gameObject.SetActive(true);

        return target.DOFade(1f, duration).SetLink(target.gameObject);
    }

    public Tween FadeOut(Image target, float duration)
    {
        if (target == null) return null;

        target.DOKill();
        return target.DOFade(0f, duration)
            .SetLink(target.gameObject)
            .OnComplete(() => target.gameObject.SetActive(false));
    }
}