using UnityEngine;
using Asteroids.Views;
using Asteroids.Models;

namespace Asteroids.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        private BulletModel bulletModel;

        public void Init()
        {
            bulletModel = new BulletModel();
            SetCurrentBulletType(BulletTypeModel.SINGLE);
            playerView.LoadBulletObjectPool();
        }

        public void SetCurrentBulletType(BulletTypeModel bulletType)
        {
            int bulletTypeIndex = (int)bulletType;
            bulletModel.SetBulletModel(bulletType, GameData.playerBulletSpeed[bulletTypeIndex], GameData.playerBulletPowerReduce[bulletTypeIndex], GameData.playerBulletLifeTime[bulletTypeIndex], GameData.playerBulletSwawnRate[bulletTypeIndex]);
        }

        public void MoveUp()
        {
            StartCoroutine(playerView.MoveToStart());
        }

        public void ChangeShipSprite(Sprite shipSprite)
        {
            playerView.ChangeShipSprite(shipSprite);
        }

        public BulletModel GetBulletModel()
        {
            return bulletModel;
        }
    }
}