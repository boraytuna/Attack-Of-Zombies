using UnityEngine;

public interface IZombie : ICharacter
{
    void TurnHumanIntoZombie(IHuman human);
}
