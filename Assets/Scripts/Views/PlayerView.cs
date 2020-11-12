using UnityEngine;
using Asteroids.Models;
using Asteroids.Managers;
using System.Collections;
using Asteroids.Controllers;

namespace Asteroids.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject playerSpaceShip;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletParent;
        [SerializeField] private GameObject shieldPrefab;
        [SerializeField] private Sprite[] playerBulletSprite;
        private GameObject[] bulletPool;
        private GameObject shield;
        private RaycastHit2D hit;
        private Vector3 tapPos;
        private Vector3 dirVec;
        Vector2 prevAngle;
        float angleChange;
        private float xPos;
        private bool isTouchingScreen;
        private bool isRotating;
        private bool isMovable;
        private float shieldTime;

        void Start()
        {
            isTouchingScreen = false;
            isRotating = false;
            isMovable = false;
            xPos = 0;
            angleChange = 0;
            shieldTime = 0;
        }

        void Update()
        {
            if (GameManager.Instance.currentGameState == GameStateModel.GAME && isMovable)
            {
                shieldTime += Time.deltaTime;
                if (shieldTime >= GameData.shieldTimer)
                {
                    SpawnShield();
                    shieldTime = 0;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    hit = Physics2D.Raycast(tapPos, Vector2.zero);
                    if (hit.collider != null && !isTouchingScreen)
                    {
                        if (hit.collider.tag == "ColliderUI")
                        {
                            isRotating = true;
                            prevAngle = Input.mousePosition;
                            StartCoroutine(ShootBullet());
                        }
                    }
                    if (Input.mousePosition.y < Screen.height / 2 && !isRotating)
                    {
                        if (!isTouchingScreen)
                        {
                            isTouchingScreen = true;
                            StartCoroutine(ShootBullet());
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isTouchingScreen = false;
                    isRotating = false;
                }
                if (isTouchingScreen)
                {
                    MoveShip();
                }
                if (isRotating)
                {
                    RotateShip();
                }
            }
        }

        private void MoveShip()
        {
            tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dirVec = Vector3.Normalize(tapPos - playerSpaceShip.transform.position);
            if (Mathf.Abs(playerSpaceShip.transform.position.x - tapPos.x) > GameData.playerInputMaxDistance)
            {
                xPos += dirVec.x * GameData.spaceShipTouchFollowSpeed * Time.deltaTime;
                playerSpaceShip.transform.position = new Vector2(xPos, playerSpaceShip.transform.position.y);
            }
        }

        private void RotateShip()
        {
            angleChange = Input.mousePosition.x - prevAngle.x;
            playerSpaceShip.transform.Rotate(Vector3.forward, angleChange * GameData.playerRotationRate);
            prevAngle = Input.mousePosition;
        }

        private IEnumerator ShootBullet()
        {
            while ((isRotating || isTouchingScreen) && GameManager.Instance.currentGameState == GameStateModel.GAME)
            {
                BulletSpawn();
                yield return new WaitForSeconds(playerController.GetBulletModel().bulletSpawnRate);
            }
        }

        private void DrawBullet(int bulletIndex, Vector2 bulletDirection)
        {
            bulletPool[bulletIndex].transform.eulerAngles = playerSpaceShip.transform.eulerAngles;
            bulletPool[bulletIndex].transform.position = playerSpaceShip.transform.GetChild(0).transform.position;
            bulletPool[bulletIndex].GetComponent<BulletHandle>().SetBulletData(playerController.GetBulletModel());
            bulletPool[bulletIndex].GetComponent<BulletHandle>().bulletDirection = bulletDirection;
            bulletPool[bulletIndex].GetComponent<SpriteRenderer>().sprite = playerBulletSprite[(int)playerController.GetBulletModel().bulletType];
            bulletPool[bulletIndex].SetActive(true);
        }

        public IEnumerator MoveToStart()
        {
            while (playerSpaceShip.transform.position.y < -1.5f)
            {
                playerSpaceShip.transform.position += Vector3.up * 2.5f * Time.deltaTime;
                yield return null;
            }
            playerSpaceShip.transform.position = new Vector3(0, -1.5f, 0);
            playerSpaceShip.transform.GetChild(1).gameObject.SetActive(true);
            isMovable = true;
        }

        public void ChangeShipSprite(Sprite shipSprite)
        {
            playerSpaceShip.GetComponent<SpriteRenderer>().sprite = shipSprite;
        }

        public void LoadBulletObjectPool()
        {
            shield = Instantiate(shieldPrefab);
            shield.name = "Shield";
            shield.SetActive(false);
            bulletPool = new GameObject[GameData.bulletPool];
            for (int i = 0; i < bulletPool.Length; i++)
            {
                bulletPool[i] = Instantiate(bulletPrefab);
                bulletPool[i].GetComponent<BulletHandle>().DisableBullet();
                bulletPool[i].transform.SetParent(bulletParent.transform);
            }
        }

        public void BulletSpawn()
        {
            switch (playerController.GetBulletModel().bulletType)
            {
                case BulletTypeModel.SINGLE:
                    FireSingleOrBlastBullet();
                    break;
                case BulletTypeModel.DOUBLE:
                    FireDoubleBullet();
                    break;
                case BulletTypeModel.BLAST:
                    FireSingleOrBlastBullet();
                    break;
                default:
                    FireSingleOrBlastBullet();
                    break;
            }

        }

        public void FireSingleOrBlastBullet()
        {
            for (int bulletIndex = 0; bulletIndex < bulletPool.Length; bulletIndex++)
            {
                if (!bulletPool[bulletIndex].activeSelf)
                {
                    DrawBullet(bulletIndex, playerSpaceShip.transform.up);
                    break;
                }
            }
        }

        public void FireDoubleBullet()
        {
            var forwardVector = playerSpaceShip.transform.up;
            var newAngle = new Vector2(forwardVector.x * Mathf.Cos((GameData.tripleBulletAngle * Mathf.PI) / 180) - forwardVector.y * Mathf.Sin((GameData.tripleBulletAngle * Mathf.PI) / 180),
            forwardVector.x * Mathf.Sin((GameData.tripleBulletAngle * Mathf.PI) / 180) + forwardVector.y * Mathf.Cos((GameData.tripleBulletAngle * Mathf.PI) / 180));
            for (int i = 0; i < 2; i++)
            {
                for (int bulletIndex = 0; bulletIndex < bulletPool.Length; bulletIndex++)
                {
                    if (!bulletPool[bulletIndex].activeSelf)
                    {
                        if (i == 0)
                        {
                            DrawBullet(bulletIndex, playerSpaceShip.transform.up);
                        }
                        else
                        {
                            DrawBullet(bulletIndex, newAngle);
                        }
                        break;
                    }
                }
            }
            playerController.SetCurrentBulletType(BulletTypeModel.SINGLE);
        }

        public void SpawnShield()
        {
            shield.transform.position = new Vector2(Random.Range(-2f, 2f), bulletParent.transform.position.y);
            shield.SetActive(true);
        }
    }
}
