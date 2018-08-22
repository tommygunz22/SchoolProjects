import java.awt.*;

import gov.nasa.worldwind.*;
import gov.nasa.worldwind.awt.WorldWindowGLCanvas;
import gov.nasa.worldwind.event.SelectEvent;
import gov.nasa.worldwind.event.SelectListener;
import gov.nasa.worldwind.geom.Angle;
import gov.nasa.worldwind.geom.Position;
import gov.nasa.worldwind.layers.IconLayer;
import gov.nasa.worldwind.layers.LayerList;
import gov.nasa.worldwind.render.UserFacingIcon;
import gov.nasa.worldwind.render.WWIcon;

import java.awt.event.*;
import java.awt.color.*;
import java.io.*;

import de.nava.informa.utils.poller.Poller;
import de.nava.informa.utils.poller.PollerObserverIF;

import javax.swing.*;
import javax.swing.Timer;
import javax.swing.event.*;
import javax.swing.table.TableCellRenderer;
import javax.swing.tree.*;

import org.jdom.*;
import org.jdom.input.*;
import org.jdom.output.*;
import org.jdom.input.SAXBuilder;

import de.nava.informa.core.ChannelIF;
import de.nava.informa.core.ItemIF;
import de.nava.informa.core.ParseException;
import de.nava.informa.core.WithTitleMIF;
import de.nava.informa.impl.basic.Category;
import de.nava.informa.impl.basic.Channel;
import de.nava.informa.impl.basic.ChannelBuilder;
import de.nava.informa.impl.basic.Feed;
import de.nava.informa.parsers.FeedParser;

import java.net.MalformedURLException;
import java.net.URL;
import java.util.*;
import java.sql.*;
public class RSSManagerFrame extends JInternalFrame implements TreeSelectionListener, ActionListener,MouseListener {

	private JTree tree;
	private DefaultTreeModel treemodel;
	private DefaultMutableTreeNode root = new DefaultMutableTreeNode("RSS Feeds");
	private Map<String,String> channelMap = new TreeMap();
	private Map catMap = new TreeMap();
	private int numClicks = 0;
	public String title;
	public int delay = 1000 * 30 * 6;
	private JTextField text = new JTextField();
	private JMenuItem URLbutton = new JMenuItem("Add Subscription");
	private JFrame Popup = new JFrame();
	private JFrame URLpopup = new JFrame();
	private JButton button = new JButton();
	//private JButton SaveButton = new JButton();
	//private JButton LoadButton = new JButton();
	private String URL;
	private String URLName;
	public Timer timer;  
	public WorldWindowGLCanvas wwd = new WorldWindowGLCanvas();
     public Model model;
     public MouseListener myListener = null;
     public BufferedReader br = null;
     public String line = "";
     public int size;
     public Object read;
     public String csvFile = "/Users/Tom/workspace/323 RSS Map Feed/src/uslocations2.csv";
     public java.net.URL link = null;
     public String cvsSplitBy = ",";
     ActionListener taskPerformer = new ActionListener()
     {
    	 public void actionPerformed(ActionEvent evt)
    	 {
    		 //TreePath path = tree.getSelectionPath();
    		 Refresh();
    		 
    		 
    	 }
     };
	public RSSFeedTable tableModel = new RSSFeedTable();
	public JTable table = new JTable( tableModel )
	
