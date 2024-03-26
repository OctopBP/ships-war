using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class AssetProvider
    {
        public async UniTask<T> LoadAssetAsync<T>(AssetReferenceT<T> reference) where T : UnityEngine.Object
        {
            if (!reference.RuntimeKeyIsValid())
            {
                throw new ArgumentException("LoadAsset<T>: reference is invalid");
            }
            
            var handle = Addressables.LoadAssetAsync<T>(reference);
            await handle.Task;
            return handle.Result;
        }
        
        public async UniTask<T> LoadAssetAsync<T>(AssetReference reference) where T : UnityEngine.Object
        {
            if (!reference.RuntimeKeyIsValid())
            {
                throw new ArgumentException("LoadAsset<T>: reference is invalid");
            }
            
            var handle = Addressables.LoadAssetAsync<T>(reference);
            await handle.Task;
            return handle.Result;
        }
    }
}