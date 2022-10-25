using Arkanoid.Models;

namespace Arkanoid
{
    internal interface IArkanoidRepository
    {
        void LoadGame(ArkanoidModel arkanoidModel);
        void SaveGame(ArkanoidModel arkanoidModel);
    }
}
