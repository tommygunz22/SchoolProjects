import gov.nasa.worldwind.BasicModel;
import gov.nasa.worldwind.*;
import gov.nasa.worldwind.awt.*;
import gov.nasa.worldwind.Model;
import gov.nasa.worldwind.avlist.AVKey;
import gov.nasa.worldwind.awt.WorldWindowGLCanvas;
import gov.nasa.worldwind.geom.Angle;
import gov.nasa.worldwind.geom.Position;
import gov.nasa.worldwind.layers.IconLayer;
import gov.nasa.worldwind.layers.Earth.*;
import gov.nasa.worldwind.render.UserFacingIcon;
import gov.nasa.worldwind.render.WWIcon;
//import gov.nasa.worldwind.WorldWind;
import gov.nasa.worldwind.avlist.*;
import gov.nasa.worldwind.cache.FileStore;
import gov.nasa.worldwind.data.*;
import gov.nasa.worldwind.util.*;
import com.sun.*;

import javax.swing.*;
public class WorldWind extends JFrame {

	public  WorldWindowGLCanvas wwd = new WorldWindowGLCanvas();
	public Model model;
	public WorldWind()
    {
       
        wwd.setPreferredSize(new java.awt.Dimension(1000, 800));
        this.getContentPane().add(wwd, java.awt.BorderLayout.CENTER);
        model = new BasicModel();// (Model)WorldWind.createConfigurationComponent(AVKey.MODEL_CLASS_NAME);
        
        
    }
	
	public void buildIconLayer (double lat,double lon)
	{
	    IconLayer layer = new IconLayer();
	    WWIcon icon = new UserFacingIcon("C:/Users/Tom/Desktop/worldwind/src/images/pushpins/castshadow-red.png",new Position(Angle.fromDegrees(lat), Angle.fromDegrees(lon),2000));
	    icon.setHighlightScale(1.5);
	    //icon.setToolTipFont(( layer.makeToolTipFont());
	    //icon.setToolTipText();
	    icon.setToolTipTextColor(java.awt.Color.YELLOW);
	    layer.addIcon(icon);
	    model.getLayers().add(layer);
	    wwd.setModel(model);
	}  
        
    
	

}



