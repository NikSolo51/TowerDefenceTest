using System.Linq;
using CodeBase.Services.StaticData;
using CodeBase.Spawner.Monster;
using CodeBase.Spawner.Tower;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.MonsterSpawnerDatas = FindObjectsOfType<MonsterSpawnMarker>()
                    .Select(x =>
                        new MonsterSpawnerData(x.m_spawnInterval, x.m_monsterType,
                            x.transform.position, x.m_target.position))
                    .ToList();

                levelData.TowersData = FindObjectsOfType<TowerSpawnMarker>()
                    .Select(x =>
                        new TowerSpawnerData(x.m_towerType, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}