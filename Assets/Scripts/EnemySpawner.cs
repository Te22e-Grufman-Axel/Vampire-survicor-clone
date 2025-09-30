using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;


public class EnemySpawner : MonoBehaviour
{
    public EnemyManager enemyManager;
    public List<Texture2D> shapeTextures;
    public void SpawnEnemy(string enemyType)
    {
        EnemyData data = enemyManager.GetEnemyData(enemyType);
        if (data == null) return;

        GameObject enemy = Instantiate(enemyManager.enemyPrefab, SpawnEnemyPos(), Quaternion.identity);

        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        var enemyAttack = enemy.GetComponent<EnemyAttack>();
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();

        enemyHealth.maxHealth = data.MaxHealth;
        enemyHealth.Damageresistance = data.Damageresistance;

        enemyMovement.speed = data.speed;
        enemyMovement.aggression = data.aggression;
        enemyMovement.attackRange = data.attackRange;
        enemyMovement.weaponType = data.weaponstype;

        enemyAttack.damage = data.damage;
        enemyAttack.attackSpeed = data.attackspeed;
        enemyAttack.attackRange = data.attackRange;
        enemyAttack.weaponType = data.weaponstype;


        if (!data.PngOrColour)
        {
            // PNG mode
            string appDataPath = System.IO.Path.Combine(
                Application.persistentDataPath, "UserImages"
            );
            string pngPath = System.IO.Path.Combine(appDataPath, data.pngName);
            StartCoroutine(LoadPngImage(pngPath, sr));
            enemy.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            // Shape + Color mode
            int shapeId = data.shape;
            if (shapeId >= 0 && shapeId < shapeTextures.Count)
            {
                Texture2D tex = shapeTextures[shapeId];

                Sprite sprite = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f),
                    200f
                );
                sr.sprite = sprite;
            }
        }

        enemy.GetComponent<Renderer>().material.color = new Color(data.color.x, data.color.y, data.color.z);
        enemy.transform.localScale = Vector3.one * data.size;
    }

    Vector3 SpawnEnemyPos()
    {

        Camera cam = Camera.main;
        float margin = 4f;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                spawnPos.x = min.x - margin;
                spawnPos.y = Random.Range(min.y - 5, max.y + 5);
                break;
            case 1: // Right
                spawnPos.x = max.x + margin;
                spawnPos.y = Random.Range(min.y - 5, max.y + 5);
                break;
            case 2: // Top
                spawnPos.x = Random.Range(min.x - 5, max.x + 5);
                spawnPos.y = max.y + margin;
                break;
            case 3: // Bottom
                spawnPos.x = Random.Range(min.x - 5, max.x + 5);
                spawnPos.y = min.y - margin;
                break;
        }
        spawnPos.z = 0;

        return spawnPos;
    }
    IEnumerator LoadPngImage(string filePath, SpriteRenderer spriteRenderer)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("PNG file not found: " + filePath);
            yield break;
        }

        string urlPath = "file:///" + filePath.Replace("\\", "/");

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(urlPath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(uwr);

                float pixelsPerUnit = 200f;

                Sprite sprite = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f),
                    pixelsPerUnit
                );

                spriteRenderer.sprite = sprite;
            }
            else
            {
                Debug.LogError("Failed to load PNG: " + uwr.error);
            }
        }
    }

}