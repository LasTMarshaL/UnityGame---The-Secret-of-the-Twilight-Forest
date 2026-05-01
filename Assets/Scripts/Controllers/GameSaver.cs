using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs; 


public class GameSaver : MonoBehaviour 
{
    private PlayerController _player;

    [SerializeField] private EnemyController[] _enemies;

    [SerializeField] private ChestController[] _chests;

    [SerializeField] private HealtGainPointController[] _hgps;

    [SerializeField] private MagicStoneController[] _magicStones;

    [SerializeField] private BossFightMusic _bossFightMusic;

    [SerializeField] private CameraController _cameraMain;

    private List<string> _allPlayerPrefsKeys = new List<string>();

    private string _allPlayerPrefsKeysAsAString = "";

    private bool isConnectedToServer = true;

    private void OnEnable()
    {
        YG2.onGetSDKData += IsConnectedToServer;

        if (YG2.saves != null)
        {
            IsConnectedToServer();
        }
    }
    private void OnDisable() => YG2.onGetSDKData -= IsConnectedToServer;


    public void Start()
    {
        _player = PlayerController.playerInstance;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            LoadGame();
        }
    }

    public void IsConnectedToServer()
    {
        isConnectedToServer = true;
    }

    public void AddKey(string key)
    {
        if (isConnectedToServer)
        {
            if (!_allPlayerPrefsKeys.Contains(key))
            {
                _allPlayerPrefsKeys.Add(key);
                _allPlayerPrefsKeysAsAString = string.Join(';', _allPlayerPrefsKeys);
                PlayerPrefs.SetString("AllKeys", _allPlayerPrefsKeysAsAString);
                _allPlayerPrefsKeys = new List<string>(_allPlayerPrefsKeysAsAString.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }

    public void RemoveKey()
    {
        if (isConnectedToServer)
        {
            if (PlayerPrefs.HasKey("AllKeys"))
            {
                _allPlayerPrefsKeys = new List<string>(PlayerPrefs.GetString("AllKeys").Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries));
                foreach (string key in _allPlayerPrefsKeys)
                {
                    PlayerPrefs.DeleteKey(key);
                }
                _allPlayerPrefsKeys.Clear();
                PlayerPrefs.DeleteKey("AllKeys");
            }
        }
    }

    public void SaveGamePlayer()
    {
        if (isConnectedToServer)
        {
            if (_player != null)
            {
                AddKey("Scene");
                AddKey("PlayerHealth");
                AddKey("PlayerCoinsQuantity");
                AddKey("PlayerPositionX");
                AddKey("PlayerPositionY");
                AddKey("PlayerLocalScaleX");
                AddKey("PlayerLocalScaleY");
                AddKey("PlayerLocalScaleZ");
                AddKey("PlayerBlockTimer");
                AddKey("PlayerAttackTimer");
                AddKey("PlayerMaxHealth");

                PlayerPrefs.SetInt("NewGame", 0);
                PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);

                PlayerPrefs.SetInt("PlayerHealth", _player.Health.Health);
                PlayerPrefs.SetInt("PlayerMaxHealth", _player.Health.MaxHealth);

                PlayerPrefs.SetInt("PlayerCoinsQuantity", _player.Wallet.Coins);

                PlayerPrefs.SetFloat("PlayerPositionX", _player.transform.position.x);
                PlayerPrefs.SetFloat("PlayerPositionY", _player.transform.position.y);

                PlayerPrefs.SetFloat("PlayerLocalScaleX", _player.LocalScaleX);
                PlayerPrefs.SetFloat("PlayerLocalScaleY", _player.LocalScaleY);
                PlayerPrefs.SetFloat("PlayerLocalScaleZ", _player.LocalScaleZ);

                PlayerPrefs.SetFloat("PlayerAttackTimer", _player.AttackTimer);
                PlayerPrefs.SetFloat("PlayerBlockTimer", _player.BlockTimer);

                PlayerPrefs.Save();
            }
        }
    }

    public void SaveGameEnemy(EnemyController en)
    {
        if (isConnectedToServer)
        {
            AddKey("Enemy" + en.ID.ToString() + "IsKilled");
            PlayerPrefs.SetInt("Enemy" + en.ID.ToString() + "IsKilled", en.EnemyHealth.IsKilled);

            PlayerPrefs.Save();
        }
    }

    public void SaveGameEnemies()
    {
        if (isConnectedToServer)
        {
            foreach (EnemyController en in _enemies)
            {
                if (en != null && en.enabled)
                {
                    AddKey("Enemy" + en.ID.ToString() + "IsKilled");
                    PlayerPrefs.SetInt("Enemy" + en.ID.ToString() + "IsKilled", en.EnemyHealth.IsKilled);

                    PlayerPrefs.Save();
                }
            }

            foreach (EnemyController en in _enemies)
            {
                if (PlayerPrefs.GetInt("Enemy" + en.ID.ToString() + "IsKilled") == 0 && en != null)
                {
                    AddKey("Enemy" + en.ID.ToString() + "Health");
                    AddKey("Enemy" + en.ID.ToString() + "PositionX");
                    AddKey("Enemy" + en.ID.ToString() + "PositionY");
                    AddKey("Enemy" + en.ID.ToString() + "MagicFistAttackTimer");
                    AddKey("Enemy" + en.ID.ToString() + "AttackTimer");
                    AddKey("Enemy" + en.ID.ToString() + "IsAgressive");

                    PlayerPrefs.SetInt("Enemy" + en.ID.ToString() + "Health", en.EnemyHealth.Health);
                    PlayerPrefs.SetFloat("Enemy" + en.ID.ToString() + "PositionX", en.transform.position.x);
                    PlayerPrefs.SetFloat("Enemy" + en.ID.ToString() + "PositionY", en.transform.position.y);
                    PlayerPrefs.SetFloat("Enemy" + en.ID.ToString() + "MagicFistAttackTimer", en.enemyMagicFistTimer);
                    PlayerPrefs.SetFloat("Enemy" + en.ID.ToString() + "AttackTimer", en.enemyAttackTimer);
                    PlayerPrefs.SetInt("Enemy" + en.ID.ToString() + "IsAgressive", en.IsAgressive ? 1 : 0);

                    PlayerPrefs.Save();

                }
            }
        }
    }

    public void SaveChest(ChestController chest)
    {
        if (isConnectedToServer)
        {
            AddKey("Chest" + chest.ID.ToString() + "IsOpened");
            PlayerPrefs.SetInt("Chest" + chest.ID.ToString() + "IsOpened", chest.ChestLogic.IsOpened);

            PlayerPrefs.Save();
        }
    }

    public void SaveChests()
    {
        if (isConnectedToServer)
        {
            foreach (ChestController chest in _chests)
            {
                if (chest != null && chest.enabled)
                {
                    AddKey("Chest" + chest.ID.ToString() + "IsOpened");
                    PlayerPrefs.SetInt("Chest" + chest.ID.ToString() + "IsOpened", chest.ChestLogic.IsOpened);

                    PlayerPrefs.Save();
                }
            }
        }
    }

    public void SaveHealthGainPoint(HealtGainPointController hgp)
    {
        if (isConnectedToServer)
        {
            AddKey("Hgp" + hgp.ID.ToString() + "IsUsed");
            PlayerPrefs.SetInt("Hgp" + hgp.ID.ToString() + "IsUsed", hgp.HealthGainPointLogic.IsUsed);

            PlayerPrefs.Save();
        }
    }

    public void SaveHealthGainPoints()
    {
        if (isConnectedToServer)
        {
            foreach (HealtGainPointController hgp in _hgps)
            {
                if (hgp != null && hgp.enabled)
                {
                    AddKey("Hgp" + hgp.ID.ToString() + "IsUsed");
                    PlayerPrefs.SetInt("Hgp" + hgp.ID.ToString() + "IsUsed", hgp.HealthGainPointLogic.IsUsed);

                    PlayerPrefs.Save();
                }
            }
        }
    }

    public void SaveMagicStone(MagicStoneController magicStone)
    {
        if (isConnectedToServer)
        {
            AddKey("MagicStone" + magicStone.ID.ToString() + "IsFound");
            PlayerPrefs.SetInt("MagicStone" + magicStone.ID.ToString() + "IsFound", magicStone.MagicStoneLogic.IsFound);

            PlayerPrefs.Save();
        }
    }

    public void SaveMagicStones()
    {
        if (isConnectedToServer)
        {
            foreach (MagicStoneController magicStone in _magicStones)
            {
                if (magicStone != null && magicStone.enabled)
                {
                    AddKey("MagicStone" + magicStone.ID.ToString() + "IsFound");
                    PlayerPrefs.SetInt("MagicStone" + magicStone.ID.ToString() + "IsFound", magicStone.MagicStoneLogic.IsFound);

                    PlayerPrefs.Save();
                }
            }
        }    
    }

    public void SaveCamera()
    {
        if (isConnectedToServer)
        {
            AddKey("CameraPositionX");
            AddKey("CameraPositionY");
            AddKey("CameraPositionZ");

            PlayerPrefs.SetFloat("CameraPositionX", _cameraMain.transform.position.x);
            PlayerPrefs.SetFloat("CameraPositionY", _cameraMain.transform.position.y);
            PlayerPrefs.SetFloat("CameraPositionZ", _cameraMain.transform.position.z);

            PlayerPrefs.Save();
        }
    }

    public void SaveLevels()
    {
        if (isConnectedToServer)
        {
            AddKey("PlayerHealthLevel");
            AddKey("PlayerDamageLevel");
            AddKey("PlayerSpeedLevel");

            PlayerPrefs.SetInt("PlayerHealthLevel", _player.Stats.HealthLevel);
            PlayerPrefs.SetInt("PlayerDamageLevel", _player.Stats.DamageLevel);
            PlayerPrefs.SetInt("PlayerSpeedLevel", _player.Stats.SpeedLevel);

            PlayerPrefs.Save();
        }
    }

    public void SaveMusic()
    {
        if (isConnectedToServer)
        {
            AddKey("BossFighting");
            AddKey("MusicTime");

            if (_bossFightMusic.IsBossFighting)
            {
                PlayerPrefs.SetInt("BossFighting", 1);
            }
            else
            {
                PlayerPrefs.SetInt("BossFighting", 0);
            }
            PlayerPrefs.SetFloat("MusicTime", _bossFightMusic.audioSource.time);

            PlayerPrefs.Save();
        }
    }

    public void GlobalSave()
    {
        SaveGamePlayer();
        SaveGameEnemies();
        SaveChests();
        SaveHealthGainPoints();
        SaveMagicStones();
        SaveCamera();
        SaveLevels();
        SaveMusic();
    }

    public void LoadGame()
    {
        if (isConnectedToServer)
        {
            if (_player != null && PlayerPrefs.GetInt("NewGame") == 0)
            {
                if (PlayerPrefs.HasKey("PlayerMaxHealth"))
                {
                    _player.Health.LoadMaxHealth(PlayerPrefs.GetInt("PlayerMaxHealth"));
                }
                if (PlayerPrefs.HasKey("PlayerHealth"))
                {
                    _player.Health.LoadHealth(PlayerPrefs.GetInt("PlayerHealth"));
                }
                if (PlayerPrefs.HasKey("PlayerCoinsQuantity"))
                {
                    _player.Wallet.LoadCoins(PlayerPrefs.GetInt("PlayerCoinsQuantity"));
                }
                if (PlayerPrefs.HasKey("PlayerPositionX") && PlayerPrefs.HasKey("PlayerPositionY"))
                {
                    _player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), 0);
                }
                if (PlayerPrefs.HasKey("PlayerLocalScaleX") && PlayerPrefs.HasKey("PlayerLocalScaleY") && PlayerPrefs.HasKey("PlayerLocalScaleZ"))
                {
                    _player.transform.localScale = new Vector3(PlayerPrefs.GetFloat("PlayerLocalScaleX"), PlayerPrefs.GetFloat("PlayerLocalScaleY"), PlayerPrefs.GetFloat("PlayerLocalScaleZ"));
                }
                if (PlayerPrefs.HasKey("PlayerAttackTimer"))
                {
                    _player.LoadAttackTimerData(PlayerPrefs.GetFloat("PlayerAttackTimer"));
                }
                if (PlayerPrefs.HasKey("PlayerBlockTimer"))
                {
                    _player.LoadBlockTimerData(PlayerPrefs.GetFloat("PlayerBlockTimer"));
                }
                if (PlayerPrefs.HasKey("PlayerHealthLevel"))
                {
                    _player.Stats.LoadHealthLevel(PlayerPrefs.GetInt("PlayerHealthLevel"));
                }
                if (PlayerPrefs.HasKey("PlayerDamageLevel"))
                {
                    _player.Stats.LoadDamageLevel(PlayerPrefs.GetInt("PlayerDamageLevel"));
                }
                if (PlayerPrefs.HasKey("PlayerSpeedLevel"))
                {
                    _player.Stats.LoadSpeedLevel(PlayerPrefs.GetInt("PlayerSpeedLevel"));
                }

                if (PlayerPrefs.HasKey("CameraPositionX") && PlayerPrefs.HasKey("CameraPositionY") && PlayerPrefs.HasKey("CameraPositionZ"))
                {
                    _cameraMain.transform.position = new Vector3(PlayerPrefs.GetFloat("CameraPositionX"), PlayerPrefs.GetFloat("CameraPositionY"), PlayerPrefs.GetFloat("CameraPositionZ"));
                }

                foreach (var en in _enemies)
                {
                    if (en == null) continue;

                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "IsKilled"))
                    {
                        if (PlayerPrefs.GetInt("Enemy" + en.ID.ToString() + "IsKilled") == 1)
                        {
                            en.EnemyDestroy();
                            continue;
                        }
                    }

                    en.enabled = false;

                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "Health"))
                    {
                        en.EnemyHealth.LoadHealth(PlayerPrefs.GetInt("Enemy" + en.ID.ToString() + "Health", en.EnemyHealth.Health));
                    }
                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "PositionX") && PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "PositionY"))
                    {
                        en.transform.position = new Vector3(PlayerPrefs.GetFloat("Enemy" + en.ID.ToString() + "PositionX"), PlayerPrefs.GetFloat("Enemy" + en.ID.ToString() + "PositionY"), 0);
                    }
                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "AttackTimer"))
                    {
                        en.LoadAttackTimerData(PlayerPrefs.GetFloat("Enemy" + en.ID.ToString() + "AttackTimer"));
                    }
                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "MagicFistAttackTimer"))
                    {
                        en.LoadMagicFistTimerData(PlayerPrefs.GetFloat("Enemy" + en.ID.ToString() + "MagicFistAttackTimer"));
                    }
                    if (PlayerPrefs.HasKey("Enemy" + en.ID.ToString() + "IsAgressive"))
                    {
                        if (PlayerPrefs.GetInt("Enemy" + en.ID.ToString() + "IsAgressive") == 1)
                        {
                            en.ChangeAgressiveSate(true);
                        }
                        else if (PlayerPrefs.GetInt("Enemy" + en.ID.ToString() + "IsAgressive") == 0)
                        {
                            en.ChangeAgressiveSate(false);
                        }
                    }

                en.enabled = true;
                }

                foreach (var chest in _chests)
                {
                    if (chest == null) continue;

                    if (PlayerPrefs.HasKey("Chest" + chest.ID.ToString() + "IsOpened"))
                    {
                        if (PlayerPrefs.GetInt("Chest" + chest.ID.ToString() + "IsOpened") == 1)
                        {
                            chest.ChestDestroy();
                            continue;
                        }
                    }
                }

                foreach (var hgp in _hgps)
                {
                    if (hgp == null) continue;

                    if (PlayerPrefs.HasKey("Hgp" + hgp.ID.ToString() + "IsUsed"))
                    {
                        if (PlayerPrefs.GetInt("Hgp" + hgp.ID.ToString() + "IsUsed") == 1)
                        {
                            hgp.HealthGainPointDestroy();
                            continue;
                        }
                    }
                }

                foreach (var magicStone in _magicStones)
                {

                    if (magicStone == null) continue;

                    if (PlayerPrefs.HasKey("MagicStone" + magicStone.ID.ToString() + "IsFound"))
                    {
                        if (PlayerPrefs.GetInt("MagicStone" + magicStone.ID.ToString() + "IsFound") == 1)
                        {
                            magicStone.MagicStoneLogic.WasFound();
                            continue;
                        }
                    }
                }

                if (PlayerPrefs.HasKey("BossFighting"))
                {
                    if (PlayerPrefs.GetInt("BossFighting") == 0)
                    {
                        _bossFightMusic.EndBossFight();
                    }
                    else if (PlayerPrefs.GetInt("BossFighting") == 1)
                    {
                        _bossFightMusic.StartBossFight();
                    }
                    if (PlayerPrefs.HasKey("MusicTime"))
                    {
                        _bossFightMusic.audioSource.time = PlayerPrefs.GetFloat("MusicTime");
                    }
                }
            }
        }
    }

    public void NewGame()
    {
        if (isConnectedToServer)
        {
            RemoveKey();
            ShowAdds();

            PlayerPrefs.SetInt("NewGame", 1);
            SceneManager.LoadScene("Level1");

            PlayerPrefs.Save();
        }
    }

    public void ContinueGame()
    {
        if (isConnectedToServer)
        {
            ShowAdds();
            PlayerPrefs.SetInt("NewGame", 0);
            SceneManager.LoadScene("Level1");

            PlayerPrefs.Save();
        }
    }

    public void ShowAdds()
    {
        YG2.InterstitialAdvShow();
    }
}