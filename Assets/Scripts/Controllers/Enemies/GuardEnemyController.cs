using UnityEngine;

public class EnemyGuardController : EnemyController // This class manages the guard enemy.
{
    [SerializeField] private MagicStoneController[] _magicStones;

    /// <summary>
    /// Handles enemy death by updating magic stone states and saving progress.
    /// </summary>
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
