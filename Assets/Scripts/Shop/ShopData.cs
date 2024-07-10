using UnityEngine;

public class ShopData
{
    public int SelectedCharacter;
    public int SelectedBullet;

    public bool Character1;
    public bool Character2;
    public bool Character3;
    public bool Character4;
    public bool Character5;

    public bool Bullet1;
    public bool Bullet2;
    public bool Bullet3;
    public bool Bullet4;
    public bool Bullet5;

    public ShopData(
        int SelectedCharacter,
        int SelectedBullet,
        bool Character1,
        bool Character2,
        bool Character3,
        bool Character4,
        bool Character5,
        bool Bullet1,
        bool Bullet2,
        bool Bullet3,
        bool Bullet4,
        bool Bullet5
    ){
        this.SelectedCharacter = SelectedCharacter;
        this.SelectedBullet = SelectedBullet;
        this.Character1 = Character1;
        this.Character2 = Character2;
        this.Character3 = Character3;
        this.Character4 = Character4;
        this.Character5 = Character5;
        this.Bullet1 = Bullet1;
        this.Bullet2 = Bullet2;
        this.Bullet3 = Bullet3;
        this.Bullet4 = Bullet4;
        this.Bullet5 = Bullet5;
    }
}
    
