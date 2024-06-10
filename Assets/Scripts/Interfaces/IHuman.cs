using UnityEngine;

public interface IHuman : ICharacter
{
    void TurnToZombie(IZombie zombie);
    void RunAwayFromZombie();
}
