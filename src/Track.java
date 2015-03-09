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
import java.io.SequenceInputStream;
import java.util.*;

public class Track {
	
	File file;
	FileInputStream fis;
	AudioInputStream ais;
	
	
	public Track(String filepath){
		
		file = new File(filepath);
		
		try {
			
		fis = new FileInputStream(file);
		ais = AudioSystem.getAudioInputStream(file.toURI().toURL());
		
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public File getFile(){
		return file;
	}
	
	public AudioInputStream getAudioInputStream(){
		return ais;
	}
	
	public FileInputStream getFileInputStream(){
		return fis;
	}
	
}


