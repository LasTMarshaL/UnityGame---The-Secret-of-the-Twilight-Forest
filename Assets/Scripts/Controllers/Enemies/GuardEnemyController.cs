using UnityEngine;

public class EnemyGuardController : EnemyController
{
    [SerializeField] private MagicStoneController[] _magicStones;

    protected override void EnemyDie()
    {
        base.EnemyDie();
        foreach (MagicStoneController magicStone in _magicStones)
        {
            magicStone.MagicStoneLogic.WasFound();
            gameSaver.SaveMagicStone(magicStone);
        }
        gameSaver.GlobalSave();
    }
}
