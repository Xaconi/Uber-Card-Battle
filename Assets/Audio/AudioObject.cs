using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour {
	
	public AudioClip music;
	public AudioClip click;
	public AudioClip musicBattle;
	public AudioClip musicEdit;
	public AudioClip sniperShot;
	public int fadeTime = 1;

	// Use this for initialization
	void Start () {
		audio.PlayOneShot(music, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void playClick(){
		audio.PlayOneShot(click, 1.0f);
	}
	
	public void playBattleMusic(){
		audio.Stop();
		audio.volume = 1;
		audio.PlayOneShot(musicBattle, 0.8f);
	}
	
	public void playMenuMusic(){
		audio.Stop();
		audio.volume = 1;
		audio.PlayOneShot(music, 0.8f);
	}
	
	public void playEditMusic(){
		audio.Stop();
		audio.volume = 1;
		audio.PlayOneShot(musicEdit, 0.8f);
	}
	
	public void playSniperShot(){
		audio.PlayOneShot(sniperShot, 0.5f);
	}
	
	public void volumeOut() { 
		audio.volume = 0;
	}
	
	public void FadeSoundLow() { 
	    if(fadeTime == 0) { 
	        audio.volume = 0;
	        return;
	    }
	    StartCoroutine(_FadeSoundLow()); 
	}
	
	public IEnumerator _FadeSoundLow() {
	    float t = fadeTime;
	    while (t > 0) {
	        yield return null;
	        t-= Time.deltaTime;
	        audio.volume = t/fadeTime;
	    }
	    yield break;
	}
	
	public void FadeSoundHigh() { 
	    if(fadeTime == 0) { 
	        audio.volume = 0;
	        return;
	    }
	    StartCoroutine(_FadeSoundHigh()); 
	}
	
	public IEnumerator _FadeSoundHigh() {
	    float t = fadeTime;
	    while (t > 0) {
	        yield return null;
	        t-= Time.deltaTime;
	        audio.volume = t/fadeTime;
	    }
	    yield break;
	}
}
