using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    [HttpGet("{input}")]
    public string CalGrade(string input) 
    {
        if (int.TryParse(input, out int score)) //ฟังก์ชั่นสำหรับแปลง String ให้เป็น type ปลายทางตามที่ฟังก์ชั่นประกาศอยู่ 
        {
            if (score >= 0 && score <= 100)
            {
                return "Your grade is : " + CalculateGrade(score);
            }
            else
            {
                return ("Please Input number 1-100");
            }
        }
        else 
        {
            string rangeGrade = GetRangeGrade(input.ToUpper());
            if (rangeGrade == null)
            {
                return ("Please Input Your Grade");
            }
            else 
            {
                return "Your range is : " + rangeGrade;
            }
        
        }
    }
    private string CalculateGrade(int score)
    {
        if (score >= 80) return "A";
        else if (score <= 79 && score >= 70) return "B";
        else if (score <= 69 && score >= 60) return "C";
        else if (score <= 59 && score >= 50) return "D";
        else return "F";
    }
    private string GetRangeGrade(string rangeGrade) 
    {
        switch (rangeGrade) 
        {
            case "A" : return "80-100";
            case "B": return "70-79";
            case "C": return "60-69";
            case "D": return "50-59";
            case "F": return "0-49";
            default: return null;
        }
    }
}
