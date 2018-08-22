import java.awt.*;
import javax.swing.*;

public class GUIUtils
{
  public static JPanel getLJPanel( String label )
  {
    return GUIUtils.getLJPanel( new JLabel( label ) );
  }
  
  public static JPanel getRJPanel( String label )
  {
    return GUIUtils.getRJPanel( new JLabel( label ) );
  }
    
  public static JPanel getCenteredPanel( String label )
  {
    return GUIUtils.getCenteredPanel( new JLabel( label ) );
  }

  public static JPanel getLJPanel( Component c )
  {
    JPanel panel = new JPanel( new FlowLayout( FlowLayout.LEFT ) );
    panel.add( c );
    return panel;
  }
  
  public static JPanel getRJPanel( Component c )
  {
    JPanel panel = new JPanel( new FlowLayout( FlowLayout.RIGHT ) );
    panel.add( c );
    return panel;
  }
    
  public static JPanel getCenteredPanel( Component c )
  {
    JPanel panel = new JPanel( new FlowLayout( FlowLayout.CENTER ) );
    panel.add( c );
    return panel;
  }
}