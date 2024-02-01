/* Icon Data Base Notes:
 * Itemlarýn id leri ile iconlarý ayný isimde olmalýdýr.
 * iconlar bir listenin içinde "ItemIcons" id lere göre sýralý olacakktýr.
 * bu dosyadan iconlar ýd ile çeklir.   
 * DATABASE BUILD YAPILMASI GEREKLÝDÝR
 * -Bu database Build almadan oluþturulmalý Start vb den cýkarýlmalýdýr.    
 */


using Fusion;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemIconDatabase : MonoBehaviour
{
    [SerializeField] string folderPath = "_gfx/Icons";
    [SerializeField] List<Sprite> sprites = new();
    [SerializeField] List<Texture2D> text2ds = new();

    [field: SerializeField]
    public List<ItemIcon> ItemIcons { get; private set; } = new();



    [ContextMenu("SetItemIconDatabse")]
    void SetIconDatabase()
    {
        sprites.Clear();
        text2ds.Clear();
        ItemIcons.Clear();
        var tempfolderPath = Application.dataPath + folderPath;
        // Klasörün içindeki tüm dosyalarý listeleyin.
        DirectoryInfo directory = new DirectoryInfo(tempfolderPath);
        FileInfo[] files = directory.GetFiles();
        List<FileInfo> newfiles = new();
        // Dosyalarý sprite'lara dönüþtürün.
        for (int i = 0; i < files.Length; i++)
        {

            if (files[i].Extension == ".png")
            {
                newfiles.Add(files[i]);
                sprites.Add(LoadNewSprite(files[i].FullName));
            }
        }

        if (sprites.Count <= 0) return;

        for (int i = 0; i < sprites.Count; i++)
        {
            ItemIcon ic = new();
            try
            {
                ic.iconId = int.Parse(newfiles[i].Name.Replace(".png", ""));
                //Debug.Log(newfiles[i].Name.Replace(".png", "") + " to int = " + int.Parse(newfiles[i].Name.Replace(".png", "")));
                ic.IconSprite = sprites[i];
                ItemIcons.Add(ic);
            }
            catch
            {
                Debug.LogWarning(
                    newfiles[i].FullName + "<color=red>" +
                    "\nInvalid item_icon file name. please make item_icon file names int and and unique!!!! (exp=  123.png)" +
                    "</color>");
            }
        }

    }

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 64.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;
        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
            {
                text2ds.Add(Tex2D);
                return Tex2D;                 // If data = readable -> return texture
            }
        }
        return null;                     // Return null if load failed
    }
}


[System.Serializable]
public class ItemIcon
{
    public int iconId; // icon id ile itemidleri ayný olmak zorunda.
    public Sprite IconSprite;
}