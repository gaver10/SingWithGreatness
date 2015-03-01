
import javax.sound.sampled.AudioFileFormat;
import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;



import javax.sound.sampled.UnsupportedAudioFileException;

import java.io.File;
import java.io.IOException;
import java.io.SequenceInputStream;
import java.util.*;

public class Mixer {

	ArrayList<AudioInputStream> audioFiles;
	String BLANK_AUDIOFILE = "placeholder";
	
	public Mixer(){
	
	}
	
	public void getTrack(String audioFilePath){
		
		try{
		audioFiles.add(AudioSystem.getAudioInputStream(new File(audioFilePath)));
		}
	
		catch(Exception g){
		
		}
	}
	
	public AudioInputStream makeAClip(AudioInputStream inputStream, int startTime, int endTime)
	{
		try{
		int lengthOfClip = endTime - startTime;	
		AudioInputStream clipStream;
		AudioFileFormat fileFormat = AudioSystem.getAudioFileFormat(inputStream);
		AudioFormat format = fileFormat.getFormat();
		int bytesPerSecond = format.getFrameSize() * (int)format.getFrameRate();
		  inputStream.skip(startTime * bytesPerSecond);
		  long framesOfAudioToCopy = lengthOfClip * (int)format.getFrameRate();
		  clipStream = new AudioInputStream(inputStream, format, framesOfAudioToCopy);
		return clipStream;
		}
		catch(Exception g){
		return null;	
		}
	}
	
	
	public AudioInputStream makeATrack(ArrayList<AudioInputStream> trackPieces) {
		
		if(trackPieces.size() == 1) {
			return trackPieces.get(0);
		}
		else {
			AudioInputStream temp = trackPieces.get(0);
			trackPieces.remove(0);
			AudioInputStream restOfTrack = makeATrack(trackPieces);
			return new AudioInputStream(new SequenceInputStream(temp, restOfTrack), temp.getFormat(), temp.getFrameLength() + restOfTrack.getFrameLength());
		}
			
	}
	public AudioInputStream	makeBlankClip(int length){
		//To do: return an input stream of silence for specified amount of time
		try {
			return makeAClip(AudioSystem.getAudioInputStream(new File(BLANK_AUDIOFILE)), 0, length);
		} catch (Exception g) {
			g.printStackTrace();
		} 
		return null;
	}
	
	public AudioInputStream makeFinalTrack(ArrayList<Integer> times, AudioInputStream originalTrack){
		ArrayList<AudioInputStream> pieces = new ArrayList<AudioInputStream>();
		boolean isSilent = true;	
		for(int i=0; i<times.size();i++){
				if(times.get(i + 1) != null){
					if(isSilent){
						pieces.add(makeBlankClip(times.get(i+1).intValue()-times.get(i).intValue()));
					}
					else {
						pieces.add(makeAClip(originalTrack, times.get(i).intValue(), times.get(i+1).intValue()));
					}
					isSilent = !isSilent;
				}
			}
		return makeATrack(pieces);
	}
	
}
