
import javax.sound.sampled.AudioFileFormat;
import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;



import javax.sound.sampled.Clip;
import javax.sound.sampled.LineUnavailableException;
import javax.sound.sampled.UnsupportedAudioFileException;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.SequenceInputStream;
import java.util.*;

public class Main {

	public static String TESTAUDIO1 = "test1.wav";
	public static String TESTAUDIO2 = "test2.wav";
	public static String TESTAUDIO3 = "Journey1.wav";
	public static String TESTAUDIO4 = "abcs.wav";
	public static ArrayList<AudioInputStream> audioFiles;
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		try {
			Mixer mixer = new Mixer();
			mixer.getTrack(TESTAUDIO1);
			mixer.getTrack(TESTAUDIO2);
			mixer.getTrack(TESTAUDIO3);
			mixer.getTrack(TESTAUDIO4);
			mixer.getTrack("BlankAudio.mp3");
			
			//mixer.playAudioInputStream(AudioSystem.getAudioInputStream(new File("test1.wav")));
			
			ArrayList<Integer> times = new ArrayList<Integer>();
			times.add(new Integer(0));
			times.add(new Integer(120));
			times.add(new Integer(120));
			times.add(new Integer(150));
			times.add(new Integer(150));
			times.add(new Integer(240));

			//AudioInputStream ais = AudioSystem.getAudioInputStream(new File(TESTAUDIO3).toURI().toURL());
			//BufferedInputStream bufferedInputStream = new BufferedInputStream(ais);
			//ais = new AudioInputStream(bufferedInputStream, ais.getFormat(), ais.getFrameLength());
			//mixer.makeFinalTrack(times, new File(TESTAUDIO3));
			//mixer.makeFinalTrack(times, new File(TESTAUDIO4));
			ArrayList<AudioInputStream> allTracks = new ArrayList<AudioInputStream>();
			allTracks.add(mixer.makeFinalTrack(times, new File(TESTAUDIO3)));
			//allTracks.add(mixer.makeFinalTrack(times, new File(TESTAUDIO4)));
			mixer.playAudioInputStream(new MixingAudioInputStream(allTracks.get(0).getFormat(),allTracks));
			 
			
			
		} catch (Exception g) {
			// TODO Auto-generated catch block
			g.printStackTrace();
		}
		
	
		
	}

	
	
	
}
