

import javax.swing.*;
import javax.swing.table.*;


import java.awt.*;
import java.awt.event.*;
import java.io.File;
import java.text.NumberFormat;
import java.util.ArrayList;

import javax.swing.JComponent;


public class MixerSwing {
	public static JTextField nameText = new JTextField("", 10);

	public static JPanel mixerControlPanel = new JPanel();
	public static JFrame mixerFrame =  new JFrame("MixerSwing");
	public static ArrayList<fileContainer> fileList = new ArrayList<fileContainer>();
	public static File fileBuf;
	
	public static void main(String[] args)
	{
		 JLabel nameLabel = new JLabel ("Name:");
         nameLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
         nameText.setAlignmentX(Component.CENTER_ALIGNMENT);
         
         JButton trackFileButton = new JButton ("Choose File");
         trackFileButton.setAlignmentX(Component.CENTER_ALIGNMENT);
         trackFileButton.addActionListener (new ActionListener () {
             public void actionPerformed (ActionEvent e) { trackButtonAction(); }
             });
         
         JButton addButton = new JButton ("Add Track");
         addButton.setAlignmentX(Component.CENTER_ALIGNMENT);
         addButton.addActionListener (new ActionListener () {
                 public void actionPerformed (ActionEvent e) { addButtonAction(); }
                 });
         
         JButton playButton = new JButton ("Play");
         playButton.setAlignmentX(Component.CENTER_ALIGNMENT);
         playButton.addActionListener (new ActionListener () {
                 public void actionPerformed (ActionEvent e) { playButtonAction(); }
                 });
         
         
         
         JButton stopButton = new JButton ("Stop");
         stopButton.setAlignmentX(Component.CENTER_ALIGNMENT);
         stopButton.addActionListener (new ActionListener () {
                 public void actionPerformed (ActionEvent e) { stopButtonAction(); }
                 });
         
         
         mixerControlPanel.setLayout(new BoxLayout(mixerControlPanel, BoxLayout.PAGE_AXIS));
        
         JPanel trackFrame = new JPanel();
         trackFrame.setLayout(new BorderLayout());
         
         JPanel trackInfoFrame = new JPanel();
         trackInfoFrame.setLayout(new FlowLayout());
         
         JPanel trackButtonPanel = new JPanel();
         trackButtonPanel.setLayout(new FlowLayout());
         
         trackInfoFrame.add(Box.createRigidArea(new Dimension(100,0)));
         trackInfoFrame.add(nameLabel);
         trackInfoFrame.add(nameText);
         trackInfoFrame.add(Box.createRigidArea(new Dimension(100,0)));
         trackButtonPanel.add(trackFileButton);
         trackButtonPanel.add(addButton);
         
         trackFrame.add(trackInfoFrame, BorderLayout.NORTH);
         trackFrame.add(trackButtonPanel, BorderLayout.CENTER);
         
         JPanel buttonFrame = new JPanel();
       
         buttonFrame.add(playButton);
         buttonFrame.add(stopButton);
	
	
         mixerFrame.setLayout(new BorderLayout());
         mixerFrame.getContentPane().add(trackFrame, BorderLayout.NORTH);
         mixerFrame.getContentPane().add (mixerControlPanel, BorderLayout.CENTER);
         mixerFrame.getContentPane().add(buttonFrame, BorderLayout.SOUTH);
         mixerFrame.addWindowListener(new WindowAdapter() {
                 public void windowClosing (WindowEvent e) { System.exit(0); }
                 });
         mixerFrame.pack();
         mixerFrame.setVisible(true);

         
	}
	
	
	public static void addButtonAction()
	{
		
		if(fileBuf != null){
			JLabel name = new JLabel(nameText.getText());
			name.setAlignmentX(Component.CENTER_ALIGNMENT);
			JSlider volume = new JSlider();
			volume.setAlignmentX(Component.CENTER_ALIGNMENT);
		
			final DefaultListModel lm = new DefaultListModel();
			
			final JList<String> testList = new JList<String>(lm);
			
			JPanel toAdd = new JPanel();
			
			FlowLayout fl = new FlowLayout();
			toAdd.setLayout(fl);
			
			fileList.add(new fileContainer(fileBuf,new ArrayList<Integer>()));
			fileBuf = null;
			final int index = fileList.size() - 1;
			
			toAdd.add(name);
			toAdd.add(volume);
			toAdd.add(testList);
			
			JButton addRange = new JButton ("+");
	        addRange.setAlignmentX(Component.LEFT_ALIGNMENT);
	        addRange.addActionListener (new ActionListener () {
	                public void actionPerformed (ActionEvent e) { addRangeButtonAction(lm,index); }
	
					private void addRangeButtonAction(DefaultListModel l,int i) {
						// TODO Auto-generated method stub
						
						
						  NumberFormat formatField = NumberFormat.getIntegerInstance();
						
						  JFormattedTextField startRange = new JFormattedTextField(formatField);
						  startRange.setColumns(5);
						  JFormattedTextField endRange = new JFormattedTextField(formatField);
						  endRange.setColumns(5);
						  
	
					      JPanel myPanel = new JPanel();
					      myPanel.add(new JLabel("Start:"));
					      myPanel.add(startRange);
					      myPanel.add(Box.createHorizontalStrut(15)); // a spacer
					      myPanel.add(new JLabel("End:"));
					      myPanel.add(endRange);
	
					      int result = JOptionPane.showConfirmDialog(null, myPanel, 
					               "Please Enter Start and End Values (seconds)", JOptionPane.OK_CANCEL_OPTION);
					      if (result == JOptionPane.OK_OPTION) {
					    	  
					    	  lm.addElement(startRange.getText()+":"+endRange.getText());
					         
					      }
					      
					      fileList.get(index).ali.add(Integer.parseInt(startRange.getText()));
					      fileList.get(index).ali.add(Integer.parseInt(endRange.getText()));
					      /*
					      System.out.println("Added");
							for (Integer in : fileList.get(index).ali){
								System.out.println(in);
							}
							*/
					
						mixerFrame.pack();
					}
	                });
	        
	        
	        JButton deleteRange = new JButton ("-");
	        deleteRange.setAlignmentX(Component.LEFT_ALIGNMENT);
	        deleteRange.addActionListener (new ActionListener () {
	                public void actionPerformed (ActionEvent e) { deleteRangeButtonAction(testList,lm,index); }
	
					private void deleteRangeButtonAction(JList jl,DefaultListModel l,int index) {
						// TODO Auto-generated method stub
						
						
						 
						int selIndex = jl.getSelectedIndex();
						//System.out.println(selIndex +"THIS");
						if(selIndex > -1){
							l.remove(selIndex);
							selIndex = selIndex *2;
							fileList.get(index).ali.remove(selIndex);
							fileList.get(index).ali.remove(selIndex);
							/*
							System.out.println("Removed");
							for (Integer in : fileList.get(index).ali){
								System.out.println(in);
							}
							*/
							
						}
					
						mixerFrame.pack();
					}
	                });
	        
	        toAdd.add(addRange);
	        toAdd.add(deleteRange);
			mixerControlPanel.add(toAdd);
			mixerFrame.pack();
	        mixerFrame.setVisible(true);
		}
	}
	
	public static void playButtonAction()
	{
		
	}
	
	public static void stopButtonAction()
	{
		
	}
	public static void trackButtonAction(){
		final JFileChooser fc = new JFileChooser();
		int returnVal = fc.showOpenDialog(mixerFrame);
		if(returnVal == JFileChooser.APPROVE_OPTION){
			fileBuf = fc.getSelectedFile();
		}
	}
	
}
