using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityForge.AnimCallbacks;

public class Demo : MonoBehaviour
{
    public const string ANIM_NAME = "demo";

    public Animation mAnimation;
    private AnimationClip mAnimClip;
    private AnimationState mAnimState;

    public Button mBtnPlay;
    public Button mBtnPre;
    public Button mBtnNext;
    public Slider mSlider;

    private float mAnimSlider = 0;
    public float AnimSlider
    {
        set
        {
           // if (value >= 0.995f)     //浮点数不准确
             //   mIsPlaying = false;
            mAnimSlider = value;
        }
        get { return mAnimSlider; }
    }

    // Use this for initialization
    void Start()
    {
        mBtnPlay.onClick.AddListener(BtnPlay);
        mBtnPre.onClick.AddListener(BtnPre);
        mBtnNext.onClick.AddListener(BtnNext);
        mSlider.onValueChanged.AddListener(SliderChange);

        mAnimClip = mAnimation.GetClip(ANIM_NAME);
        mAnimState = mAnimation[ANIM_NAME];

        mAnimation.AddClipCallback(ANIM_NAME, 0.5f, CallbackPoint);
    }

    float tempValue = 0;
    void LateUpdate()
    {
        if (mIsPlaying)
        {
            AnimSlider += Time.deltaTime / 100;
        }

        if (mIsPlaying && mSlider != null && mSlider.value != AnimSlider)
        {
            mSlider.value = AnimSlider;
        }

        if (mAnimation != null)
        {
            if (tempValue != AnimSlider)
            {
                mAnimState.speed = 1;
                tempValue = AnimSlider;
                mAnimState.normalizedTime = AnimSlider;
            }
            else
            {
                mAnimState.speed = 0;
            }
        }
    }

    private bool mIsPlaying = false;
    public void BtnPlay()
    {
        mIsPlaying = !mIsPlaying;
    }

    public void BtnPre()
    {
        mSlider.value -= 0.01f;
    }

    public void BtnNext()
    {
        mSlider.value += 0.01f;
    }

    public void SliderChange(float value)
    {
        if (mAnimState == null || mAnimation == null) return;

        if (mAnimSlider != value)
        {
            mAnimState.speed = 1;
            mAnimation.Play();
            mAnimSlider = value;
        }
        else
        {
            mAnimState.speed = 0;
        }
    }

    public void CallbackPoint()
    {
        Debug.Log("Callback in point 2f");
    }
}
