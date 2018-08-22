import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.text.StyledDocument;

import java.net.*;
import java.io.*;

// Import the Informa Libraries
import de.nava.informa.core.*;

// Import our classes


public class HTMLViewer extends JInternalFrame implements ActionListener
{
  private ItemIF item;
  private JTextArea text;
  private JButton readArticle = new JButton( "Read Article" );
  private String browser;

  public HTMLViewer( ItemIF item )
  {
    super( item.getTitle(), true, true, true, true );
    try
    {
      // Build the top panel
      JPanel topPanel = new JPanel( new GridLayout( 3, 2 ) );
      //topPanel.add( GUIUtils.getRJPanel( "Channel:" ) );
      String channel = item.getChannel().getTitle();
     // topPanel.add( GUIUtils.getLJPanel( channel ) );
     // topPanel.add( GUIUtils.getRJPanel( "Title:" ) );
     // topPanel.add( GUIUtils.getLJPanel( item.getTitle() ) );
      java.util.Date date = item.getDate();
      if( date != null )
      {
        topPanel.add( GUIUtils.getRJPanel( "Date:" ) );
        topPanel.add( GUIUtils.getLJPanel( date.toString() ) );
      }
      this.getContentPane().add( topPanel, BorderLayout.NORTH );

      // Initialize the editor pane
      this.item = item;
      String description = item.getDescription();
      text = new JTextArea( description );
      text.setLineWrap( true );
      text.setWrapStyleWord( true );
      this.getContentPane().add( new JScrollPane( text ) );

      // Add link button
      this.getContentPane().add( GUIUtils.getRJPanel( readArticle ), BorderLayout.SOUTH );
      readArticle.addActionListener( this );
      // Show the frame
      this.setVisible( true );
      this.setSize( 600, 400 );
    }
    catch( Exception e )
    {
      e.printStackTrace();
    }
  }

  public void actionPerformed( ActionEvent e )
  {
	  JFrame URLpopup = new JFrame();
	  JButton button = new JButton();
	  String URL;
	  int choice;
    if( e.getSource() == this.readArticle )
    {
      String link = this.item.getLink().toString();
      	URLpopup.add(button);
		URLpopup.pack();
		URLpopup.setVisible(true);
		URLpopup.setLocation(600,400);
		choice = JOptionPane.showConfirmDialog(URLpopup, "Would you like to view Article in your Internet Browser?", "Doge", JOptionPane.YES_NO_OPTION);
		URLpopup.dispose();
		if(choice == 0){
			
		        this.browser = "C:\\Users\\Tom\\AppData\\Local\\Google\\Chrome\\Application\\chrome.exe";
		        Runtime rt = Runtime.getRuntime();
		        try {
					Process p = rt.exec( new String[] { browser, link } );
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
		        //readArticle.addActionListener( this );
		        
		      
		}
		
		else
		{
		
      if( link != null && link.length() > 0 )
      {
        try
        {
        	 URL u = new URL(link);
        	JEditorPane jep = new JEditorPane(u);
        	jep.setEditable(false);
        	jep.setPage(u);
        	JScrollPane jsp = new JScrollPane(jep,JScrollPane.VERTICAL_SCROLLBAR_ALWAYS,JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
        	
        	this.setContentPane(jsp);
         // Runtime rt = Runtime.getRuntime();
          //Process p = rt.exec( new String[] { browser, link } );
        }
        catch( Exception ex )
        {
          ex.printStackTrace();
        }
      }
		}
    }
    
  }
}