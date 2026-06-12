// ItemDataBase の ID 群を名前付き定数で表現する。
// gimmicks.md の対応表を C# 側に持ち込んだもの。
// const int なので switch の case 値にもそのまま使える。
//
// 規則: <カテゴリ><状態> (例: TankWithCylinder = 水槽にシリンダー挿入後)
//       オン/オフ系は <カテゴリ>Off / <カテゴリ>On に統一
public static class MysteryIds
{
    // 絵画
    public const int APaintingBefore = 0;
    public const int APaintingAfter  = 1;
    public const int BlackLight      = 12;
    public const int BPaintingBefore = 22;
    public const int BPaintingAfter  = 35;
    public const int VaseBefore      = 28;
    public const int VaseAfter       = 29;

    // 机/パズル
    public const int DeskClosed   = 2;
    public const int DeskOpen     = 3;
    public const int Puzzle       = 4;
    public const int PuzzleSolved = 5;
    public const int Paint        = 34;

    // ピアノ
    public const int Piano = 10;

    // 台座
    public const int PedestalEmpty  = 14;
    public const int PedestalLoaded = 15;
    public const int PedestalButton = 13;
    public const int Apple          = 39;

    // 水槽
    public const int TankFull         = 16;
    public const int TankWithCylinder = 17;
    public const int TankEmpty        = 18;
    public const int TankHole         = 19;
    public const int TankHoleOpen     = 20;
    public const int Cylinder         = 11;

    // ランプ (B側)
    public const int BlueLampOff    = 31;
    public const int BlueLampOn     = 32;
    public const int YellowLampOff  = 37;
    public const int YellowLampOn   = 38;
    public const int RedLampOff     = 42;
    public const int RedLampOn      = 43;
    public const int LampHint       = 36;
    public const int WaterSwitchOff = 33;
    public const int WaterSwitchOn  = 47;

    // オルゴール
    public const int MusicBoxEmpty  = 40;
    public const int MusicBoxLoaded = 41;

    // 脱出
    public const int KeyA           = 21;
    public const int KeyholeAClosed = 8;
    public const int KeyholeAOpen   = 9;
    public const int DoorAClosed    = 6;
    public const int DoorAOpen      = 7;
    public const int KeyB           = 30;
    public const int KeyholeBClosed = 26;
    public const int KeyholeBOpen   = 27;
    public const int DoorBClosed    = 24;
    public const int DoorBOpen      = 25;

    // 宝箱・ヒント
    public const int TreasureChestClosed = 44;
    public const int TreasureChestOpen   = 45;
    public const int Nazo1Hint           = 23;
}
