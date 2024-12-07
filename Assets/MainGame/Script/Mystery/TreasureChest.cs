using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    //画像データ
    public Sprite[] LampSprites = new Sprite[5];
    public Sprite TreasureChestSprite;

    MysteryManager mysteryManager;

    // Start is called before the first frame update
    void Start()
    {
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
    /// <param name="misteryLampnum"></param>
    public void SpriteLampUpdate(int misteryLampnum)
    {
        switch (misteryLampnum)
        {
            case 0:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[0];
                break;
            case 1:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[1];
                break;
            case 2:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[2];
                break;
            case 3:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[3];
                break;
            case 4:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[4];
                break;
            case 5:
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LampSprites[5];
                break;
        }
    }
}
