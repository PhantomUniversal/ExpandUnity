using UnityEngine;

namespace PhantomEngine
{
    public class TextureResize
    {
        public Texture2D Resize(Texture2D source, float ratio)
        {
            int width = Mathf.RoundToInt(source.width * ratio);
            int height = Mathf.RoundToInt(source.height * ratio);
            return Resize(source, width, height);
        }
        
        public Texture2D Resize(Texture2D source, int width, int height)
        {
            // 성능 최적화를 위해 RenderTexture를 사용합니다.
            RenderTexture rt = RenderTexture.GetTemporary(width, height);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = rt;

            // 원본 텍스처를 RenderTexture에 Blit하여 스케일링 처리
            Graphics.Blit(source, rt);

            // 새로운 Texture2D에 스케일된 결과를 읽어옵니다.
            Texture2D result = new Texture2D(width, height, source.format, false);
            result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            result.Apply();

            // 이전 RenderTexture 설정 복구 및 임시 RenderTexture 해제
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(rt);

            return result;
        }
    }
}