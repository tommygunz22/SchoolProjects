import javax.swing.tree.*;

// Import the Informa libraries
import de.nava.informa.core.*;

public class FeedNode extends DefaultMutableTreeNode
{
  public FeedNode( ChannelIF channel )
  {
    this.setUserObject( channel );
  }

  public boolean getAllowsChildren()
  {
    return false;
  }
  
  public boolean isLeaf()
  {
    return true;
  }
  
  public String toString()
  {
    ChannelIF channel = ( ChannelIF )this.getUserObject();
    return channel.getTitle();
  }
}