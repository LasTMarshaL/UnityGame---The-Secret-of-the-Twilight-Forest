using UnityEngine;

public class BossEnemyController : SecretEnemyController 
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

    protected override void Dialog()
    {
        base.Dialog();
        _bossFightMusic.StartBossFight();
    }

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
