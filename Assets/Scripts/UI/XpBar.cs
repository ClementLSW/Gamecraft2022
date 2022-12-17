using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Image[] bars;
    public Vector3 minPos, maxPos;
    private void Update()
    {
        var totalXP = GameManager.Player.xpProgress.Select(x => (float)x).Sum();
        //var ratio = totalXP / GameManager.NeededToLevel;
        var normalized = GameManager.Player.xpProgress.Select((e, i) => new { val = (float)((float)e / GameManager.NeededToLevel), element = (Element) i}).OrderBy(e => -e.val).ToArray();
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].color = AssetDB._.elementCol[normalized[i].element].lightTheme;
            float accum = normalized[i].val;
            for (int j = 0; j < i; j++)
                accum += normalized[j].val;
            bars[i].rectTransform.anchoredPosition = Vector3.Lerp(minPos, maxPos, accum);
        }

        //float blueBar = (blueHealth / blueTotal) / 3;
        //float purpleBar = (purpleHealth / purpleTotal) / 3 + blueBar;
        //float yellowBar = (yellowHealth / yellowTotal) / 3 + purpleBar;
        //blueFill.anchoredPosition = Vector3.Lerp(minPos, maxPos, blueBar);
        //purpleFill.anchoredPosition = Vector3.Lerp(minPos, maxPos, purpleBar);
        //yellowFill.anchoredPosition = Vector3.Lerp(minPos, maxPos, yellowBar);
    }
}
