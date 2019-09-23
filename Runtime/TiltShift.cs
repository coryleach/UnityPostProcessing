using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Gameframe.PostProcessing
{
  
  [Serializable]
  [PostProcess(typeof(TiltShiftRenderer), PostProcessEvent.AfterStack, "Custom/TiltShift")]
  public sealed class TiltShift : PostProcessEffectSettings
  {
    [Range(0f, 25f), Tooltip("Max blur size")]
    public FloatParameter maxBlurSize = new FloatParameter { value = 5.0f };
    [Range(0f, 15f), Tooltip("Blur area")]
    public FloatParameter blurArea = new FloatParameter { value = 1.0f };
    [Range(-0.5f, 0.5f), Tooltip("Offset Blur Position")]
    public FloatParameter offset = new FloatParameter { value = 0f };
    [Tooltip("Small Kernel Size")]
    public BoolParameter smallKernelSize = new BoolParameter { value = false };
  }
 
  public sealed class TiltShiftRenderer : PostProcessEffectRenderer<TiltShift>
  {
    public override void Render(PostProcessRenderContext context)
    {
      var sheet = context.propertySheets.Get(Shader.Find("Hidden/Gameframe/PostFX/TiltShift"));
      sheet.properties.SetFloat("_BlurSize", settings.maxBlurSize < 0.0f ? 0.0f : settings.maxBlurSize);
      sheet.properties.SetFloat("_BlurArea", settings.blurArea);
      sheet.properties.SetFloat("_Offset", settings.offset);
      sheet.properties.SetFloat("_Mode", settings.smallKernelSize ? 1 : 0);
      context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
  }
  
}