	{
		@Override
		  public Component prepareRenderer(TableCellRenderer renderer,int row,int column)
        {
            Component comp=super.prepareRenderer(renderer,row, column);
           int modelRow=convertRowIndexToModel(row);
          // System.out.println(modelRow);
           if(!(boolean) table.getValueAt(modelRow, 3))//isRowSelected(modelRow))
               comp.setForeground(Color.blue);
           else
               comp.setForeground(Color.black);
           return comp;
        }
	};
	private JMenuItem UnSubbutton = new JMenuItem("|   UnSubscribe   ");
	private JMenuItem RenameButton = new JMenuItem("| Rename Subscription ");
	private JMenuItem FindbyMap = new JMenuItem("|  FindbyMap ");
	private JMenuItem SetRefresh = new JMenuItem("|  Change Refresh Time ");
	private JMenuItem SaveButton = new JMenuItem("|  Save ");
	private JMenuItem LoadButton = new JMenuItem("|  Load ");
	private JFrame HTMLWindow = new JFrame();
	  private Set listeners = new TreeSet();
	  
	  
	 public RSSManagerFrame()
	  {
	    super( "RSS Feed Manager",true,false,true,true);
	  this.setSize(1000, 1000);
	    // Build our tree
	     treemodel = new DefaultTreeModel( root );
	    tree = new JTree( treemodel );
	   // RSSFeedTable tableModel = new RSSFeedTable();
		 // JTable table = new JTable( tableModel );
	   tree.addTreeSelectionListener(this);
	   this.table.addMouseListener( this);
	  timer = new Timer(delay,taskPerformer);
	  
	timer.start();
	  
		JMenuBar menuBar = new JMenuBar();
		this.setJMenuBar(menuBar);
		menuBar.add(URLbutton);
		URLbutton.addActionListener(this);
		menuBar.add(UnSubbutton);
		UnSubbutton.addActionListener(this);
		menuBar.add(RenameButton);
		RenameButton.addActionListener(this);
		menuBar.add(FindbyMap);
		FindbyMap.addActionListener(this);
		menuBar.add(SetRefresh);
		SetRefresh.addActionListener(this);
		menuBar.add(SaveButton);
		SaveButton.addActionListener(this);
		menuBar.add(LoadButton);
		LoadButton.addActionListener(this);
		
	
		
	
	    // Build our frame
	    this.getContentPane().add( new JSplitPane( JSplitPane.HORIZONTAL_SPLIT, 
	                          new JScrollPane( tree), 
	                          new JScrollPane(table ) ) );
	  
	    // Show the frame
	    Font newfont = new Font("Serif", Font.BOLD, 12);
		  this.getContentPane().setBackground(Color.CYAN);
		  this.setFont(newfont);
	    this.setVisible( true );
	    this.setSize( 600, 400 );
	    this.setLocation( 1, 1 );
	
	    
	 
	    
	  }
	 
	 
	 public void Refresh()
	 {
		 this.tableModel.list.clear();
		 this.tableModel.fireTableDataChanged();
		
		 timer.restart();
		 TreePath path = tree.getSelectionPath();
		 //this.channelMap.get(path.toString());
		  URL url;
			try {
				
				url = new URL(this.channelMap.get(path.getLastPathComponent().toString()));
				  ChannelIF channel = FeedParser.parse( new ChannelBuilder(), url );
				  //this.channelMap.put(node_1.toString(),URL);
			      
			      //System.out.println("node_1 name is " + node_1);
				     //System.out.println( "Channel selected: " + channel.getTitle() );
				     //this.tableModel.setChannel( channel );
			      
				  //PollerObserverIF observer = new UpdateRSS();
			      //poll.registerChannel(channel,1*30*1000);
			     System.out.println( "Channel: " + channel.getTitle() );
			     System.out.println( "Description: " + channel.getDescription() );
			      System.out.println( "PubDate: " + channel.getPubDate() );
			      Collection items = channel.getItems();
			      for( Iterator i=items.iterator(); i.hasNext(); )
			      {
			        ItemIF item = ( ItemIF )i.next();
			       tableModel.addItem(item);
			        System.out.println( "Channel: " + channel.getTitle() );
				     System.out.println( "Description: " + channel.getDescription() );
				      System.out.println( "PubDate: " + channel.getPubDate() );
				      System.out.println("Refresh Time: " + channel.getUpdatePeriod());
			        //System.out.println(tableModel.getValueAt(1, 0));
			       System.out.println( item.getTitle() );
			       /* System.out.println( "\t" + item.getDescription() );
			        System.out.println( "Categories: " + item.getCategories() );
			        System.out.println( "Date: " + item.getDate() );
			        System.out.println( "Link: " + item.getLink() + "\n" );*/
			      }
			      this.tableModel.fireTableDataChanged();
			      URLpopup.setLocation(600,400);
					URLpopup.add(button);
					URLpopup.pack();
					URLpopup.setVisible(true);
					 JOptionPane.showMessageDialog(URLpopup,"Your RSS Feeds have been updated");
					 URLpopup.dispose();
					 URLpopup.setVisible(false);
				
			} catch (MalformedURLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (IOException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (ParseException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} 
		 
	 }
	 
	 
		public void valueChanged( TreeSelectionEvent e ) 
		 
		  {
			 System.out.println("In valueChanged");
			 TreeNode selectedNode;
			 this.tableModel.list.clear();
			 //System.out.println(e.getOldLeadSelectionPath().getLastPathComponent());
			 
			 if(e.getNewLeadSelectionPath().getLastPathComponent() != null)
			 {
				 //selectedNode = (TreeNode) e.getOldLeadSelectionPath().;
				 selectedNode = ( TreeNode )e.getNewLeadSelectionPath().getLastPathComponent();
			 }
			 else
			 {
		     selectedNode = ( TreeNode )e.getOldLeadSelectionPath().getParentPath();
		     
		     tableModel.fireTableStructureChanged();
			 }
		 
			 if(selectedNode.isLeaf())
			 {
			  URL url;
			try {
				url = new URL(this.channelMap.get(selectedNode.toString()));
				  ChannelIF channel = FeedParser.parse( new ChannelBuilder(), url );
				  //this.channelMap.put(node_1.toString(),URL);
			      
			      //System.out.println("node_1 name is " + node_1);
				     //System.out.println( "Channel selected: " + channel.getTitle() );
				     //this.tableModel.setChannel( channel );
			      
				  //PollerObserverIF observer = new UpdateRSS();
			      //poll.registerChannel(channel,1*30*1000);
			     System.out.println( "Channel: " + channel.getTitle() );
			     System.out.println( "Description: " + channel.getDescription() );
			      System.out.println( "PubDate: " + channel.getPubDate() );
			      Collection items = channel.getItems();
			      for( Iterator i=items.iterator(); i.hasNext(); )
			      {
			        ItemIF item = ( ItemIF )i.next();
			       tableModel.addItem(item);
			        System.out.println( "Channel: " + channel.getTitle() );
				     System.out.println( "Description: " + channel.getDescription() );
				      System.out.println( "PubDate: " + channel.getPubDate() );
				      System.out.println("Refresh Time: " + channel.getUpdatePeriod());
			        //System.out.println(tableModel.getValueAt(1, 0));
			       System.out.println( item.getTitle() );
			       /* System.out.println( "\t" + item.getDescription() );
			        System.out.println( "Categories: " + item.getCategories() );
			        System.out.println( "Date: " + item.getDate() );
			        System.out.println( "Link: " + item.getLink() + "\n" );*/
			      }
			      this.tableModel.fireTableDataChanged();
				
			} catch (MalformedURLException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (IOException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} catch (ParseException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			} 
			 }
			 
			 else
			 {
				 this.tableModel.list.clear();
				 this.tableModel.fireTableDataChanged();
			 }
		     
		
		    	   /* if( selectedNode.isLeaf() )
		    	    {
		    	    	System.out.println("in if statment");
		    	    	//ItemIF doge = (ItemIF) tableModel.list.get(1);
		    	    	//tableModel.list.add(doge);
		    	    tableModel.fireTableDataChanged();
		    	    
		    	    	//tableModel.fireTableStructureChanged();
		    	    
		    	    	//tableModel.fireTableRowsInserted(tableModel.list.size() - 1, tableModel.list.size() - 1);
		    	        //tableModel.fireTableRowsUpdated( 0, tableModel.list.size() - 1 );
		    	     // ChannelIF chan = (ChannelIF)doge;
		    	      //System.out.println( "Channel selected: " + chan.getTitle() );
		    	      //this.tableModel.setChannel( chan );
		    	      // Continue...
		      
		      // Continue...
		    }*/
		    	   
		  }
	 

	
  
	
	public void actionPerformed (ActionEvent e)
	{
		
		if(e.getSource()== URLbutton)
		{
			URLpopup.setLocation(600,400);
			URLpopup.add(button);
			URLpopup.pack();
			URLpopup.setVisible(true);
			URL = JOptionPane.showInputDialog(URLpopup,"Enter URL", null);
			URLpopup.dispose();
			URLpopup.setVisible(true);
			URLName = JOptionPane.showInputDialog(URLpopup,"Enter Name of RSS Feed",null);
			URLpopup.dispose();
			
			//root.add(node_1);
			//treemodel.
			//treemodel.insertNodeInto(node_1, root, root.getChildCount());
			//Poller poll = new Poller();
		 
			try
			{
			  URL url = new URL(URL ); 
			  DefaultMutableTreeNode node_1 = new DefaultMutableTreeNode(URLName);
		      ChannelIF channel = FeedParser.parse( new ChannelBuilder(), url );
		      this.channelMap.put(node_1.toString(),URL);
		      
				
				treemodel.insertNodeInto(node_1, root, root.getChildCount());
		      System.out.println("node_1 name is " + node_1);
			     //System.out.println( "Channel selected: " + channel.getTitle() );
			     //this.tableModel.setChannel( channel );
		      
			  //PollerObserverIF observer = new UpdateRSS();
		      //poll.registerChannel(channel,1*30*1000);
		     System.out.println( "Channel: " + channel.getTitle() );
		     System.out.println( "Description: " + channel.getDescription() );
		      System.out.println( "PubDate: " + channel.getPubDate() );
		      Collection items = channel.getItems();
		      for( Iterator i=items.iterator(); i.hasNext(); )
		      {
		        ItemIF item = ( ItemIF )i.next();
		       tableModel.addItem(item);
		        System.out.println( "Channel: " + channel.getTitle() );
			     System.out.println( "Description: " + channel.getDescription() );
			      System.out.println( "PubDate: " + channel.getPubDate() );
			      System.out.println("Refresh Time: " + channel.getUpdatePeriod());
		        //System.out.println(tableModel.getValueAt(1, 0));
		       System.out.println( item.getTitle() );
		       /* System.out.println( "\t" + item.getDescription() );
		        System.out.println( "Categories: " + item.getCategories() );
		        System.out.println( "Date: " + item.getDate() );
		        System.out.println( "Link: " + item.getLink() + "\n" );*/
		      }
			}
			 catch( Exception ex )
			    {
				 URLpopup.setVisible(true);
				 JOptionPane.showMessageDialog(URLpopup, "Link given could not be found", "WOW SUCH ERROR",JOptionPane.ERROR_MESSAGE);
				 URLpopup.dispose();
				
			      ex.printStackTrace();
			    }
			
		}
		if(e.getSource() == SetRefresh)
		{
			timer.stop();
			URLpopup.add(button);
			URLpopup.pack();
			URLpopup.setLocation(600, 400);
			URLpopup.setVisible(true);
			int x;
			JOptionPane.showMessageDialog(URLpopup, "Your current refresh time is "+ delay / 1000 + " seconds");
			String del = JOptionPane.showInputDialog(URLpopup,"Enter Refresh Time in seconds", null);
			if(del == null)
			{
				x = delay / 1000;
			}
			else
			{
				x = Integer.parseInt(del);
			}
			
			//poll.setPeriod(x * 60 * 1000);
			delay = (int) (x * 1000);
			//timer.setDelay(delay);
			timer.setInitialDelay(delay);
			timer.restart();
			URLpopup.dispose();
			URLpopup.setVisible(false);
		}
		
		if(e.getSource()== RenameButton)
		{
			String RenameFeed;
			String NewFeedName;
			Object root;
			Popup.add(button);
			Popup.pack();
			Popup.setLocation(600,400);
			Popup.setVisible(true);
			
			RenameFeed = JOptionPane.showInputDialog(Popup,"Enter name of RSS Feed you wish to rename",null);
			Popup.dispose();
			NewFeedName = JOptionPane.showInputDialog(Popup,"Enter the new Name of feed",null);
			root = tree.getModel().getRoot();
			int cc = tree.getModel().getChildCount(root);
			for(int i = 0; i < cc; i++ )
			{
				DefaultMutableTreeNode child = (DefaultMutableTreeNode) tree.getModel().getChild(root, i);
				System.out.println(child.toString());
				if(0==RenameFeed.compareTo(child.toString()))
				{
					System.out.println("found feed");
					child.setUserObject(NewFeedName);
					String value = this.channelMap.get(RenameFeed.toString());
					this.channelMap.remove(child.toString());
					this.channelMap.put(NewFeedName,value);
					treemodel.nodeChanged(child);
					break;
				}
			}
			
			
		}
		
		if(e.getSource() == SaveButton)
		{
			Properties prop = new Properties();
			OutputStream output = null;
			URLpopup.add(button);
			URLpopup.pack();
			URLpopup.setLocation(600, 400);
			URLpopup.setVisible(true);
			try
			{
				String outputname = JOptionPane.showInputDialog(URLpopup,"Enter name of the output file to save to:",null);
				URLpopup.dispose();
				output = new FileOutputStream(outputname);
				Iterator it = this.channelMap.entrySet().iterator();
				while(it.hasNext())
				{
					Map.Entry< String, String> pairs = (Map.Entry<String, String>)it.next();
					prop.setProperty(pairs.getKey().toString(), pairs.getValue().toString());
					
				}
				prop.store(output, null);
				
			}
			catch(IOException io)
			{
				io.printStackTrace();
			}
		}
		if(e.getSource() == LoadButton)
		{
			Properties prop = new Properties();
			InputStream input = null;
			URLpopup.add(button);
			URLpopup.pack();
			URLpopup.setLocation(600, 400);
			URLpopup.setVisible(true);
			try
			{
				String inputname = JOptionPane.showInputDialog(URLpopup,"Enter name of properties file you wish to load:",null);
				URLpopup.dispose();
				input = new FileInputStream(inputname);
				prop.load(input);
				Iterator it = prop.entrySet().iterator();
				while(it.hasNext())
				{
					Map.Entry< String, String> pairs = (Map.Entry<String, String>)it.next();
					this.channelMap.put(pairs.getKey().toString(), pairs.getValue().toString());
					DefaultMutableTreeNode node_1 = new DefaultMutableTreeNode(pairs.getKey().toString());
					
					treemodel.insertNodeInto(node_1, root, root.getChildCount());
					
				}
				
			}
			catch(IOException io)
			{
				io.printStackTrace();
			}
		}
		if(e.getSource()== UnSubbutton)
		{
			String FeedName;
			Object root;
			TreePath path = tree.getSelectionPath();
			tree.setSelectionPath(path.getParentPath());
			this.tableModel.list.clear();
			this.tableModel.fireTableDataChanged();
			
			Popup.add(button);
			Popup.pack();
			Popup.setVisible(true);
			FeedName = JOptionPane.showInputDialog(Popup,"Enter name of RSS Feed you wish to unsubscribe from",null);
			Popup.dispose();
			root = tree.getModel().getRoot();
			int cc = tree.getModel().getChildCount(root);
			for(int i = 0; i < cc; i++ )
			{
				DefaultMutableTreeNode child = (DefaultMutableTreeNode) tree.getModel().getChild(root, i);
				System.out.println(child.toString());
				if(0==FeedName.compareTo(child.toString()))
				{
					System.out.println("found feed");
					//child.removeFromParent();
					this.channelMap.remove(FeedName.toString());
					treemodel.removeNodeFromParent(child);
					tableModel.list.clear();
					tableModel.fireTableStructureChanged();
					break;
				}
			}
			
			
		}
		if(e.getSource()== FindbyMap)
		{
			//Popup = new WorldWind();
           String longitude;
           String latitude;
           Double lon = null;
           Double lat = null;
           int count = 0;
             wwd.setPreferredSize(new java.awt.Dimension(1000, 800));
             Popup.getContentPane().add(wwd, java.awt.BorderLayout.CENTER);
             Component glassPane = Popup.getGlassPane();
             
			glassPane.addMouseListener(myListener);
             model = new BasicModel();
             IconLayer layer = new IconLayer();
            size = tableModel.list.size();
            //String title = ((ItemIF) (tableModel.list.get(0))).getTitle();
            //System.out.println("title of list 0 =" + title);
            
            
            final Collection icons = null;
            for(int i = 0; i < size;i++)
            {
            	title = ((ItemIF) (tableModel.list.get(i))).getTitle();
            
            try
            {
            	br = new BufferedReader(new FileReader(csvFile));
            	
            	while((line = br.readLine())!= null)
            	{
            		String[] city = line.split(cvsSplitBy);
            		/*for(int j = 0; j < 6; j++ )
            		{
            			
            			city[j] = city[j].replaceAll("\"","" );
            			System.out.println(city[j]);
            			/*for(int k = 0; k < city[i].length(); k++)
            			{
            				if(k ==0)
            				{
            					city[i]. = city[i].charAt(k+1);
            				}
            			}
            		}*/
            		//System.out.println("City =" + city[3] + "," + city[2]);
            		if(title.contains(city[3]) && count < 15)
            		{
            			//System.out.println("The Title contains " + city[3]);
            			longitude = city[6];
            			latitude = city[5];
            			lon = Double.parseDouble(longitude);
            			lat = Double.parseDouble(latitude);
            			
            		    WWIcon icon = new UserFacingIcon("C:/Users/Tom/Desktop/worldwind/src/images/pushpins/castshadow-red.png",new Position(Angle.fromDegrees(lat), Angle.fromDegrees(lon),2000));
            		    //icons.add(icon);
            		    icon.setHighlightScale(1.5);
            		   // icon.setToolTipFont(( ((Object) layer).makeToolTipFont());
            		    icon.setToolTipText(title);
            		    icon.setShowToolTip(true);
            		    icon.setToolTipTextColor(java.awt.Color.YELLOW);
            		    layer.addIcon(icon);
            		    count++;
            		   
            		    model.getLayers().add(layer);
            		    wwd.setModel(model);
            			
            			Popup.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
                        Popup.pack();                
                        Popup.setVisible(true);
                        wwd.addSelectListener(new SelectListener()
                        {
                        	public void selected(SelectEvent event) {
                        		if (event.getEventAction().equals(SelectEvent.LEFT_DOUBLE_CLICK)) {
                        		System.out.println("Left click!");
                        		//if(event.getTopPickedObject().hasPosition()) {
                        		
                        		Position pos = wwd.getCurrentPosition();//getTopPickedObject().getPosition() ;
                        		try
                        		{
                        			br = new BufferedReader(new FileReader(csvFile));
                        			line = br.readLine();
                        			while((line = br.readLine())!= null)
                        			{
                        				
                        				String[] city2 = line.split(cvsSplitBy);
                        				//System.out.println("city2 lat =" + city2[5]);
                        				if(pos.getLatitude().getDegrees() == Double.parseDouble(city2[5]) && pos.getLongitude().getDegrees() == Double.parseDouble(city2[6]))
                        				{
                        					title = city2[3];
                        					System.out.println(title);
                        					for(int i = 0; i < size;i++)
                            	            {
                        						//System.out.println("title of table model =" + ((ItemIF)(tableModel.list.get(i))).getTitle());
                        						if(((ItemIF)(tableModel.list.get(i))).getTitle().contains(title))
                        						{
                        							
                        							link = ((ItemIF)(tableModel.list.get(i))).getLink();
                        							title = ((ItemIF)(tableModel.list.get(i))).getTitle();
                        							System.out.println(link);
                        							break;
                        						}
                            	            }
                        					break;
                        				}
                        				//System.out.println(link);
                                		//System.out.println("Lat = " + pos.getLatitude().getDegrees() + 
                                		//", Lon = " + pos.getLongitude().getDegrees() +
                                		//", Elv = " + pos.getElevation());
                                		}
                        			 ChannelBuilder channel = new ChannelBuilder();
                        		        ChannelIF channel2 = new Channel();
                        			ItemIF item2 = channel.createItem(channel2,title , channel2.getDescription(), link);
                        			fireShowLink(item2);
                        		}
                        		catch(Exception exp)
                        		{
                        			exp.printStackTrace();
                        		}
                        		
                        	}
                        		
                        		

						
                        	}});
            	           
            			
            	            
            	           
            			break;
            			
            		}
            	}
            	 //System.out.println("number of icons = " +layer.getIcons().iterator().
            	 } catch (Exception exc)
            	{
            		exc.printStackTrace();
            	}
            		
            }
		}
            
           
		
		
		
		
		
	}

	
	    
	
	  public void mouseReleased(MouseEvent e)
	  {
	  }
	  /**
	   * Invoked when the mouse enters a component.
	   */
	  public void mouseEntered(MouseEvent e)
	  {
	  }
	  /**
	   * Invoked when the mouse exits a component.
	   */
	  public void mouseExited(MouseEvent e)
	  {
	  }

	  public void addRSSActionListener( RSSActionListener l )
	  {
	    this.listeners.add( l );
	  }

	  public void removeRSSActionListener( RSSActionListener l )
	  {
	    this.listeners.remove( l );
	  }
	  public void fireShowLink( ItemIF item )
	  {
		  System.out.println("In fireshowlink");
	    RSSActionEvent e = new RSSActionEvent( item );
	    //Iterator i = this.listeners.iterator();
	    //RSSActionListener l = ( RSSActionListener )i;
	      this.showLink(e);
	   /* for( Iterator i=this.listeners.iterator(); i.hasNext(); )
	    {
	      RSSActionListener l = ( RSSActionListener )i.next();
	      l.showLink( e );
	    }*/
	  }
	  @SuppressWarnings("deprecation")
	public void showLink(RSSActionEvent event)
	  {
		  System.out.println("In showlink");
		 //JDesktopPane desktop = new JDesktopPane();
	    HTMLViewer viewer = new HTMLViewer(  event.getItem() );
	   HTMLWindow.add( viewer );
	   HTMLWindow.setSize(800, 600);
	   HTMLWindow.setLocationRelativeTo(table);
	    
	    //position += INDENT;
	    //viewer.setLocation( position, position );
	    //desktop.setLocation(2,2);
	    HTMLWindow.setVisible(true);
	    //HTMLWindow.show();
	  }
	
	  public void mouseClicked(MouseEvent e)
	  {
		
	    if( e.getClickCount() == 2 )
	    {
	      int row = this.table.rowAtPoint( e.getPoint() );
	     
	      if( row != -1 )
	      {
	        // User double clicked a row in the table
	    	  
	    	 System.out.println(this.tableModel.isCellEditable(row, 3));
	    	  //.tableModel.ChangeValueAt( row, 3);
	    	 this.tableModel.setValueAt(false, row, 3);
	    	 //this.tableModel.fireTableStructureChanged();
	    	 
	    	 System.out.println( this.tableModel.getValueAt(row, 3));
	        String title =  (String) this.tableModel.getValueAt(row, 0);
	        Object link =  this.tableModel.getValueAt(row, 2);
	        System.out.println( link + ": " + title );
	        //ItemIF item = (ItemIF) this.tableModel.getValueAt(row, 2);
	        ChannelBuilder channel = new ChannelBuilder();
	        ChannelIF channel2 = new Channel();
	        ItemIF item2 = channel.createItem(channel2, title, channel2.getDescription(), this.tableModel.getValuelink(row, 2));
	        
	        
	        this.fireShowLink( item2);
	      }
	    }
	    if(e.getSource() == wwd)
	    {
	    	System.out.println("doge");
	    }
	    
	  }



	@Override
	public void mousePressed(MouseEvent e) {
		// TODO Auto-generated method stub
		
	}








	




	
	
	
	
}
