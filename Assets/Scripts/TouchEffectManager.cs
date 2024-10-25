using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    public GameObject TouchEffectOBJ;   // ����Ʈ ������
    public GameObject uiCanvas;         // UI ĵ����

    float TouchTime;
    float defaultTime = 0.05f; // ����Ʈ �ð�

    void Update()
    {
        if ((Input.touchCount == 1 || Input.GetMouseButton(0)) && TouchTime >= defaultTime)
        {
            TouchEffectFun();
            TouchTime = 0;
        }

        TouchTime += Time.deltaTime;
    }

    public void TouchEffectFun()
    {
        RectTransform rt = uiCanvas.GetComponent<RectTransform>();

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, Input.mousePosition, null, out Vector3 Point))
        {
            GameObject Temp = Instantiate(TouchEffectOBJ, Point, Quaternion.identity, uiCanvas.transform);
        }
    }
}