using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    //画像データ
    public Sprite[] LampSprites = new Sprite[5];
    public Sprite TreasureChestSprite;

    MysteryManager mysteryManager;

    // パフォーマンス: 子 SpriteRenderer のキャッシュ、直前に反映したランプ番号を保持
    SpriteRenderer lampSpriteRenderer;
    int lastAppliedLamp = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 1)
        {
            lampSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        if (lampSpriteRenderer == null)
        {
            Debug.LogWarning("[TreasureChest] ランプ用 SpriteRenderer が取得できませんでした。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MysteryClerSet();
    }

    /// <summary>
    /// 謎クリア判定のランプ更新呼出
    /// </summary>
    public void MysteryClerSet()
    {
        if (MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO1])
        {
            SpriteLampUpdate(1);
        }
        if (MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO2])
        {
            SpriteLampUpdate(2);
        }
        if (MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO3A] &&
            MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO3B])
        {
            SpriteLampUpdate(3);
        }
        if (MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO4A] &&
            MysteryManager.MysteryList[(int)MysteryManager.MysteryType.NAZO4B])
        {
            SpriteLampUpdate(4);
        }
    }

    /// <summary>
    /// ランプ画像更新
    /// </summary>
    /// <param name="misteryLampnum">0〜(LampSprites.Length-1)のランプ番号</param>
    public void SpriteLampUpdate(int misteryLampnum)
    {
        // 配列範囲チェック
        if (LampSprites == null || misteryLampnum < 0 || misteryLampnum >= LampSprites.Length)
        {
            Debug.LogWarning("[TreasureChest] 無効なランプ番号: " + misteryLampnum);
            return;
        }

        // 値が変わっていない場合は再描画を避ける（GC削減＋ダーティフラグ抑制）
        if (lastAppliedLamp == misteryLampnum) return;

        if (lampSpriteRenderer == null && transform.childCount > 1)
        {
            lampSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        if (lampSpriteRenderer == null) return;

        lampSpriteRenderer.sprite = LampSprites[misteryLampnum];
        lastAppliedLamp = misteryLampnum;
    }
}
