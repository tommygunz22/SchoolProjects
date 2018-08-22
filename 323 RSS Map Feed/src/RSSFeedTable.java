

// Import the Java classes
import javax.swing.*;
import javax.swing.table.*;

import java.net.URL;
import java.util.*;

// Import the Informa Libraries
import de.nava.informa.core.*;

/**
 * Table Model representing a list of RSS Items
 */
public class RSSFeedTable extends AbstractTableModel
{
  public ArrayList list = new ArrayList();
  public ArrayList titles = new ArrayList();

  public RSSFeedTable()
  {
  }

  public void setChannel( ChannelIF channel )
  {
    // Remove the old items
    int size = list.size();
    list.clear();
    this.fireTableRowsDeleted( 0, size );

    // Add the new items
    list.addAll( channel.getItems() );
    this.fireTableRowsUpdated( 0, list.size() - 1 );
  }

  public void addItem( ItemIF item )
  {
    this.list.add( item );
    //this.fireTableRowsInserted( list.size() - 1, list.size() - 1 );
  }

  public boolean isCellEditable(int rowIndex, int columnIndex)
  {
	 switch (columnIndex) {
      case 3:
          return true;
      default:
          return false;
  }
  }
  
  public String getColumnName(int column)
  {
    switch( column )
    {
    case 0: return "Title";
    case 1: return "Date";
    case 2: return "Link";
    case 3: return "UnRead?";
    default: return "Unknown";
    }
  }
  
  public Class getColumnClass(int columnIndex)
  {
    return new String().getClass();
  }
  
  public int getColumnCount()
  {
    return 4;
  }
  
  public int getRowCount()
  {
    return this.list.size();
  }
  
  public Object getValueAt(int rowIndex, int columnIndex)
  {
    ItemIF item = ( ItemIF )this.list.get( rowIndex );
    switch( columnIndex )
    {
     case 0: return item.getTitle();
    case 1: return item.getDate();
    case 2: return item.getLink();
    case 3: return item.getUnRead();
    
    default: return "Unknown";
    }
  }
  
  
  public Object ChangeValueAt( int rowIndex, int columnIndex)
  {
    ItemIF item = ( ItemIF )this.list.get( rowIndex );
    switch( columnIndex )
    {
    
    case 3: return item.getUnRead();
    default: return "Unknown";
    }
  }
  public URL getValuelink(int rowIndex, int columnIndex)
  {
    ItemIF item = ( ItemIF )this.list.get( rowIndex );
   return item.getLink();
  }

  @Override
  public void setValueAt(Object value, int row, int col)
  {
	  ItemIF item = ( ItemIF )this.list.get( row );
     if(col == 3)
    	 item.setUnRead(false);
      
      fireTableCellUpdated(row,col);
  }

}

