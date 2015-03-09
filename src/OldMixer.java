

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
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.SequenceInputStream;
import java.util.*;


public class OldMixer {

	private final int BUFFER_SIZE = 128000;
	ArrayList<AudioInputStream> audioFiles;
	String BLANK_AUDIOFILE = "BlankAudio.wav";
	AudioInputStream blankstream;
	File blankfile;
	AudioFormat blankaudioformat;
	ArrayList<ArrayList<AudioInputStream>> tracks;
	
	public OldMixer(){
	try {
		
		blankfile = new File(BLANK_AUDIOFILE);
		blankaudioformat = AudioSystem.getAudioFileFormat(blankfile).getFormat();
		blankstream = AudioSystem.getAudioInputStream(blankfile.toURI().toURL());
	} catch (Exception e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
	}
	}
	
	public void getTrack(String audioFilePath){
		
		try{
		audioFiles.add(AudioSystem.getAudioInputStream(new File(audioFilePath)));
		}
	
		catch(Exception g){
		
		}
	}
	
	@SuppressWarnings("resource")
	public AudioInputStream makeAClip(AudioInputStream inputStream, int startTime, int endTime)
	{
		try{
		inputStream.mark(0);	
		int lengthOfClip = endTime - startTime;	
		AudioInputStream clipStream;
		AudioFileFormat fileFormat = AudioSystem.getAudioFileFormat(blankfile);
		AudioFormat format = fileFormat.getFormat();
		int bytesPerSecond = format.getFrameSize() * (int)format.getFrameRate();
		//System.out.println(startTime * bytesPerSecond);
		
		//System.out.println(skipAmount(inputStream, startTime * bytesPerSecond));
		 
		  long framesOfAudioToCopy = lengthOfClip * (int)format.getFrameRate();
		  System.out.println(framesOfAudioToCopy);
		  clipStream = new AudioInputStream(inputStream, format, framesOfAudioToCopy);
		  BufferedInputStream bis = new BufferedInputStream(clipStream);
		  clipStream = new AudioInputStream(bis, clipStream.getFormat(), clipStream.getFrameLength()); 
		  clipStream = new CloseShieldAudioInputStream(clipStream); 
		 // clipStream = new AudioInputStream(clipStream, clipStream.getFormat(), clipStream.getFrameLength()); 
		 System.out.println("made a clip of length" + lengthOfClip);
		  
		  return clipStream;
		}
		catch(Exception g){
			System.out.println("failed to make a clip thing here");
			g.printStackTrace();
		return null;	
		}
	}
	
	
	public AudioInputStream makeATrack(ArrayList<AudioInputStream> trackPieces) {

		System.out.println("Size:" + trackPieces.size());
		
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
			blankstream.mark(0);
			AudioInputStream temp = makeAClip(blankstream,0,length);
			blankstream.reset();
			
			 return makeAClip(AudioSystem.getAudioInputStream(new File(BLANK_AUDIOFILE).toURI().toURL()), 0, length);
		} catch (Exception g) {
			g.printStackTrace();
		} 
		return null;
	}
	
	public AudioInputStream makeFinalTrack(ArrayList<Integer> times, AudioInputStream originalTrack){
		ArrayList<AudioInputStream> pieces = new ArrayList<AudioInputStream>();
		boolean isSilent = false;
		AudioFormat format;
		try {
			format = AudioSystem.getAudioFileFormat(blankfile).getFormat();

		int bytesPerSecond = format.getFrameSize() * (int)format.getFrameRate();
		long amountAlreadySkipped = 0;
		
		for(int i=0; i<times.size();i+=2){
				if(times.get(i + 1) != null){
					
					if(isSilent){
						pieces.add(makeBlankClip(times.get(i+1).intValue()-times.get(i).intValue()));
						skipAmount(originalTrack, bytesPerSecond *(times.get(i+1).intValue()-times.get(i).intValue()));
					}
					else {
						pieces.add(makeAClip(originalTrack, times.get(i).intValue(), times.get(i+1).intValue()));
					}
					isSilent = !isSilent;
				}
			}

			
		return makeATrack(pieces);
		
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
