namespace BulletinBoard.Data.Utils;

public class PasswordCreator
{
    const string symbols ="1234567890!@#$%&*()qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
    string password = "";
    private Random r = new Random();
    
    public string PassCreator(int passLength)
    {
        var t = symbols.ToArray();
        password += t[r.Next(1,symbols.Length)];
        passLength--;
        if (passLength != 0)
        {
            PassCreator(passLength);
        }
        return password;
    }
}