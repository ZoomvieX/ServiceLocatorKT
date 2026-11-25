using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public interface IFadeService
{
    Tween FadeIn(Image target, float duration);
    Tween FadeOut(Image target, float duration);
}