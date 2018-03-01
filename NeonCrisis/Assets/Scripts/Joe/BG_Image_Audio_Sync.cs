using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BG_Image_Audio_Sync : MonoBehaviour {

    public float rms_value;
    public float db_value;
    public float pitch_value;
    public float frequency_value;

    private const int q_samples = 1024;
    private const float ref_value = 0.1f;
    private const float threshold = 0.02f;

    public float[] samples;
    private float[] spectrum;
    private float f_sample;

    public AudioSource audio_source;

    Vector3 initial_scale;
    Vector3 scale_target;
    public float fade_target, scale_speed, fade_speed;
    [Range(0, 2)]
    public float rms_threshold;

    SpriteRenderer sprite_renderer;
    Color initial_color;
    
    // Use this for initialization
	void Start () {
        initial_scale = this.transform.localScale;

        samples = new float[q_samples];
        spectrum = new float[q_samples];
        f_sample = AudioSettings.outputSampleRate;

        sprite_renderer = GetComponent<SpriteRenderer>();
        initial_color = sprite_renderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        Analyse_Sound();
	}

    //code from https://answers.unity.com/questions/157940/getoutputdata-and-getspectrumdata-they-represent-t.html
    void Analyse_Sound()
    {
        audio_source.GetOutputData(samples, 0);
        float sum = 0;
        for(int i=  0; i < q_samples; i++)
        {
            sum += samples[i] * samples[i];
        }

        rms_value = Mathf.Sqrt(sum / q_samples);
        db_value = 20 * Mathf.Log10(rms_value / ref_value);
        if(db_value < -160)
        {
            db_value = -160;
        }
        audio_source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float max_v = 0;
        int max_n = 0;
        for(int i = 0; i < q_samples; i++)
        {
            if(!(spectrum[i] > max_v) || !(spectrum[i] > threshold))
            {
                continue;
            }
            else
            {
                max_v = spectrum[i];
                max_n = i;
            }
        }
        frequency_value = max_n;
        if(max_n > 0 && max_n < q_samples - 1)
        {
            float dl = spectrum[max_n - 1] / spectrum[max_n];
            float dr = spectrum[max_n + 1] / spectrum[max_n];
            frequency_value += 0.5f * (dr * dr - dl * dl);
        }
        pitch_value = frequency_value * (f_sample / 2) / q_samples;

        Visualise();
    }

    void Visualise()
    {
        if(rms_value > rms_threshold)
        {
            scale_target = initial_scale + initial_scale * (rms_value);
            transform.localScale = Vector3.Lerp(transform.localScale, scale_target, scale_speed * Time.deltaTime);
        }

        sprite_renderer.color = initial_color;
        fade_target = 0.5f + rms_value;
        Color color = initial_color;
        color.a = Mathf.Lerp(initial_color.a, fade_target, fade_speed * Time.deltaTime);
        //initial_color.a = 0.5f + rms_value;
        sprite_renderer.color = initial_color;
    }
}
