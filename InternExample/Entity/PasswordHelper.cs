using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


public static class PasswordHelper
{
    // hash ด้วย PBKDF2
    public static string GeneratePasswordHash(string password)
    {
        // สร้าง salt
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // 128 บิต = 16 ไบต์

        // แปลงรหัสผ่านเป็น hash โดยใช้ PBKDF2 และ HMACSHA256
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password, // รหัสผ่านที่รับมา
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256, // ฟังก์ชัน HMACSHA256
            iterationCount: 100000, // จำนวนรอบการทำงาน
            numBytesRequested: 256 / 8)); // จำนวนไบต์ที่ต้องการ (256 บิต = 32 ไบต์)

        // แปลงค่า salt เป็น Base64 เพื่อเก็บไว้พร้อมกับ hash
        string saltBase64 = Convert.ToBase64String(salt);
        return $"{saltBase64}:{hashed}"; // เก็บทั้ง salt และ hash ไว้ในฐานข้อมูล
    }

    public static bool VerifyPassword(string inputPassword, string storedPasswordHash)
    {
        // แยก salt และ hash ออกจาก string ที่เก็บไว้ในฐานข้อมูล
        var parts = storedPasswordHash.Split(':');
        if (parts.Length != 2)
            return false; // ข้อมูลไม่ถูกต้อง

        string saltBase64 = parts[0];
        string storedHash = parts[1];

        // แปลงค่า salt จาก Base64 กลับเป็น byte[]
        byte[] salt = Convert.FromBase64String(saltBase64);

        // แปลงรหัสผ่านที่รับมาเป็น hash โดยใช้ salt เดิม
        string hashOfInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: inputPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        // ตรวจสอบว่า hash ของ input ตรงกับ hash ที่เก็บไว้หรือไม่
        return string.Equals(hashOfInput, storedHash);
    }

}
