using AsketHypertext.Models;

namespace AsketHypertext.Services
{
    public interface IAsketParser
    {
        AsketPage Parse(string[] asketPageLines);
    }
}
