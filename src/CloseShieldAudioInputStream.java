import java.io.IOException;
import java.io.InputStream;

import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;



public class CloseShieldAudioInputStream extends AudioInputStream {


	    public CloseShieldAudioInputStream(AudioInputStream in) throws Exception, IOException {
	    	
	    	super(in, in.getFormat(), in.getFrameLength());
	       
	    }

	    @Override
	    public void close() throws IOException {
	    	
	    }

	    public void myClose() throws IOException {
	        super.close();
	    }
	}
	
	
