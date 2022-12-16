using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : PostProcessingApplication
{
    [System.Serializable]
    public struct WaveInstance
    {
        public Vector2 loc;
        public float timeStarted;
        public WaveInstance(Vector2 _loc)
        {
            loc = _loc;
            timeStarted = Time.time;
        }

    }
    public static Ripple _;
    public float time = 0;
    public List<WaveInstance> waves = new();

    protected override void Awake()
    {
        base.Awake();
        _ = this;
    }
    public void AddWave(Vector2 loc)
    {
        waves.Add(new WaveInstance(loc));
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var currLoc = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            AddWave(currLoc);

        }
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (waves.Count != 0)
        {
            int width = source.width / 2;
            int height = source.height / 2;
            RenderTextureFormat format = source.format;

            RenderTexture currentSrc = source;
            RenderTexture currentDst = null;
            foreach (var wave in waves)
            {
                currentDst = RenderTexture.GetTemporary(width, height, 0, format);
                material.SetFloat("iTime", Time.time - wave.timeStarted);
                material.SetVector("WaveCentre", wave.loc);

                Graphics.Blit(currentSrc, currentDst, material);
                RenderTexture.ReleaseTemporary(currentSrc);
                currentSrc = currentDst;
            }

            RenderTexture.ReleaseTemporary(currentSrc);
            Graphics.Blit(currentDst, destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}
