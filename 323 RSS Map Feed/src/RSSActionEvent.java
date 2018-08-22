
import java.util.*;
import java.net.*;

// Import the Informa Libraries
import de.nava.informa.core.*;

public class RSSActionEvent extends EventObject
{
  private ItemIF item;
  public RSSActionEvent( ItemIF item )
  {
    super( item );
    this.item = item;
  }

  public URL getLink()
  {
    return this.item.getLink();
  }

  public ItemIF getItem()
  {
    return this.item;
  }
} 