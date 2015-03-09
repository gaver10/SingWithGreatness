
import javax.sound.sampled.AudioFileFormat;
import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;



import javax.sound.sampled.Clip;
import javax.sound.sampled.DataLine;
import javax.sound.sampled.LineUnavailableException;
import javax.sound.sampled.SourceDataLine;
import javax.sound.sampled.UnsupportedAudioFileException;

import java.io.BufferedInputStream;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.SequenceInputStream;
import java.util.*;


public class Mixer {
	byte[] buffer = new byte[1024];
	ArrayList<File> audioFiles;
	private final int BUFFER_SIZE = 128000;
	String BLANK_AUDIOFILE = "BlankAudio.wav";
	AudioInputStream blankstream;
	File blankfile;
	AudioFormat blankaudioformat;
	ArrayList<ArrayList<AudioInputStream>> tracks;
	
	public Mixer(){
		audioFiles = new ArrayList<File>();
		blankfile = new File(BLANK_AUDIOFILE);
	}
	
public void getTrack(String audioFilePath){
		
		try{
		audioFiles.add(new File(audioFilePath));
		}
	
		catch(Exception g){
		
		}
	}
	
public AudioInputStream makeAClip(File file, int startTime, int endTime)
{
	try{
			
		int lengthOfClip = endTime - startTime;	
		AudioInputStream clipStream;
		AudioInputStream tempStream;
		AudioFileFormat fileFormat = AudioSystem.getAudioFileFormat(file);
		AudioFormat format = fileFormat.getFormat();
		int bytesPerSecond = format.getFrameSize() * (int)format.getFrameRate();
		//System.out.println(startTime * bytesPerSecond);
		
		
		 
		  long framesOfAudioToCopy = lengthOfClip * (int)format.getFrameRate();
		//  System.out.println("Frame of audio to copy: " + framesOfAudioToCopy);
		  
		  tempStream = AudioSystem.getAudioInputStream(file);
		  BufferedInputStream bis = new BufferedInputStream(tempStream);
		  tempStream = new AudioInputStream(bis, tempStream.getFormat(), tempStream.getFrameLength());
		  tempStream.skip(startTime * bytesPerSecond);
		  clipStream = new AudioInputStream(tempStream, tempStream.getFormat(), framesOfAudioToCopy);
		  
		  System.out.println("Approx file size: " + bytesPerSecond * file.length());
		  System.out.println("Bytes to be skipped: " + startTime * bytesPerSecond);
		 // System.out.println("Bytes actually skipped: " +skipAmount(clipStream, startTime * bytesPerSecond));
		 // System.out.println("Amount to be skipped: " + skipAmount(clipStream, startTime * bytesPerSecond));
		 // clipStream = new CloseShieldAudioInputStream(clipStream); 
		 // clipStream = new AudioInputStream(clipStream, clipStream.getFormat(), clipStream.getFrameLength()); 
		// System.out.println("made a clip of length" + lengthOfClip);
		  
		  return clipStream;
		}
		catch(Exception g){
			System.out.println("failed to make a clip thing here");
			g.printStackTrace();
		return null;	
		}
	
	
}

public AudioInputStream makeATrack(ArrayList<AudioInputStream> trackPieces) {

	//System.out.println("Size:" + trackPieces.size());
	
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
		System.out.println("make a blank clip of length" + length);
		//blankstream.mark(0);
		//AudioInputStream temp = makeAClip(blankfile,0,length);
	//	blankstream.reset();
		
		 return makeAClip(blankfile, 0, length);
	} catch (Exception g) {
		g.printStackTrace();
	} 
	return null;
}


public AudioInputStream makeFinalTrack(ArrayList<Integer> times, File originalTrack){
	Byte[] bytes = new Byte[(int) originalTrack.length()];
	
	boolean isSilent = false;
	AudioFormat format;
	AudioInputStream temp;
	ByteArrayOutputStream baos = new ByteArrayOutputStream();
	int len;
	try {
		format = AudioSystem.getAudioFileFormat(originalTrack).getFormat();

	int bytesPerSecond = format.getFrameSize() * (int)format.getFrameRate();
	long amountAlreadySkipped = 0;
	int numBytesRead = 0; 
	int bytesToRead = 0;
	for(int i=0; i<times.size();i+=2){
			if(times.get(i + 1) != null){
				bytesToRead = bytesPerSecond * (times.get(i+1) - times.get(i)); 
				if(isSilent){
					temp = makeBlankClip(times.get(i+1).intValue()-times.get(i).intValue());
					while (((numBytesRead < bytesToRead) && (len = temp.read(buffer)) > -1) ) {
					    baos.write(buffer, 0, len);
					    numBytesRead++;
					}
					baos.flush();
				}
				else {
					temp = makeAClip(originalTrack, times.get(i).intValue(), times.get(i+1).intValue());
					while ((numBytesRead < bytesToRead) &&(len = temp.read(buffer)) > -1 ) {
					    baos.write(buffer, 0, len);
					    numBytesRead++;
					}
					baos.flush();
				}
				isSilent = !isSilent;
			}
		}

	ByteArrayInputStream bis = new ByteArrayInputStream(baos.toByteArray());
		
	return new AudioInputStream(bis, AudioSystem.getAudioFileFormat(originalTrack).getFormat(), AudioSystem.getAudioFileFormat(originalTrack).getFrameLength());
	
	}
	 catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
	
}

public int playAudioInputStream(AudioInputStream audio) {
	//make a clip from the audio input stream
	try {
		
		
		SourceDataLine sourceLine = (SourceDataLine) AudioSystem.getLine(new DataLine.Info(SourceDataLine.class, audio.getFormat()));
	    sourceLine.open(audio.getFormat());
	    sourceLine.start();
	    int nBytesRead = 0;
	    byte[] abData = new byte[BUFFER_SIZE];
	    int BytesRead = 0;
	 
	    while (nBytesRead != -1) {
	        try {
	        	BytesRead = nBytesRead;
	            nBytesRead = audio.read(abData);
	        } catch (IOException e) {
	            e.printStackTrace();
	        }
	        if (nBytesRead >= 0) {
	            @SuppressWarnings("unused")
	            int nBytesWritten = sourceLine.write(abData, 0, nBytesRead);
	        }
	    }
	    sourceLine.drain();
	    sourceLine.close();
		
		
		
	
		return BytesRead;
	} catch (Exception g) {
		// TODO Auto-generated catch block
		g.printStackTrace();
		return 0;
	}
	
	//play the clip to default sound device
	
}

public long skipAmount(AudioInputStream ais, long bytestobeskipped)
{
	long bytesSkipped = 0;
	long temp;
	System.out.println("Bytes to be skippeD: "+bytestobeskipped);
	try {
	while(bytesSkipped < bytestobeskipped)
	{
		
		temp = ais.skip(bytestobeskipped);
		bytestobeskipped = bytestobeskipped - temp;
		bytesSkipped= bytesSkipped + temp;
		System.out.println(temp);
	}
	System.out.println("Skipped " + bytesSkipped);
	
	return bytesSkipped;
	} catch (IOException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
		return 0;
	}
}



}

