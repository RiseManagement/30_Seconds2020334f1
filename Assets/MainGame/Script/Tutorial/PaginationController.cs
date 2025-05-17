using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaginationController : MonoBehaviour
{
    [Header("表示対象の画像")]
    [SerializeField] private Image imageLeft;
    [SerializeField] private Image imageRight;

    [Header("全ての画像リスト")]
    [SerializeField] private List<Sprite> allSprites;

    [Header("ナビゲーションボタン")]
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;

    [Header("ページインジケーター")]
    [SerializeField] private GameObject paginationDotPrefab;
    [SerializeField] private Transform paginationContainer;

    [Header("ページインジケーターの色")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.gray;

    [Header("シーン遷移ボタン（最後のページ用）")]
    [SerializeField] private Button nextSceneButton;

    private int currentPage = 0;
    private int totalPages;
    private Image[] paginationDots;

    void Start()
    {
        totalPages = Mathf.CeilToInt(allSprites.Count / 2f);
        SetupPaginationDots();
        UpdateUI();

        btnLeft.onClick.AddListener(() => ChangePage(-1));
        btnRight.onClick.AddListener(() => ChangePage(1));

        nextSceneButton.onClick.AddListener(() =>
        {
            SceneTransitions.SceneLaod(SceneTransitions.SceneName.MAINGAMEFIRST);
        });
    }

    void SetupPaginationDots()
    {
        paginationDots = new Image[totalPages];
        for (int i = 0; i < totalPages; i++)
        {
            GameObject dot = Instantiate(paginationDotPrefab, paginationContainer);
            paginationDots[i] = dot.GetComponent<Image>();
        }
    }

    void ChangePage(int direction)
    {
        int nextPage = currentPage + direction;
        if (nextPage >= 0 && nextPage < totalPages)
        {
            currentPage = nextPage;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        int leftIndex = currentPage * 2;
        int rightIndex = leftIndex + 1;

        imageLeft.sprite = (leftIndex < allSprites.Count) ? allSprites[leftIndex] : null;
        imageLeft.gameObject.SetActive(leftIndex < allSprites.Count);

        imageRight.sprite = (rightIndex < allSprites.Count) ? allSprites[rightIndex] : null;
        imageRight.gameObject.SetActive(rightIndex < allSprites.Count);

        for (int i = 0; i < paginationDots.Length; i++)
        {
            paginationDots[i].color = (i == currentPage) ? activeColor : inactiveColor;
        }

        btnLeft.gameObject.SetActive(currentPage > 0);
        btnRight.gameObject.SetActive(currentPage < totalPages - 1);

        nextSceneButton.gameObject.SetActive(currentPage == totalPages - 1);
    }
}
