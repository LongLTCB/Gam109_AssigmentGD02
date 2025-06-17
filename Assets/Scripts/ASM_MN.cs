using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using System;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score)
    {
        // sinh viên viết tiếp code ở đây
        if (score < 100)
            return "Đồng";
        else if (score < 500)
            return "Bạc";
        else if (score < 1000)
            return "Vàng";
        else
            return "Kim cương";
        
    }

    public void YC1()
    {
        // sinh viên viết tiếp code ở đây
        int id = ScoreKeeper.Instance.GetID();
        string name = ScoreKeeper.Instance.GetUserName();
        int Score = ScoreKeeper.Instance.GetScore();
        int regionId = ScoreKeeper.Instance.GetIDregion();

        Region region = listRegion.Find(r => r.ID == regionId);

        Players newPlayer = new Players(id, name, Score, region);
        

        listPlayer.Add(newPlayer);

        Debug.Log($"Da luu: {name} - ID: {id} - score: {Score} - region: {region}");
    }
    public void YC2()
    {
        // sinh viên viết tiếp code ở đây
        
        foreach (Players p in listPlayer)
        {
            string rank = calculate_rank(p.Score);
            Debug.Log($"ID: {p.Id}, Name: {p.Name}, Score: {p.Score}, Region: {p.Region.Name}, Rank: {rank}");
        }

    }
    public void YC3()
    {
        // sinh viên viết tiếp code ở đây
        int currentScore = ScoreKeeper.Instance.GetScore();

        foreach (Players p in listPlayer)
        {
            if (p.Score < currentScore)
            {
                Debug.Log($"[YC3] ID: {p.Id}, Name: {p.Name}, Score: {p.Score}, Region: {p.Region.Name}");
            }
        }
    }
    public void YC4()
    {
        // sinh viên viết tiếp code ở đây
        int currentId = ScoreKeeper.Instance.GetID();

        var player = listPlayer.FirstOrDefault(p => p.Id == currentId);
        if (player != null)
        {
            Debug.Log($"[YC4] ID: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region.Name}");
        }
        else
        {
            Debug.Log($"[YC4] Không tìm thấy người chơi có ID = {currentId}");
        }
    }
    public void YC5()
    {
        // sinh viên viết tiếp code ở đây
        var sortedList = listPlayer.OrderByDescending(p => p.Score).ToList();

        foreach (Players p in sortedList)
        {
            Debug.Log($"[YC5] ID: {p.Id}, Name: {p.Name}, Score: {p.Score}, Region: {p.Region.Name}");
        }
    }
    public void YC6()
    {
        // sinh viên viết tiếp code ở đây
        var lowest5 = listPlayer.OrderBy(p => p.Score).Take(5).ToList();

        foreach (Players p in lowest5)
        {
            Debug.Log($"[YC6] ID: {p.Id}, Name: {p.Name}, Score: {p.Score}, Region: {p.Region.Name}");
        }
    }
    public void YC7()
    {
        // sinh viên viết tiếp code ở đây
        Thread thread = new Thread(CalculateAndSaveAverageScoreByRegion);
        thread.Name = "BXH";
        thread.IsBackground = true; // chạy ngầm, tự kết thúc nếu app đóng
        thread.Start();

    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        // sinh viên viết tiếp code ở đây
        var grouped = listPlayer
        .GroupBy(p => p.Region.Name)
        .Select(g => new
        {
            RegionName = g.Key,
            AverageScore = g.Average(p => p.Score)
        });

        string path = "bxhRegion.txt";
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var item in grouped)
            {
                writer.WriteLine($"Region: {item.RegionName} - Average Score: {item.AverageScore:F2}");
            }
        }

        Debug.Log("Đã ghi file " + path);
    }

}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    // sinh viên viết tiếp code ở đây
    public int Id; // Mã nhân vật
    public string Name; // tên nhân vâtj
    public int Score; // điểm số đạt đc
    public Region Region; // vùng quốc gia
    public Players(int id, string name, int score, Region region)
    {
        this.Id = id;
        this.Name = name;
        this.Score = score;
        this.Region = region;
    }
}