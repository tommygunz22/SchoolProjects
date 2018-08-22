import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JTextArea;
import javax.swing.SpringLayout;

import java.awt.Color;
import java.awt.Desktop;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Toolkit;

import javax.swing.JButton;

import java.awt.BorderLayout;

import javax.swing.JFormattedTextField;

import java.awt.GridLayout;

import javax.swing.JLabel;

import java.awt.FlowLayout;

import com.google.api.services.mapsengine.model.Image;
import com.jgoodies.forms.layout.FormLayout;
import com.jgoodies.forms.layout.ColumnSpec;
import com.jgoodies.forms.layout.RowSpec;

import de.nava.informa.core.ItemIF;

import javax.swing.JToolBar;

import java.awt.event.*;

import javax.swing.*;
import javax.swing.GroupLayout.Alignment;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.DefaultMutableTreeNode;

import java.awt.Font;
import java.awt.Component;
public class creatgui extends JFrame 
{
	public JDesktopPane desktop = new JDesktopPane()
	{
	    ImageIcon icon = new ImageIcon("C:/Users/Tom/workspace/src/doge.png");
	    java.awt.Image image = icon.getImage();

	    java.awt.Image newimage = image.getScaledInstance(1500, 1000, java.awt.Image.SCALE_SMOOTH);
	    
	    @Override
	    protected void paintComponent(Graphics g)
	    {
	        super.paintComponent(g);
	        g.drawImage(newimage, 0, 0, this);
	    }
	};
	private JTable table;
	private JTextField textField;
	//private ImageIcon icon = new ImageIcon("");
	//desktop.setLayout(new BorderLayout());
	
  
	
	private RSSManagerFrame rssManager = new RSSManagerFrame();
	public creatgui() 
	{
		super("Meaty Jellyfishes RSS Reader");
		
		/*ImageIcon img = new ImageIcon("C:/Users/Tom/workspace/src/doge.png");

		JLabel label_background = new JLabel();
		label_background.setIcon(img);
		desktop.setLayout(new BorderLayout());
		desktop.add(label_background, BorderLayout.CENTER);
		*/
		
		desktop.setSize(1000, 1000);
		rssManager.setLocation(0, 0);
		rssManager.setSize(1190, 560);

		
		desktop.add(rssManager);
		
		
		
		
		this.getContentPane().add(desktop,BorderLayout.CENTER);
		Font newfont = new Font("Serif", Font.BOLD, 12);
		this.setSize(1200,600);
		this.setFont(newfont);
		Dimension d = Toolkit.getDefaultToolkit().getScreenSize();
		this.setLocation(200,100);
		this.setVisible(true);
		this.addWindowListener(new WindowAdapter(){public void windowClosing(WindowEvent e) { System.exit(0);}});
		
		/*JButton AddFeedButton = new JButton("Add New Feed");
		AddFeedButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
			}
		});
		menuBar.add(AddFeedButton);
		
		JButton btnEditChannels = new JButton("Edit Channels");
		menuBar.add(btnEditChannels);
		
		JButton btnViewMap = new JButton("View Map");
		menuBar.add(btnViewMap);
		
		JButton btnSaveLoad = new JButton("Save/Load Config");
		menuBar.add(btnSaveLoad);*/
		
		
		

		}
	
	

	public static void main(String[] args) {
		
		
		creatgui Creategui = new creatgui(); 
	
	}
	private static void addPopup(Component component, final JPopupMenu popup) {
		component.addMouseListener(new MouseAdapter() {
			public void mousePressed(MouseEvent e) {
				if (e.isPopupTrigger()) {
					showMenu(e);
				}
			}
			public void mouseReleased(MouseEvent e) {
				if (e.isPopupTrigger()) {
					showMenu(e);
				}
			}
			private void showMenu(MouseEvent e) {
				popup.show(e.getComponent(), e.getX(), e.getY());
			}
		});
	}
}
