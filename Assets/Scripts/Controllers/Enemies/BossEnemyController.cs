using UnityEngine;

public class BossEnemyController : SecretEnemyController // This class manages the boss enemy.
{
    [SerializeField] private MagicStoneController[] _magicStones;

    [SerializeField] private BossFightMusic _bossFightMusic;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Displays the dialog and initiates boss fight music.
    /// </summary>
    protected override void Dialog()
    {
        base.Dialog();
        _bossFightMusic.StartBossFight();
    }

    /// <summary>
    /// Handles enemy death by ending the boss fight music, updating the state of all magic stones, saving their status,
    /// and performing a global save.
    /// </summary>
    protected override void EnemyDie()
    {
        base.EnemyDie();
        _bossFightMusic.EndBossFight();

        foreach (var magicStone in _magicStones)
        {
            if (magicStone == null) continue;

            magicStone.MagicStoneLogic.WasFound();
            gameSaver.SaveMagicStone(magicStone);
        }
        gameSaver.GlobalSave();
    }
}
