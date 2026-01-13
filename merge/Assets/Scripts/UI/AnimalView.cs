using UnityEngine;
using UnityEngine.UI;

public class AnimalView : MonoBehaviour
{
    [SerializeField] private Image image;

    /// <summary>
    /// BoardView에서 호출됨
    /// AnimalInstance → AnimalData → sprite 를 UI에 반영
    /// </summary>
    public void Bind(AnimalData data)
    {
        if (data == null)
        {
            image.sprite = null;
            return;
        }

        image.sprite = data.sprite;
        image.SetNativeSize(); // 선택: 원본 크기 유지
    }
}
