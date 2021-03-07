using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldUI : MonoBehaviour
{
    Transform CoinPanel;
    Sequence goldAnimation;

    // Start is called before the first frame update
    void Start()
    {
        AnimationCoin();
    }

    
    void AnimationCoin()
    {
        CoinPanel = GameObject.FindGameObjectWithTag("CoinPanel").transform; //hedefimin ne olduğunu belirttim
        goldAnimation = DOTween.Sequence(); // burada animasyonumu sıfırdan bir sequence olarak belirledim herhangi bir animasyon seçmek isteyebilirim

        goldAnimation.Append(transform.DOMove(CoinPanel.position, 2)
            .SetEase(Ease.OutBounce))//gidiş aimasyonumu seçiyorum
            .OnComplete(() => Destroy(gameObject));//arayüzde kullandığım altın objeleri ulaşınca ulaşması gereken yere yok olsun diyorum
            
    }

}
